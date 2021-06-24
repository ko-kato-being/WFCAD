﻿using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class RemoveCommand : EditCommand {
        public RemoveCommand(Canvas vCanvas, IEnumerable<IShape> vShapes) : base(vCanvas, vShapes) { }
        public override void Execute() {
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
