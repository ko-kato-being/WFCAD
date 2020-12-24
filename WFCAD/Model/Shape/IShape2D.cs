using System.Drawing;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 2次元図形を表すインターフェース
    /// </summary>
    public interface IShape2D : IShape {
        /// <summary>
        /// 外枠
        /// </summary>
        System.Drawing.Rectangle FrameRectangle { get; }

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        new void SetPoints(Point vStartPoint, Point vEndPoint);
    }
}
