using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// 線クラス
    /// </summary>
    public class Line : Shape1D {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Line(PointF vStartPoint, PointF vEndPoint, Color vColor) : base(vStartPoint, vEndPoint, vColor) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Line() { }

        #region　メソッド

        /// <summary>
        /// パスを初期化します
        /// </summary>
        protected override void InitializePath(PointF vStartPoint, PointF vEndPoint) {
            this.MainPath.AddLine(vStartPoint, vEndPoint);
            this.SubPath.AddLine(vStartPoint, vEndPoint);
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override Shape DeepCloneCore() => new Line();

        #endregion　メソッド
    }
}
