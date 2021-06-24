using System;
using System.Linq;
using System.Collections.Generic;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class ResetCommand : Command {
        protected readonly List<IShape> FShapes;
        public ResetCommand(Canvas vCanvas) : base(vCanvas) => FShapes = FCanvas.Shapes.Select(x => x.DeepClone()).ToList();

        public override void Execute() {
            FCanvas.Shapes.Clear();
            FCanvas.Draw();
        }
        public override void Undo() {
            FCanvas.Shapes.AddRange(FShapes);
            FCanvas.Draw();
        }
    }
}
