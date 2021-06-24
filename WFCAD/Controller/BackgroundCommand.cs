using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class BackgroundCommand : EditCommand {
        public BackgroundCommand(Canvas vCanvas, IEnumerable<IShape> vShapes) : base(vCanvas, vShapes) { }

        public override void Execute() {
            foreach ((IShape wShape, int wIndex) in FShapes.OrderByDescending(x => x.Index)) {
                FCanvas.Shapes.RemoveAt(wIndex);
            }
            foreach ((IShape wShape, int wIndex) in FShapes.OrderByDescending(x => x.Index)) {
                FCanvas.Shapes.Insert(0, wShape);
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach ((IShape wShape, int wIndex) in FShapes) {
                FCanvas.Shapes.RemoveAt(0);
            }
            foreach ((IShape wShape, int wIndex) in FShapes.OrderBy(x => x.Index)) {
                FCanvas.Shapes.Insert(wIndex, wShape);
            }
            FCanvas.Draw();
        }
    }
}
