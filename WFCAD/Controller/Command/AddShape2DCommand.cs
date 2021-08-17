using System;
using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class AddShape2DCommand : AddShapeCommand {
        public AddShape2DCommand(Canvas vCanvas) : base(vCanvas) { }
        protected override void SelectDefaultFramePoint(IEnumerable<IFramePoint> vFramePoints)
            => vFramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.Bottom).IsSelected = true;
    }
}
