using System.Drawing;

namespace WFCAD.Controller {
    public interface IAddShapeCommand {
        Point StartPoint { get; }
        Point EndPoint { get; }
        Color Color { get; }
        void Execute();
        void SetParams(Point vStartPoint, Point vEndPoint, Color vColor);
    }
}
