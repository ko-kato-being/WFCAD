﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class AddShapeCommand : Command, IAddShapeCommand {
        protected Point FStartPoint;
        protected Point FEndPoint;
        protected Color FColor;
        protected IShape FShape;

        public AddShapeCommand(Canvas vCanvas) : base(vCanvas) { }
        public virtual void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
            FColor = vColor;
        }
        public override void Execute() {
            FShape.IsSelected = true;
            IShape wShape;
            if (FCanvas.IsPreviewing) {
                wShape = FShape.DeepClone();
                this.SelectDefaultFramePoint(wShape.FramePoints);
            } else {
                wShape = FShape;
            }
            FCanvas.Shapes.Add(wShape);
            FCanvas.Draw();
        }
        public override void Undo() {
            FCanvas.Shapes.Remove(FShape);
            FCanvas.Draw();
        }

        protected abstract void SelectDefaultFramePoint(IEnumerable<IFramePoint> vFramePoints);
        public abstract IAddShapeCommand Clone();
    }
}