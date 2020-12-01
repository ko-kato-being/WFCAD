using System.Collections.Generic;
using System.Drawing;
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
        /// 現在の図形
        /// </summary>
        public IShape CurrentShape { get; set; }

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
        /// 図形を追加します
        /// </summary>
        public void AddShape() {
            if (this.CurrentShape == null) return;
            IShape wShape = this.CurrentShape.DeepClone();
            wShape.StartPoint = this.MouseDownLocation;
            wShape.EndPoint = this.MouseUpLocation;
            FShapes.Add(wShape);
            this.Refresh();
        }

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        public void Clear() {
            Image wOldImage = FPictureBox.Image;
            FPictureBox.Image = new Bitmap(FPictureBox.Width, FPictureBox.Height);
            wOldImage?.Dispose();
            using (var wGraphics = Graphics.FromImage(FPictureBox.Image)) {
                wGraphics.FillRectangle(Brushes.White, new System.Drawing.Rectangle(0, 0, FPictureBox.Width, FPictureBox.Height));
            }
            this.FShapes.Clear();
        }

        #endregion メソッド

    }
}
