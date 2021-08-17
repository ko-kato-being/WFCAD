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
        protected override void CreateFrame() {
            // 枠点と基準点の設定
            this.FramePoints = new List<IFramePoint> {
                new FramePoint(this.StartPoint, FramePointLocationKindEnum.Start, this.EndPoint),
                new FramePoint(this.EndPoint, FramePointLocationKindEnum.End, this.StartPoint)
            };
            foreach (IFramePoint wFrame in this.FramePoints) {
                wFrame.Selected += () => this.IsSelected = true;
            }
        }

        /// <summary>
        /// 端点を設定します
        /// </summary>
        protected override void SetBothEndsPoint(PointF vStartPoint, PointF vEndPoint) {
            this.Points = new PointF[3] {
                vStartPoint,
                vEndPoint,
                PointF.Empty, // 中心点は後で設定する
            };
        }

        /// <summary>
        /// 中心点を設定します
        /// </summary>
        protected override void SetCenterPoint()
            => this.Points[2] = new PointF((this.StartPoint.X + this.EndPoint.X) / 2f, (this.StartPoint.Y + this.EndPoint.Y) / 2f);

        /// <summary>
        /// 描画のコア処理
        /// </summary>
        protected override void DrawCore(Graphics vGraphics) {
            using (var wPen = new Pen(this.Color, 3f)) {
                vGraphics.DrawPath(wPen, this.MainPath);
            }
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
