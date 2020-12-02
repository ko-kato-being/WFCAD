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
        public override void Draw(Graphics vGraphics) {
            FFrameRectangle = new System.Drawing.Rectangle(Math.Min(this.StartPoint.X, this.EndPoint.X), Math.Min(this.StartPoint.Y, this.EndPoint.Y), this.Width, this.Height);
            vGraphics.DrawEllipse(this.Option, FFrameRectangle);
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(Point vMouseLocation) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddEllipse(FFrameRectangle);
                return wPath.IsVisible(vMouseLocation.X, vMouseLocation.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override IShape DeepCloneCore() => new Ellipse();


        #endregion　メソッド

    }
}
