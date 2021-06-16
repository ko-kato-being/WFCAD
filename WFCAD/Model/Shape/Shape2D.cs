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
        /// コンストラクタ
        /// </summary>
        public Shape2D(PointF vStartPoint, PointF vEndPoint, Color vColor) : base(vStartPoint, vEndPoint, vColor) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Shape2D() { }

        /// <summary>
        /// 次元数
        /// </summary>
        public override int Dimensionality => 2;

        /// <summary>
        /// 枠を生成します
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
                new FramePoint(wTopLeft, FramePointLocationKindEnum.Top, wBottomRight),
                new FramePoint(wTop, FramePointLocationKindEnum.TopRight, wBottom, vScalingX:false),
                new FramePoint(wTopRight, FramePointLocationKindEnum.Right, wBottomLeft),
                new FramePoint(wRight, FramePointLocationKindEnum.RightBottom, wLeft, vScalingY:false),
                new FramePoint(wBottomRight, FramePointLocationKindEnum.Bottom, wTopLeft),
                new FramePoint(wBottom, FramePointLocationKindEnum.BottomLeft, wTop, vScalingX:false),
                new FramePoint(wBottomLeft, FramePointLocationKindEnum.Left, wTopRight),
                new FramePoint(wLeft, FramePointLocationKindEnum.LeftTop, wRight, vScalingY:false),
            };
        }

        /// <summary>
        /// 点を設定します
        /// </summary>
        protected override void SetPoints(PointF vStartPoint, PointF vEndPoint) {
            PointF wCenterPoint = vStartPoint;
            if (vStartPoint == vEndPoint) {
            } else {
                float wCenterX = this.SubPath.PathPoints.Select(x => x.X).Sum() / 4f;
                float wCenterY = this.SubPath.PathPoints.Select(x => x.Y).Sum() / 4f;
                wCenterPoint = new PointF(wCenterX, wCenterY);
            }
            this.Points = new PointF[1] {
                wCenterPoint,
            };
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        protected override void DrawFrame(Graphics vGraphics) {
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
