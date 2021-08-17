using System.Collections.Generic;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class AddShape1DCommand : AddShapeCommand {
        public AddShape1DCommand(Canvas vCanvas) : base(vCanvas) { }
        protected override void SelectDefaultFramePoint(IEnumerable<IFramePoint> vFramePoints)
            => vFramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.End).IsSelected = true;
    }
}
