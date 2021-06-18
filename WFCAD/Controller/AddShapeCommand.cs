using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class AddShapeCommand : IAddShapeCommand {
        protected readonly Canvas FCanvas;
        protected Point FStartPoint;
        protected Point FEndPoint;
        protected Color FColor;
        protected IShape FShape;

        public AddShapeCommand(Canvas vCanvas) => FCanvas = vCanvas;
        public abstract void Execute();
        public abstract void Undo();
        public void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
            FColor = vColor;
        }
        public abstract IAddShapeCommand Clone();
    }
}
