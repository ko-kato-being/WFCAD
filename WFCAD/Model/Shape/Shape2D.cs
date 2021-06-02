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
        /// コンストラクタ
        /// </summary>
        public Shape2D(PointF vStartPoint, PointF vEndPoint, Color vColor) : base(vStartPoint, vEndPoint, vColor) {
            this.InitializePath(vStartPoint, vEndPoint);
            this.CreateFrame(vStartPoint, vEndPoint);
            float wCenterX = this.SubPath.PathPoints.Select(x => x.X).Sum() / 4f;
            float wCenterY = this.SubPath.PathPoints.Select(x => x.Y).Sum() / 4f;
            FPoints = new PointF[1] {
                new PointF(wCenterX, wCenterY)
            };
        }

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        protected override void CreateFrame(PointF vStartPoint, PointF vEndPoint) {
            // 引数で受け取った始点と終点を対角線とする矩形に対して、
            // 左上の点と右下の点を始点と終点に設定します。
            var wStartPoint = new PointF(Math.Min(vStartPoint.X, vEndPoint.X), Math.Min(vStartPoint.Y, vEndPoint.Y));
            var wEndPoint = new PointF(Math.Max(vStartPoint.X, vEndPoint.X), Math.Max(vStartPoint.Y, vEndPoint.Y));

            // 枠点の座標
            var wTopLeft = wStartPoint;
            var wTop = new PointF(wStartPoint.X + (wEndPoint.X - wStartPoint.X) / 2, wStartPoint.Y);
            var wTopRight = new PointF(wEndPoint.X, wStartPoint.Y);
            var wLeft = new PointF(wStartPoint.X, wStartPoint.Y + (wEndPoint.Y - wStartPoint.Y) / 2);
            var wRight = new PointF(wEndPoint.X, wStartPoint.Y + (wEndPoint.Y - wStartPoint.Y) / 2);
            var wBottomLeft = new PointF(wStartPoint.X, wEndPoint.Y);
            var wBottom = new PointF(wStartPoint.X + (wEndPoint.X - wStartPoint.X) / 2, wEndPoint.Y);
            var wBottomRight = wEndPoint;

            // 枠点と基準点の設定
            this.FramePoints = new List<IFramePoint> {
                new FramePoint(wTopLeft, FramePointLocationKindEnum.TopLeft, wBottomRight),
                new FramePoint(wTop, FramePointLocationKindEnum.Top, wBottom, vScalingX:false),
                new FramePoint(wTopRight, FramePointLocationKindEnum.TopRight, wBottomLeft),
                new FramePoint(wLeft, FramePointLocationKindEnum.Left, wRight, vScalingY:false),
                new FramePoint(wRight, FramePointLocationKindEnum.Right, wLeft, vScalingY:false),
                new FramePoint(wBottomLeft, FramePointLocationKindEnum.BottomLeft, wTopRight),
                new FramePoint(wBottom, FramePointLocationKindEnum.Bottom, wTop, vScalingX:false),
                new FramePoint(wBottomRight, FramePointLocationKindEnum.BottomRight, wTopLeft),
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
