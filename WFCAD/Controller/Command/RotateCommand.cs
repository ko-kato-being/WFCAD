using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class RotateCommand : Command {
        private readonly List<IShape> FShapes;
        private readonly float FAngle;
        public RotateCommand(Canvas vCanvas, float vAngle) : base(vCanvas) {
            FShapes = FCanvas.Shapes.Where(x => x.IsSelected).ToList();
            FAngle = vAngle;
        }
        public override void Execute() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
                wShape.Rotate(FAngle);
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
                wShape.Rotate(FAngle * -1);
            }
            FCanvas.Draw();
        }
    }
}
