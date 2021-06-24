using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class CutCommand : Command {
        private readonly List<(IShape Shape, int Index)> FShapes = new List<(IShape Shape, int Index)>();
        public CutCommand(Canvas vCanvas) : base(vCanvas) {
            for (int i = 0; i < FCanvas.Shapes.Count; i++) {
                if (!FCanvas.Shapes[i].IsSelected) continue;
                FCanvas.Shapes[i].Move(new SizeF(10, 10));
                FShapes.Add((FCanvas.Shapes[i], i));
            }
        }
        public override void Execute() {
            FCanvas.Clipboad = FShapes.Select(x => x.Shape).ToList();
            foreach ((IShape wShape, int wIndex) in FShapes.OrderByDescending(x => x.Index)) {
                FCanvas.Shapes.RemoveAt(wIndex);
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach ((IShape wShape, int wIndex) in FShapes.OrderBy(x => x.Index)) {
                FCanvas.Shapes.Insert(wIndex, wShape);
            }
            FCanvas.Draw();
        }
    }
}
