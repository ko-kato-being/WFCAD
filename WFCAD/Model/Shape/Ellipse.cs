using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD {
    /// <summary>
    /// 楕円クラス
    /// </summary>
    public class Ellipse : Shape {
        private System.Drawing.Rectangle FFrameRectangle;

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        protected override void DrawCore(Graphics vGraphics) {
            FFrameRectangle = new System.Drawing.Rectangle(Math.Min(this.StartPoint.X, this.EndPoint.X), Math.Min(this.StartPoint.Y, this.EndPoint.Y), this.Width, this.Height);
            using (var wPen = new Pen(this.Color)) {
                vGraphics.DrawEllipse(wPen, FFrameRectangle);
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(Point vCoordinate) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddEllipse(FFrameRectangle);
                return wPath.IsVisible(vCoordinate.X, vCoordinate.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override IShape DeepCloneCore() => new Ellipse();


        #endregion　メソッド

    }
}
