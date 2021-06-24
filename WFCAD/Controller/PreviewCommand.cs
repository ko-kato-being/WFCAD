using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class PreviewCommand : Command {
        protected readonly List<IShape> FShapes;
        protected readonly Point FStartPoint;
        protected readonly Point FEndPoint;
        public PreviewCommand(Canvas vCanvas, IEnumerable<IShape> vShapes, Point vStartPoint, Point vEndPoint) : base(vCanvas) {
            FShapes = vShapes.ToList();
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
        }
    }
}
