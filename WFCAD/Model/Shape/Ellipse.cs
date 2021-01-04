using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 楕円クラス
    /// </summary>
    public class Ellipse : Shape2D {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Ellipse(Color vColor) : base(vColor) { }

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
        public override bool IsHit(Point vCoordinate) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddEllipse(this.FrameRectangle);
                return wPath.IsVisible(vCoordinate.X, vCoordinate.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Ellipse(this.Color);


        #endregion　メソッド

    }
}
