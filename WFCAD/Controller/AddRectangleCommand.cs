using WFCAD.Model;
using Rectangle = WFCAD.Model.Rectangle;

namespace WFCAD.Controller {
    public class AddRectangleCommand : AddShapeCommand {
        public AddRectangleCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() {
            FShape = new Rectangle(FStartPoint, FEndPoint, FColor);
            FCanvas.Add(FShape);
        }
        public override void Undo() { }
        public override IAddShapeCommand Clone() => new AddRectangleCommand(FCanvas);
    }
}
