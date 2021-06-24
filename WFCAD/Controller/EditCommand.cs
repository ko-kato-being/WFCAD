using System;
using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class EditCommand : Command {
        protected readonly List<(IShape Shape, int Index)> FShapes = new List<(IShape Shape, int Index)>();
        public EditCommand(Canvas vCanvas, IEnumerable<IShape> vShapes) : base(vCanvas) {
            for (int i = 0; i < FCanvas.Shapes.Count; i++) {
                if (!vShapes.Contains(FCanvas.Shapes[i])) continue;
                FShapes.Add((FCanvas.Shapes[i], i));
            }
        }
    }
}
