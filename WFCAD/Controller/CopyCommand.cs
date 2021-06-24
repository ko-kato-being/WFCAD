using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class CopyCommand : Command {
        private readonly List<IShape> FShapes;
        public CopyCommand(Canvas vCanvas) : base(vCanvas) {
            FShapes = FCanvas.Shapes.Where(x => x.IsSelected).Select(x => x.DeepClone()).ToList();
            foreach (IShape wShape in FShapes) {
                wShape.Move(new SizeF(10, 10));
            }
        }
        public override void Execute() => FCanvas.Clipboad = FShapes;
        public override void Undo() { }
    }
}
