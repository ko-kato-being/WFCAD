using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD {
    /// <summary>
    /// 線クラス
    /// </summary>
    public class Line : Shape {

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public override void Draw(Graphics vGraphics) => vGraphics.DrawLine(this.Option, this.StartPoint, this.EndPoint);

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(Point vMouseLocation) {
            // 三角不等式を使用して判定しています。
            double wAC = Utilities.GetDistance(this.StartPoint, vMouseLocation);
            double wCB = Utilities.GetDistance(vMouseLocation, this.EndPoint);
            double wAB = Utilities.GetDistance(this.StartPoint, this.EndPoint);
            // 誤差以内の値なら線分上にあるとする。
            return (wAC + wCB - wAB < 0.1d);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override IShape DeepCloneCore() => new Line();

        #endregion　メソッド
    }
}
