﻿using System;
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
        private List<IShape> FCopyShapes;

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
        /// 図形リスト
        /// </summary>
        public IEnumerable<IShape> Shapes => FPreviewShapes ?? FShapes;

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
                try {
                    checked {
                        wShape.Draw(FGraphics, !this.IsPreviewing);
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

            foreach (IShape wShape in Enumerable.Reverse(FShapes.Where(x => x.IsSelected))) {
                IFramePoint wFramePoint = wShape.FramePoints.FirstOrDefault(x => x.IsHit(vCoordinate));
                if (wFramePoint == null) continue;
                SetFramePointIsSelected(wFramePoint.CurrentLocationKind, (vShape) => vShape.IsSelected && vShape.Dimensionality == wShape.Dimensionality);
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
                foreach (IFramePoint wFrame in wShape.FramePoints) {
                    wFrame.IsSelected = false;
                }
            }
            this.Draw();
        }

        /// <summary>
        /// 全選択します
        /// </summary>
        public void SelectAll() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
            }
            this.Draw();
        }

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape) {
            vShape.IsSelected = true;
            if (this.IsPreviewing) {
                IShape wShape = vShape.DeepClone();
                wShape.FramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.Bottom).IsSelected = true;
                FPreviewShapes.Add(wShape);
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
        /// リセットします
        /// </summary>
        public void Reset() {
            FShapes.Clear();
            this.Draw();
        }

        /// <summary>
        /// コピーします
        /// </summary>
        public void Copy(bool vIsCut) {
            var wSelectedShapes = FShapes.Where(x => x.IsSelected).ToList();
            if (wSelectedShapes.Count == 0) return;

            FCopyShapes = new List<IShape>();
            foreach (IShape wShape in wSelectedShapes) {
                if (vIsCut) FShapes.Remove(wShape);
                IShape wCopy = wShape.DeepClone();

                // 選択状態にしておく
                wCopy.IsSelected = true;

                wCopy.Move(C_DefaultMovingSize);
                FCopyShapes.Add(wCopy);
            }
            if (vIsCut) this.Draw();
        }

        /// <summary>
        /// 貼り付けます
        /// </summary>
        public void Paste() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = false;
            }
            FShapes.AddRange(FCopyShapes.Select(x => x.DeepClone()));

            // 貼り付け位置を更新しておく
            foreach (IShape wShape in FCopyShapes) {
                wShape.Move(C_DefaultMovingSize);
            }
            this.Draw();
        }

        /// <summary>
        /// 複製します
        /// </summary>
        public void Clone() {
            var wClonedShapes = new List<IShape>();
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                IShape wClone = wShape.DeepClone();

                // 選択状態をスイッチします
                wShape.IsSelected = false;
                wClone.IsSelected = true;

                wClone.Move(C_DefaultMovingSize);
                wClonedShapes.Add(wClone);
            }
            FShapes.AddRange(wClonedShapes);
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