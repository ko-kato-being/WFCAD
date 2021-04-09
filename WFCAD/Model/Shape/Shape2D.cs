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
                new FramePoint(wTopLeft, FramePointLocationKindEnum.TopLeft, wBottomRight),
                new FramePoint(wTop, FramePointLocationKindEnum.Top, wBottomLeft, wBottomRight),
                new FramePoint(wTopRight, FramePointLocationKindEnum.TopRight, wBottomLeft),
                new FramePoint(wLeft, FramePointLocationKindEnum.Left, wTopRight, wBottomRight),
                new FramePoint(wRight, FramePointLocationKindEnum.Right, wTopLeft, wBottomLeft),
                new FramePoint(wBottomLeft, FramePointLocationKindEnum.BottomLeft, wTopRight),
                new FramePoint(wBottom, FramePointLocationKindEnum.Bottom, wTopLeft, wTopRight),
                new FramePoint(wBottomRight, FramePointLocationKindEnum.BottomRight, wTopLeft),
            };
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        public override void DrawFrame(Bitmap vBitmap, Graphics vGraphics) {
            using (var wPen = new Pen(C_FrameColor)) {
                vGraphics.DrawPath(wPen, this.SubPath);
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen, this.Matrix);
                }
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(PointF vCoordinate) => this.MainPath.IsVisible(vCoordinate.X, vCoordinate.Y);

        /// <summary>
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected override (PointF StartPoint, PointF EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, SizeF vSize) {
            var wPoints = vFramePoint.BasePoints.ToList();
            SizeF wAdjustSize = vSize;
            if (wPoints.Count == 2) {
                // 基準点を結ぶ線分に対して水平方向には拡大・縮小できないように制限します
                if (wPoints[0].X == wPoints[1].X) {
                    wAdjustSize = new SizeF(vSize.Width, 0);
                } else if (wPoints[0].Y == wPoints[1].Y) {
                    wAdjustSize = new SizeF(0, vSize.Height);
                }
            }
            wPoints.Add(vFramePoint.Point + wAdjustSize);
            var wStartPoint = new PointF(wPoints.Min(p => p.X), wPoints.Min(p => p.Y));
            var wEndPoint = new PointF(wPoints.Max(p => p.X), wPoints.Max(p => p.Y));
            return (wStartPoint, wEndPoint);
        }
    }
}
