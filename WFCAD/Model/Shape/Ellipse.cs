using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 楕円クラス
    /// </summary>
    public class Ellipse : Shape2D {

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        protected override void DrawCore(Graphics vGraphics) {
            vGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var wBrush = new SolidBrush(this.Color))
            using (var wPen = new Pen(C_BorderColor, 3f)) {
                vGraphics.DrawEllipse(wPen, this.FrameRectangle);
                vGraphics.FillEllipse(wBrush, this.FrameRectangle);
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(PointF vCoordinate) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddEllipse(this.FrameRectangle);
                return wPath.IsVisible(vCoordinate.X, vCoordinate.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Ellipse();

        #endregion　メソッド

    }
}
