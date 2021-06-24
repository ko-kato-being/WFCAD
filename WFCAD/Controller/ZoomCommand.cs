﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public class ZoomCommand : PreviewCommand {
        private readonly List<(IFramePoint FramePoint, IShape Parent)> FFramePoints;
        public ZoomCommand(Canvas vCanvas, IEnumerable<IShape> vShapes, Point vStartPoint, Point vEndPoint)
            : base(vCanvas, vShapes, vStartPoint, vEndPoint)
            => FFramePoints = FShapes.Select(x => (x.FramePoints.Single(y => y.IsSelected), x)).ToList();
        public override void Execute() {
            foreach ((IFramePoint wFramePoint, IShape wParent) in FFramePoints) {
                wFramePoint.IsSelected = true;
                wParent.Zoom(wFramePoint, FStartPoint, FEndPoint, FCanvas.IsPreviewing);
            }
            FCanvas.Draw();
        }
        public override void Undo() {
            foreach ((IFramePoint wFramePoint, IShape wParent) in FFramePoints) {
                wFramePoint.IsSelected = true;
                wParent.Zoom(wFramePoint, FEndPoint, FStartPoint, FCanvas.IsPreviewing);
            }
            FCanvas.Draw();
        }

    }
}
