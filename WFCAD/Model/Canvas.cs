using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// キャンバスクラス
    /// </summary>
    public class Canvas {

        #region 定数

        private static readonly Size C_DefaultMovingSize = new Size(10, 10);

        #endregion 定数

        #region イベント

        /// <summary>
        /// 更新イベント
        /// </summary>
        public event Action Updated;

        #endregion イベント

        #region フィールド

        private Graphics FGraphics;
        private readonly Color FCanvasColor;
        private List<IShape> FShapes = new List<IShape>();
        private List<IShape> FPreviewShapes;


        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Canvas(Image vImage, Color vCanvasColor) {
            FGraphics = Graphics.FromImage(vImage);
            FGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            FCanvasColor = vCanvasColor;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// プレビュー中かどうか
        /// </summary>
        public bool IsPreviewing {
            get => FPreviewShapes != null;
            set {
                if (value) {
                    FPreviewShapes = FShapes.Select(x => x.DeepClone()).ToList();
                } else {
                    FPreviewShapes = null;
                }
            }
        }

        /// <summary>
        /// 枠点が選択されているか
        /// </summary>
        public bool IsFramePointSelected { get; private set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// グラフィックを更新します
        /// </summary>
        public void UpdateGraphics(Image vImage) {
            FGraphics.Dispose();
            FGraphics = Graphics.FromImage(vImage);
            FGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.Draw();
        }

        /// <summary>
        /// 描画します
        /// </summary>
        public void Draw() {
            FGraphics.Clear(FCanvasColor);
            foreach (IShape wShape in FPreviewShapes ?? FShapes) {
                wShape.ApplyAffine();
                try {
                    checked {
                        wShape.Draw(FGraphics);
                    }
                } catch (OverflowException) {
                    // TODO:オーバーフローの原因調査 (とりあえず無視)
                    Console.WriteLine("オーバーフローしました。");
                }
            }
            this.Updated?.Invoke();
        }

        /// <summary>
        /// 選択します
        /// </summary>
        public void Select(Point vCoordinate, bool vIsMultiple) {
            if (this.FramePointSelect(vCoordinate)) return;

            bool wIsAlreadyHit = false;
            bool wIsMultiSelected = FShapes.Where(x => x.IsSelected).ToList().Count >= 2;
            foreach (IShape wShape in Enumerable.Reverse(FShapes)) {
                bool wIsHit = wShape.IsHit(vCoordinate);
                if (vIsMultiple) {
                    // 複数選択モードの場合
                    if (!wIsAlreadyHit && wIsHit) {
                        wShape.IsSelected = !wShape.IsSelected;
                    }
                } else if (wIsMultiSelected) {
                    // 既に複数選択されている場合
                    if (!wIsAlreadyHit && wIsHit && !wShape.IsSelected) {
                        // 未選択の図形を選択した場合は他の図形の選択を解除して終了
                        this.Unselect();
                        wShape.IsSelected = true;
                        return;
                    }
                } else {
                    if (!wIsAlreadyHit && wIsHit) {
                        wShape.IsSelected = true;
                    } else {
                        wShape.IsSelected = false;
                    }
                }
                wIsAlreadyHit |= wIsHit;
            }
            if (!wIsAlreadyHit) this.Unselect();
            this.Draw();
        }

        /// <summary>
        /// 枠点を選択します
        /// </summary>
        private bool FramePointSelect(Point vCoordinate) {
            // 各図形の枠点選択状態を設定します
            void SetFramePointIsSelected(FramePointLocationKindEnum? vTargetKind, Func<IShape, bool> vFilter) {
                foreach (IShape wShape in FShapes.Where(vFilter)) {
                    foreach (IFramePoint wFramePoint in wShape.FramePoints) {
                        wFramePoint.IsSelected = wFramePoint.CurrentLocationKind == vTargetKind;
                    }
                }
            }

            // 全図形の枠点選択状態を初期化
            SetFramePointIsSelected(null, (vShape) => true);
            this.IsFramePointSelected = false;

            foreach (IShape wShape in Enumerable.Reverse(FShapes.Where(x => x.IsSelected))) {
                IFramePoint wFramePoint = wShape.FramePoints.FirstOrDefault(x => x.IsHit(vCoordinate));
                if (wFramePoint == null) continue;
                SetFramePointIsSelected(wFramePoint.CurrentLocationKind, (vShape) => vShape.IsSelected && vShape.Dimensionality == wShape.Dimensionality);
                this.IsFramePointSelected = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 選択を解除します
        /// </summary>
        public void Unselect() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = false;
            }
            this.Draw();
        }

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape) {
            if (this.IsPreviewing) {
                IShape wShape = vShape.DeepClone();
                wShape.IsSelected = true;
                wShape.FramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.Bottom).IsSelected = true;
                FPreviewShapes.Add(wShape);
                this.IsFramePointSelected = true;
            } else {
                FShapes.Add(vShape);
            }
            this.Draw();
        }

        /// <summary>
        /// 削除します
        /// </summary>
        public void Remove() {
            for (int i = FShapes.Count - 1; i >= 0; i--) {
                if (!FShapes[i].IsSelected) continue;
                FShapes.RemoveAt(i);
            }
            this.Draw();
        }

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(PointF vStartPoint, PointF vEndPoint) {
            var wSize = new SizeF(vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y);
            if (wSize.IsEmpty) return;

            foreach (IShape wShape in (FPreviewShapes ?? FShapes).Where(x => x.IsSelected)) {
                wShape.Move(wSize);
            }
            this.Draw();
        }

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(PointF vStartPoint, PointF vEndPoint) {
            if (vStartPoint == vEndPoint) return;

            foreach (IShape wShape in (FPreviewShapes ?? FShapes).Where(x => x.IsSelected)) {
                wShape.Zoom(vStartPoint, vEndPoint, this.IsPreviewing);
            }
            this.Draw();
        }

        /// <summary>
        /// 回転させます
        /// </summary>
        public void Rotate(float vAngle) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.Rotate(vAngle);
            }
            this.Draw();
        }

        #endregion メソッド

    }
}