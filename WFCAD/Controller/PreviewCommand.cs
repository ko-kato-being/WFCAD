using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class PreviewCommand : Command {
        protected readonly Point FStartPoint;
        protected readonly Point FEndPoint;
        public PreviewCommand(Canvas vCanvas, Point vStartPoint, Point vEndPoint) : base(vCanvas) {
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
        }
    }
}
