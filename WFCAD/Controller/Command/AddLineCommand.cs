using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class AddLineCommand : AddShape1DCommand {
        public AddLineCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            base.SetParams(vStartPoint, vEndPoint, vColor);
            FShape = new Line(FStartPoint, FEndPoint, FColor);
        }
        public override IAddShapeCommand Clone() => new AddLineCommand(FCanvas);
    }
}
