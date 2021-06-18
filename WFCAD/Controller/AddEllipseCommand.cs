using WFCAD.Model;

namespace WFCAD.Controller {
    public class AddEllipseCommand : AddShapeCommand {
        public AddEllipseCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() {
            FShape = new Ellipse(FStartPoint, FEndPoint, FColor);
            FCanvas.Add(FShape);
        }
        public override void Undo() { }
        public override IAddShapeCommand Clone() => new AddEllipseCommand(FCanvas);
    }
}
