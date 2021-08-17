using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 1次元図形クラス
    /// </summary>
    public abstract class Shape1D : Shape {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Shape1D(PointF vStartPoint, PointF vEndPoint, Color vColor) : base(vStartPoint, vEndPoint, vColor) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Shape1D() { }

        /// <summary>
        /// 次元数
        /// </summary>
        public override int Dimensionality => 1;

        /// <summary>
        /// 枠を生成します
        /// </summary>
        protected override void CreateFrame(PointF vStartPoint, PointF vEndPoint) {
            // 引数で受け取った始点と終点を対角線とする矩形に対して、
            // 左上の点と右下の点を始点と終点に設定します。
            var wStartPoint = new PointF(Math.Min(vStartPoint.X, vEndPoint.X), Math.Min(vStartPoint.Y, vEndPoint.Y));
            var wEndPoint = new PointF(Math.Max(vStartPoint.X, vEndPoint.X), Math.Max(vStartPoint.Y, vEndPoint.Y));

            // 枠点と基準点の設定
            this.FramePoints = new List<IFramePoint> {
                new FramePoint(wStartPoint, FramePointLocationKindEnum.Start, wEndPoint),
                new FramePoint(wEndPoint, FramePointLocationKindEnum.End, wStartPoint)
            };
            foreach (IFramePoint wFrame in this.FramePoints) {
                wFrame.Selected += () => this.IsSelected = true;
            }
        }

        /// <summary>
        /// 点を設定します
        /// </summary>
        protected override void SetPoints(PointF vStartPoint, PointF vEndPoint) {
            // 中心点を設定しておく
            this.Points = new PointF[1] {
                new PointF((vStartPoint.X + vEndPoint.X) / 2f, (vStartPoint.Y + vEndPoint.Y) / 2f),
            };
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        protected override void DrawFrame(Graphics vGraphics) {
            using (var wPen = new Pen(C_FrameColor)) {
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen);
                }
            }
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(PointF vCoordinate) {
            // 三角不等式を使用して判定しています。
            PointF wStartPoint = this.FramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.Start).MainPoint;
            PointF wEndPoint = this.FramePoints.Single(x => x.CurrentLocationKind == FramePointLocationKindEnum.End).MainPoint;
            double wAC = Utilities.GetDistance(wStartPoint, vCoordinate);
            double wCB = Utilities.GetDistance(vCoordinate, wEndPoint);
            double wAB = Utilities.GetDistance(wStartPoint, wEndPoint);
            // 誤差以内の値なら線分上にあるとする。
            return (wAC + wCB - wAB < 0.1d);
        }


        /// <summary>
        /// 現在の位置種類を設定します
        /// </summary>
        protected override void SetLocation() {
            // TODO:実装
        }
    }
}
