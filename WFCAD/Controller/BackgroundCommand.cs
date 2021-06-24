﻿using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class BackgroundCommand : Command {
        private readonly List<(IShape Shape, int Index)> FShapes = new List<(IShape Shape, int Index)>();
        public BackgroundCommand(Canvas vCanvas) : base(vCanvas) {
            for (int i = 0; i < FCanvas.Shapes.Count; i++) {
                if (!FCanvas.Shapes[i].IsSelected) continue;
                FShapes.Add((FCanvas.Shapes[i], i));
            }
        }
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