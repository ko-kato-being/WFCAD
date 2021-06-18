using WFCAD.Model;
using Rectangle = WFCAD.Model.Rectangle;

namespace WFCAD.Controller {
    public class AddRectangleCommand : AddShapeCommand {
        public AddRectangleCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() => FCanvas.Add(new Rectangle(FStartPoint, FEndPoint, FColor));
        public override IAddShapeCommand Clone() => new AddRectangleCommand(FCanvas);
    }
}
