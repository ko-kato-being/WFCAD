using System.Drawing;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle : Shape2D {

        #region　メソッド

        /// <summary>
        /// 初期化します
        /// </summary>
        public override void InitializePath(PointF vStartPoint, PointF vEndPoint) {
            var wRectangle = new RectangleF(vStartPoint.X, vStartPoint.Y, vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y);
            this.MainPath.AddRectangle(wRectangle);
            this.SubPath.AddRectangle(wRectangle);

            float wCenterX = this.SubPath.PathPoints.Select(x => x.X).Sum() / 4f;
            float wCenterY = this.SubPath.PathPoints.Select(x => x.Y).Sum() / 4f;
            FPoints = new PointF[1] {
                new PointF(wCenterX, wCenterY)
            };
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

        #endregion　メソッド

    }
}
