using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 線クラス
    /// </summary>
    public class Line : Shape1D {

        #region　メソッド

        /// <summary>
        /// 初期化します
        /// </summary>
        public override void InitializePath(PointF vStartPoint, PointF vEndPoint) {

        }

        /// <summary>
        /// 描画します
        /// </summary>
        public override void Draw(Graphics vGraphics) {
            using (var wPen = new Pen(this.Color, 2f)) {
                vGraphics.DrawLine(wPen, this.StartPoint, this.EndPoint);
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(PointF vCoordinate) {
            // 三角不等式を使用して判定しています。
            double wAC = Utilities.GetDistance(this.StartPoint, vCoordinate);
            double wCB = Utilities.GetDistance(vCoordinate, this.EndPoint);
            double wAB = Utilities.GetDistance(this.StartPoint, this.EndPoint);
            // 誤差以内の値なら線分上にあるとする。
            return (wAC + wCB - wAB < 0.1d);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Line();

        #endregion　メソッド
    }
}
