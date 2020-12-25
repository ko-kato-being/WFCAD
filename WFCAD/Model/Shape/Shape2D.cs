using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model.Frame;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 2次元図形クラス
    /// </summary>
    public abstract class Shape2D : Shape {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Shape2D(Color vColor) : base(vColor) { }

        /// <summary>
        /// 次元数
        /// </summary>
        public override int Dimensionality => 2;

        /// <summary>
        /// 外枠
        /// </summary>
        public System.Drawing.Rectangle FrameRectangle => new System.Drawing.Rectangle(this.StartPoint.X, this.StartPoint.Y, this.Width, this.Height);

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public override void SetPoints(Point vStartPoint, Point vEndPoint) {
            // 引数で受け取った始点と終点を対角線とする矩形に対して、
            // 左上の点と右下の点を始点と終点に設定します。
            this.StartPoint = new Point(Math.Min(vStartPoint.X, vEndPoint.X), Math.Min(vStartPoint.Y, vEndPoint.Y));
            this.EndPoint = new Point(Math.Max(vStartPoint.X, vEndPoint.X), Math.Max(vStartPoint.Y, vEndPoint.Y));

            // 枠点の座標
            var wTopLeft = this.StartPoint;
            var wTop = new Point(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.StartPoint.Y);
            var wTopRight = new Point(this.EndPoint.X, this.StartPoint.Y);
            var wLeft = new Point(this.StartPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wRight = new Point(this.EndPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wBottomLeft = new Point(this.StartPoint.X, this.EndPoint.Y);
            var wBottom = new Point(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.EndPoint.Y);
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
        protected override void DrawFrame(Graphics vGraphics) {
            using (var wPen = new Pen(C_FrameColor)) {
                vGraphics.DrawRectangle(wPen, this.FrameRectangle);
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen);
                }
            }
        }

        /// <summary>
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected override (Point StartPoint, Point EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, Size vSize) {
            var wPoints = vFramePoint.BasePoints.ToList();
            Size wAdjustSize = vSize;
            if (wPoints.Count == 2) {
                // 基準点を結ぶ線分に対して水平方向には拡大・縮小できないように制限します
                if (wPoints[0].X == wPoints[1].X) {
                    wAdjustSize = new Size(vSize.Width, 0);
                } else if (wPoints[0].Y == wPoints[1].Y) {
                    wAdjustSize = new Size(0, vSize.Height);
                }
            }
            wPoints.Add(vFramePoint.Point + wAdjustSize);
            var wStartPoint = new Point(wPoints.Min(p => p.X), wPoints.Min(p => p.Y));
            var wEndPoint = new Point(wPoints.Max(p => p.X), wPoints.Max(p => p.Y));
            return (wStartPoint, wEndPoint);
        }
    }
}
