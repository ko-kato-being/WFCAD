using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class MoveCommand : PreviewCommand {
        public MoveCommand(Canvas vCanvas, Point vStartPoint, Point vEndPoint) : base(vCanvas, vStartPoint, vEndPoint) { }
        public override void Execute() => FCanvas.Move(FStartPoint, FEndPoint);
        public override void Undo() { }
    }
}
