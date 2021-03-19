﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// キャンバスクラス
    /// </summary>
    public class Canvas : ICanvas {

        #region 定数

        private static readonly Size C_DefaultMovingSize = new Size(10, 10);

        #endregion 定数

        #region フィールド

        private List<IShape> FShapes = new List<IShape>();
        private bool FIsFramePointSelected;

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// 表示状態
        /// </summary>
        public bool IsPreviewing { get; set; }

        /// <summary>
        /// クリップボード
        /// </summary>
        public List<IShape> Clipboard { get; set; } = new List<IShape>();

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            if (this.IsPreviewing) return vBitmap;
            FShapes.ForEach(x => x.Draw(vBitmap));
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.DrawFrame(vBitmap);
            }
            return vBitmap;
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
            FIsFramePointSelected = false;

            foreach (IShape wShape in Enumerable.Reverse(FShapes.Where(x => x.IsSelected))) {
                IFramePoint wFramePoint = wShape.FramePoints.FirstOrDefault(x => x.IsHit(vCoordinate));
                if (wFramePoint == null) continue;
                SetFramePointIsSelected(wFramePoint.LocationKind, (vShape) => vShape.IsSelected && vShape.Dimensionality == wShape.Dimensionality);
                FIsFramePointSelected = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 全選択します
        /// </summary>
        public void AllSelect() => FShapes.ForEach(x => x.IsSelected = true);

        /// <summary>
        /// 選択を解除します
        /// </summary>
        public void Unselect() => FShapes.ForEach(x => x.IsSelected = false);

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape) => FShapes.Add(vShape);


        /// <summary>
        /// 編集します
        /// </summary>
        public void Edit(Size vSize) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                if (FIsFramePointSelected) {
                    // 拡大・縮小
                    wShape.ChangeScale(vSize);
                } else {
                    // 移動
                    wShape.Move(vSize);
                }
            }
        }

        /// <summary>
        /// 右に回転させます
        /// </summary>
        public void RotateRight() {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.RotateRight();
            }
        }

        /// <summary>
        /// 最前面に移動します
        /// </summary>
        public void MoveToFront() => FShapes = FShapes.OrderBy(x => x.IsSelected).ToList();

        /// <summary>
        /// 最背面に移動します
        /// </summary>
        public void MoveToBack() => FShapes = FShapes.OrderByDescending(x => x.IsSelected).ToList();

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
        }

        /// <summary>
        /// コピーします
        /// </summary>
        public void Copy(bool vIsCut = false) {
            var wSelectedShapes = FShapes.Where(x => x.IsSelected).ToList();
            if (wSelectedShapes.Count == 0) return;

            this.Clipboard = new List<IShape>();
            foreach (IShape wShape in wSelectedShapes) {
                if (vIsCut) FShapes.Remove(wShape);
                IShape wCopy = wShape.DeepClone();

                // 選択状態にしておく
                wCopy.IsSelected = true;

                wCopy.Move(C_DefaultMovingSize);
                this.Clipboard.Add(wCopy);
            }
        }

        /// <summary>
        /// 貼り付けます
        /// </summary>
        public void Paste() {
            FShapes.ForEach(x => x.IsSelected = false);
            FShapes.AddRange(this.Clipboard.Select(x => x.DeepClone()));

            // 貼り付け位置を更新しておく
            this.Clipboard.ForEach(x => x.Move(C_DefaultMovingSize));
        }

        /// <summary>
        /// 削除します
        /// </summary>
        public void Remove() => FShapes.RemoveAll(x => x.IsSelected);

        /// <summary>
        /// クリアします
        /// </summary>
        public void Clear() => FShapes.Clear();

        /// <summary>
        /// 自身のインスタンスを複製します
        /// </summary>
        public ICanvas DeepClone() {
            var wClone = new Canvas();
            wClone.FIsFramePointSelected = FIsFramePointSelected;
            foreach (IShape wShape in this.Clipboard) {
                wClone.Clipboard.Add(wShape.DeepClone());
            }
            foreach (IShape wShape in FShapes) {
                wClone.Add(wShape.DeepClone());
            }
            return wClone;
        }

        #endregion メソッド

    }
}
