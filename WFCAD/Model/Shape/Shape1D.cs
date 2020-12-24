using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model.Frame;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 1次元図形クラス
    /// </summary>
    public abstract class Shape1D : Shape, IShape1D {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Shape1D(Color vColor) : base(vColor) { }

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public override void SetPoints(Point vStartPoint, Point vEndPoint) {
            // 左に位置する点を始点にします
            bool wIsLeft = vStartPoint.X < vEndPoint.X;
            this.StartPoint = wIsLeft ? vStartPoint : vEndPoint;
            this.EndPoint = wIsLeft ? vEndPoint : vStartPoint;

            this.FramePoints = new List<IFramePoint> {
                new FramePoint(this.StartPoint, FramePointLocationKindEnum.Start, this.EndPoint),
                new FramePoint(this.EndPoint, FramePointLocationKindEnum.End, this.StartPoint),
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
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected override (Point StartPoint, Point EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, Size vSize)
            => (vFramePoint.Point + vSize, vFramePoint.BasePoints.First());
    }
}
