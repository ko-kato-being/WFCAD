using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Ellipse : Shape2D {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Ellipse(PointF vStartPoint, PointF vEndPoint, Color vColor) : base(vStartPoint, vEndPoint, vColor) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Ellipse() { }

        #region　メソッド

        /// <summary>
        /// パスを初期化します
        /// </summary>
        protected override void InitializePath(PointF vStartPoint, PointF vEndPoint) {
            var wRectangle = new RectangleF(vStartPoint.X, vStartPoint.Y, vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y);
            this.MainPath.AddEllipse(wRectangle);
            this.SubPath.AddRectangle(wRectangle);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Ellipse();

        #endregion　メソッド

    }
}
