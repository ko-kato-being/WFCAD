using System.Drawing;
using WFCAD.Model;
using Rectangle = WFCAD.Model.Rectangle;

namespace WFCAD.Controller {
    public class AddRectangleCommand : Command, IAddShapeCommand {

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Color Color { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddRectangleCommand(Canvas vCanvas) : base(vCanvas) { }

        public override void Execute() => FCanvas.Add(new Rectangle(this.StartPoint, this.EndPoint, this.Color));

        public void SetParams(Point vStartPoint, Point vEndPoint, Color vColor) {
            this.StartPoint = vStartPoint;
            this.EndPoint = vEndPoint;
            this.Color = vColor;
        }
    }
}
