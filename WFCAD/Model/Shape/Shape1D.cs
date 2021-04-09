﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 1次元図形クラス
    /// </summary>
    public abstract class Shape1D : Shape {

        /// <summary>
        /// 次元数
        /// </summary>
        public override int Dimensionality => 1;

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public override void SetPoints(PointF vStartPoint, PointF vEndPoint) {
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
        public override void DrawFrame(Bitmap vBitmap, Graphics vGraphics) {
            using (var wPen = new Pen(C_FrameColor)) {
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen, this.Matrix);
                }
            }
        }

        /// <summary>
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected override (PointF StartPoint, PointF EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, SizeF vSize)
            => (vFramePoint.Point + vSize, vFramePoint.BasePoints.First());
    }
}
