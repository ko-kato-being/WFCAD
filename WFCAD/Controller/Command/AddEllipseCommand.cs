using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class AddEllipseCommand : AddShape2DCommand {
        public AddEllipseCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            base.SetParams(vStartPoint, vEndPoint, vColor);
            FShape = new Ellipse(FStartPoint, FEndPoint, FColor);
        }
        public override IAddShapeCommand Clone() => new AddEllipseCommand(FCanvas);
    }
}
