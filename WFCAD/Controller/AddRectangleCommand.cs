using System.Drawing;
using WFCAD.Model;
using Rectangle = WFCAD.Model.Rectangle;

namespace WFCAD.Controller {
    public class AddRectangleCommand : IAddShapeCommand {
        private readonly Canvas FCanvas;
        private Point FStartPoint;
        private Point FEndPoint;
        private Color FColor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddRectangleCommand(Canvas vCanvas) {
            FCanvas = vCanvas;
        }

        public void Execute() => FCanvas.Add(new Rectangle(FStartPoint, FEndPoint, FColor));

        public void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            FStartPoint = vStartPoint;
            FEndPoint = vEndPoint;
            FColor = vColor;
        }
    }
}
