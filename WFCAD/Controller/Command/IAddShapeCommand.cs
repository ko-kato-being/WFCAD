using System.Drawing;

namespace WFCAD.Controller {
    public interface IAddShapeCommand : ICommand {
        void SetParams(Point vStartPoint, Point vEndPoint, Color vColor);
        IAddShapeCommand Clone();
    }
}
