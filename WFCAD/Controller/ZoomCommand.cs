using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class ZoomCommand : PreviewCommand {
        public ZoomCommand(Canvas vCanvas,  Point vStartPoint, Point vEndPoint) : base(vCanvas, vStartPoint, vEndPoint) { }
        public override void Execute() => FCanvas.Zoom(FStartPoint, FEndPoint);
        public override void Undo() { }
    }
}
