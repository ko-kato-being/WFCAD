using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD {
    /// <summary>
    /// 図形群クラス
    /// </summary>
    public class Shapes : IShapes {
        private List<IShape> FShapes = new List<IShape>();

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            FShapes.ForEach(x => x.Draw(vBitmap));
            return vBitmap;
        }

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape) => FShapes.Add(vShape);

        /// <summary>
        /// 選択します
        /// </summary>
        public void Select(Point vMouseLocation, bool vIsMultiple) {
            bool wHasSelected = false;
            foreach (IShape wShape in Enumerable.Reverse(FShapes)) {
                if (wHasSelected) {
                    wShape.IsSelected = false;
                } else {
                    bool wIsHit = wShape.IsHit(vMouseLocation);
                    if (vIsMultiple) {
                        wShape.IsSelected = wShape.IsSelected || wIsHit;
                    } else {
                        wShape.IsSelected = wIsHit;
                        if (wShape.IsSelected) {
                            wHasSelected = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(Size vSize) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.StartPoint += vSize;
                wShape.EndPoint += vSize;
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

                // 右下方向
                var wMovingSize = new Size(10, 10);
                wClone.StartPoint += wMovingSize;
                wClone.EndPoint += wMovingSize;
                wClonedShapes.Add(wClone);
            }
            FShapes.AddRange(wClonedShapes);
        }

        /// <summary>
        /// 削除します
        /// </summary>
        public void Remove() => FShapes.RemoveAll(x => x.IsSelected);

        /// <summary>
        /// クリアします
        /// </summary>
        public void Clear() => FShapes.Clear();

        #endregion メソッド

    }
}
