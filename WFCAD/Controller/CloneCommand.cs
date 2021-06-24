using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class CloneCommand : Command {
        private readonly List<IShape> FBaseShapes;
        private readonly List<IShape> FShapes;
        public CloneCommand(Canvas vCanvas) : base(vCanvas) {
            FBaseShapes = FCanvas.Shapes.Where(x => x.IsSelected).ToList();
            FShapes = FBaseShapes.Select(x => x.DeepClone()).ToList();
            foreach (IShape wShape in FShapes) {
                wShape.Move(new SizeF(10, 10));
            }
        }

        public override void Execute() {
            foreach (IShape wShape in FBaseShapes) {
                wShape.IsSelected = false;
            }
            FCanvas.Shapes.AddRange(FShapes);
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach (IShape wShape in FBaseShapes) {
                wShape.IsSelected = true;
                FCanvas.Shapes.RemoveAt(FCanvas.Shapes.Count - 1);
            }
            FCanvas.Draw();
        }
    }
}
