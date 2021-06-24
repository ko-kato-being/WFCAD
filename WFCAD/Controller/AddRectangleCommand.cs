using System.Drawing;
using WFCAD.Model;
using Rectangle = WFCAD.Model.Rectangle;

namespace WFCAD.Controller {
    public class AddRectangleCommand : AddShapeCommand {
        public AddRectangleCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() {
            FCanvas.Add(FShape);
        }
        public override void Undo() { }
        public override void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            base.SetParams(vStartPoint, vEndPoint, vColor);
            FShape = new Rectangle(FStartPoint, FEndPoint, FColor);
        }
        public override IAddShapeCommand Clone() => new AddRectangleCommand(FCanvas);
    }
}
