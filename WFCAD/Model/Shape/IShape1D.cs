using System.Drawing;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 1次元図形を表すインターフェース
    /// </summary>
    public interface IShape1D : IShape {
        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        new void SetPoints(Point vStartPoint, Point vEndPoint);
    }
}
