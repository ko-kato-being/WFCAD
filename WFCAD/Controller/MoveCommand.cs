using System.Collections.Generic;
using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class MoveCommand : PreviewCommand {
        public MoveCommand(Canvas vCanvas, IEnumerable<IShape> vShapes, Point vStartPoint, Point vEndPoint) : base(vCanvas, vShapes, vStartPoint, vEndPoint) { }
        public override void Execute() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
                wShape.Move(FStartPoint, FEndPoint);
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
                wShape.Move(FEndPoint, FStartPoint);
            }
            FCanvas.Draw();
        }
    }
}
