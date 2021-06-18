using WFCAD.Model;

namespace WFCAD.Controller {
    public class AddEllipseCommand : AddShapeCommand {
        public AddEllipseCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() => FCanvas.Add(new Ellipse(FStartPoint, FEndPoint, FColor));
        public override IAddShapeCommand Clone() => new AddEllipseCommand(FCanvas);
    }
}
