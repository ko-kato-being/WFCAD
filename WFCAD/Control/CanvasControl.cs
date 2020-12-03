using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスコントロール
    /// </summary>
    public class CanvasControl : ICanvasControl {
        private readonly PictureBox FPictureBox;
        private List<IShape> FShapes;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasControl(PictureBox vPictureBox) {
            FShapes = new List<IShape>();
            FPictureBox = vPictureBox;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// マウスダウン位置
        /// </summary>
        public Point MouseDownLocation { get; set; }

        /// <summary>
        /// マウスアップ位置
        /// </summary>
        public Point MouseUpLocation { get; set; }

        /// <summary>
        /// 現在のマウスカーソル位置
        /// </summary>
        public Point CurrentMouseLocation { get; set; }

        /// <summary>
        /// 描画色
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 再描画します
        /// </summary>
        public void Refresh() {
            Image wOldImage = FPictureBox.Image;
            FPictureBox.Image = new Bitmap(FPictureBox.Width, FPictureBox.Height);
            wOldImage?.Dispose();
            using (var wGraphics = Graphics.FromImage(FPictureBox.Image)) {
                this.FShapes.ForEach(x => x.Draw(wGraphics));
            }
        }

        /// <summary>
        /// 図形を選択します
        /// </summary>
        public void SelectShape(Point vMouseLocation, bool vIsMultiple) {
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
                wShape.Option.Color = wShape.IsSelected ? Color.Blue : Color.Black;
            }
            this.Refresh();
        }

        /// <summary>
        /// 図形を追加します
        /// </summary>
        public void AddShape(IShape vShape) {
            // 二点間の距離が10以下の図形は意図していないとみなして追加しない。
            double wDistance = Utilities.GetDistance(this.MouseDownLocation, this.MouseUpLocation);
            if (wDistance < 10) return;

            IShape wShape = vShape.DeepClone();
            wShape.StartPoint = this.MouseDownLocation;
            wShape.EndPoint = this.MouseUpLocation;
            wShape.Option = new Pen(this.Color);
            FShapes.Add(wShape);
            this.Refresh();
        }

        /// <summary>
        /// 図形を移動します
        /// </summary>
        public void MoveShapes(Point vMouseLocation) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                var wMovingSize = new Size(vMouseLocation.X - this.CurrentMouseLocation.X, vMouseLocation.Y - this.CurrentMouseLocation.Y);
                wShape.StartPoint += wMovingSize;
                wShape.EndPoint += wMovingSize;
            }
            this.CurrentMouseLocation = vMouseLocation;
            this.Refresh();
        }

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        public void RemoveShapes() {
            FShapes.RemoveAll(x => x.IsSelected);
            this.Refresh();
        }

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        public void Clear() {
            FShapes.Clear();
            this.Refresh();
        }

        #endregion メソッド

    }
}
