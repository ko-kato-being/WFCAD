using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 2次元図形クラス
    /// </summary>
    public abstract class Shape2D : Shape {

        /// <summary>
        /// 次元数
        /// </summary>
        public override int Dimensionality => 2;

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public override void SetPoints(PointF vStartPoint, PointF vEndPoint) {
            // 引数で受け取った始点と終点を対角線とする矩形に対して、
            // 左上の点と右下の点を始点と終点に設定します。
            this.StartPoint = new PointF(Math.Min(vStartPoint.X, vEndPoint.X), Math.Min(vStartPoint.Y, vEndPoint.Y));
            this.EndPoint = new PointF(Math.Max(vStartPoint.X, vEndPoint.X), Math.Max(vStartPoint.Y, vEndPoint.Y));

            // 枠点の座標
            var wTopLeft = this.StartPoint;
            var wTop = new PointF(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.StartPoint.Y);
            var wTopRight = new PointF(this.EndPoint.X, this.StartPoint.Y);
            var wLeft = new PointF(this.StartPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wRight = new PointF(this.EndPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wBottomLeft = new PointF(this.StartPoint.X, this.EndPoint.Y);
            var wBottom = new PointF(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.EndPoint.Y);
            var wBottomRight = this.EndPoint;

            // 枠点と基準点の設定
            this.FramePoints = new List<IFramePoint> {
                new FramePoint(wTopLeft, FramePointLocationKindEnum.TopLeft, wBottomRight, -1, -1),
                new FramePoint(wTop, FramePointLocationKindEnum.Top, wBottom, 0, -1),
                new FramePoint(wTopRight, FramePointLocationKindEnum.TopRight, wBottomLeft, 1, -1),
                new FramePoint(wLeft, FramePointLocationKindEnum.Left, wRight, -1, 0),
                new FramePoint(wRight, FramePointLocationKindEnum.Right, wLeft, 1, 0),
                new FramePoint(wBottomLeft, FramePointLocationKindEnum.BottomLeft, wTopRight, -1, 1),
                new FramePoint(wBottom, FramePointLocationKindEnum.Bottom, wTop, 0, 1),
                new FramePoint(wBottomRight, FramePointLocationKindEnum.BottomRight, wTopLeft, 1, 1),
            };
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        public override void DrawFrame(Graphics vGraphics) {
            using (var wPen = new Pen(C_FrameColor)) {
                vGraphics.DrawPath(wPen, this.SubPath);
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen);
                }
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(PointF vCoordinate) => this.MainPath.IsVisible(vCoordinate.X, vCoordinate.Y);
    }
}
