using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class MoveCommand : Command {
        private readonly List<IShape> FShapes;
        private readonly Point FStartPoint;
        private readonly Point FEndPoint;
        public MoveCommand(Canvas vCanvas, Point vStartPoint, Point vEndPoint) : base(vCanvas) {
            FShapes = FCanvas.Shapes.Where(x => x.IsSelected).ToList();
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
        }
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
