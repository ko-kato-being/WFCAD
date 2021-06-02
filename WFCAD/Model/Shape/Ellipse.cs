using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 楕円クラス
    /// </summary>
    public class Ellipse : Shape2D {

        #region　メソッド

        /// <summary>
        /// 初期化します
        /// </summary>
        public override void InitializePath(PointF vStartPoint, PointF vEndPoint) {
            var wRectangle = new RectangleF(vStartPoint.X, vStartPoint.Y, vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y);
            this.MainPath.AddEllipse(wRectangle);
            this.SubPath.AddRectangle(wRectangle);
        }

        /// <summary>
        /// 描画します
        /// </summary>
        public override void Draw(Graphics vGraphics) {
            using (var wBrush = new SolidBrush(this.Color))
            using (var wPen = new Pen(C_BorderColor, 2f)) {
                vGraphics.FillPath(wBrush, this.MainPath);
                vGraphics.DrawPath(wPen, this.MainPath);
            }
            if (!this.IsSelected) return;
            this.DrawFrame(vGraphics);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Ellipse();

        #endregion　メソッド

    }
}
