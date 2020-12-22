using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle : Shape {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Rectangle(Color vColor) : base(vColor) { }

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        protected override void DrawCore(Graphics vGraphics) {
            using (var wPen = new Pen(this.Color)) {
                vGraphics.DrawRectangle(wPen, this.FrameRectangle);
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(Point vCoordinate) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddRectangle(this.FrameRectangle);
                return wPath.IsVisible(vCoordinate.X, vCoordinate.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override IShape DeepCloneCore() => new Rectangle(FPrevColor);

        #endregion　メソッド

    }
}
