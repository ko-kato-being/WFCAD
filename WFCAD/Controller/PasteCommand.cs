using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class PasteCommand : Command {
        private List<IShape> FShapes;
        public PasteCommand(Canvas vCanvas) : base(vCanvas) { }
        public override void Execute() {
            FShapes = FCanvas.Shapes.Where(x => x.IsSelected).ToList();
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = false;
            }
            FCanvas.Shapes.AddRange(FCanvas.Clipboad.Select(x => x.DeepClone()));
            foreach (IShape wShape in FCanvas.Clipboad) {
                wShape.Move(new SizeF(10, 10));
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach (IShape wShape in FShapes) {
                wShape.IsSelected = true;
            }
            for (int i = 0; i < FCanvas.Clipboad.Count; i++) {
                FCanvas.Shapes.RemoveAt(FCanvas.Shapes.Count - 1);
            }
            foreach (IShape wShape in FCanvas.Clipboad) {
                wShape.Move(new SizeF(-10, -10));
            }
            FCanvas.Draw();
        }
    }
}
