using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// キャンバスクラス
    /// </summary>
    public class Canvas : IDisposable {

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

        private readonly Matrix FMatrix;
        private List<IShape> FShapes = new List<IShape>();

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Canvas(Image vImage, Color vCanvasColor) {
            FMatrix = new Matrix();
            this.Graphics = Graphics.FromImage(vImage);
            this.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.CanvasColor = vCanvasColor;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// グラフィック
        /// </summary>
        public Graphics Graphics { get; set; }

        /// <summary>
        /// キャンバスの色
        /// </summary>
        public Color CanvasColor { get; set; }

        /// <summary>
        /// 枠点が選択されているか
        /// </summary>
        public bool IsFramePointSelected { get; private set; }

        /// <summary>
        /// クリップボード
        /// </summary>
        public List<IShape> Clipboard { get; set; } = new List<IShape>();

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// すべてのリソースを解放します
        /// </summary>
        public void Dispose() {
            FMatrix.Dispose();
            this.Graphics.Dispose();
        }

        /// <summary>
        /// 描画します
        /// </summary>
        public void Draw() {
            this.Graphics.Clear(this.CanvasColor);
            foreach (IShape wShape in FShapes) {
                wShape.Draw(this.Graphics);
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
                        wFramePoint.IsSelected = wFramePoint.LocationKind == vTargetKind;
                    }
                }
            }

            // 全図形の枠点選択状態を初期化
            SetFramePointIsSelected(null, (vShape) => true);
            IsFramePointSelected = false;

            foreach (IShape wShape in Enumerable.Reverse(FShapes.Where(x => x.IsSelected))) {
                IFramePoint wFramePoint = wShape.FramePoints.FirstOrDefault(x => x.IsHit(vCoordinate));
                if (wFramePoint == null) continue;
                SetFramePointIsSelected(wFramePoint.LocationKind, (vShape) => vShape.IsSelected && vShape.Dimensionality == wShape.Dimensionality);
                IsFramePointSelected = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 全選択します
        /// </summary>
        public void AllSelect() {
            FShapes.ForEach(x => x.IsSelected = true);
            this.Draw();
        }

        /// <summary>
        /// 選択を解除します
        /// </summary>
        public void Unselect() {
            FShapes.ForEach(x => x.IsSelected = false);
            this.Draw();
        }

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape, Point vStartPoint, Point vEndPoint) {
            vShape.Initialize(vStartPoint, vEndPoint);
            FShapes.Add(vShape);
            this.Draw();
        }

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(PointF vStartPoint, PointF vEndPoint) {
            float wOffsetX = vEndPoint.X - vStartPoint.X;
            float wOffsetY = vEndPoint.Y - vStartPoint.Y;
            if (wOffsetX == 0 && wOffsetY == 0) return;

            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.Move(wOffsetX, wOffsetY);
            }
            this.Draw();
        }

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(PointF vStartPoint, PointF vEndPoint) {
            float wScaleX = vEndPoint.X / vStartPoint.X;
            float wScaleY = vEndPoint.Y / vStartPoint.Y;
            if (wScaleX == 0 && wScaleY == 0) return;

            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.Zoom(wScaleX, wScaleY);
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

        /// <summary>
        /// 最前面に移動します
        /// </summary>
        public void MoveToFront() {
            FShapes = FShapes.OrderBy(x => x.IsSelected).ToList();
            this.Draw();
        }

        /// <summary>
        /// 最背面に移動します
        /// </summary>
        public void MoveToBack() {
            FShapes = FShapes.OrderByDescending(x => x.IsSelected).ToList();
            this.Draw();
        }

        /// <summary>
        /// 複製します
        /// </summary>
        public void Clone() {
            //var wClonedShapes = new List<IShape>();
            //foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
            //    IShape wClone = wShape.DeepClone();

            //    // 選択状態をスイッチします
            //    wShape.IsSelected = false;
            //    wClone.IsSelected = true;

            //    wClone.Move(C_DefaultMovingSize);
            //    wClonedShapes.Add(wClone);
            //}
            //FShapes.AddRange(wClonedShapes);
            //this.Draw();
        }

        /// <summary>
        /// コピーします
        /// </summary>
        public void Copy(bool vIsCut = false) {
            //var wSelectedShapes = FShapes.Where(x => x.IsSelected).ToList();
            //if (wSelectedShapes.Count == 0) return;

            //this.Clipboard = new List<IShape>();
            //foreach (IShape wShape in wSelectedShapes) {
            //    if (vIsCut) FShapes.Remove(wShape);
            //    IShape wCopy = wShape.DeepClone();

            //    // 選択状態にしておく
            //    wCopy.IsSelected = true;

            //    wCopy.Move(C_DefaultMovingSize);
            //    this.Clipboard.Add(wCopy);
            //}
            //if (vIsCut) this.Draw();
        }

        /// <summary>
        /// 貼り付けます
        /// </summary>
        public void Paste() {
            //FShapes.ForEach(x => x.IsSelected = false);
            //FShapes.AddRange(this.Clipboard.Select(x => x.DeepClone()));

            //// 貼り付け位置を更新しておく
            //this.Clipboard.ForEach(x => x.Move(C_DefaultMovingSize));
            //this.Draw();
        }

        /// <summary>
        /// 削除します
        /// </summary>
        public void Remove(IShape vShape) {
            FShapes.Remove(vShape);
            this.Draw();
        }

        /// <summary>
        /// クリアします
        /// </summary>
        public void Clear() {
            FShapes.Clear();
            this.Draw();
        }

        #endregion メソッド

    }
}