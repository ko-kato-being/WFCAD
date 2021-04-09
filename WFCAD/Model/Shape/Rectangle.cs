using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle : Shape2D {

        #region　メソッド

        /// <summary>
        /// 初期化します
        /// </summary>
        public override void Initialize(PointF vStartPoint, PointF vEndPoint) {
            var wRectangle = new RectangleF(vStartPoint.X, vStartPoint.Y, vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y);
            this.MainPath.AddRectangle(wRectangle);
            this.SubPath.AddRectangle(wRectangle);
        }

        /// <summary>
        /// 描画します
        /// </summary>
        public override void Draw(Bitmap vBitmap, Graphics vGraphics) {
            using (var wBrush = new SolidBrush(this.Color))
            using (var wPen = new Pen(C_BorderColor, 2f)) {
                vGraphics.FillPath(wBrush, this.MainPath);
                vGraphics.DrawPath(wPen, this.MainPath);
            }
            if (!this.IsSelected) return;
            this.DrawFrame(vBitmap, vGraphics);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Rectangle();

        #endregion　メソッド

    }
}
