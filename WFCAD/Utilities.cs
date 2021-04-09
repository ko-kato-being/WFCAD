using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD {
    /// <summary>
    /// ユーティリティクラス
    /// </summary>
    public static class Utilities {
        /// <summary>
        /// 二点間の座標の距離を取得します
        /// </summary>
        public static double GetDistance(PointF vPoint1, PointF vPoint2) {
            double wWidth = Math.Abs(vPoint1.X - vPoint2.X);
            double wHeight = Math.Abs(vPoint1.Y - vPoint2.Y);
            return Math.Sqrt(wWidth * wWidth + wHeight * wHeight);
        }

        /// <summary>
        /// 指定した座標を指定した点を中心にして右に90度回転させます
        /// </summary>
        public static PointF RotateRight90(PointF vTarget, PointF vOrigin) {
            int wSin = -1; // -π/2
            int wCos =  0; // -π/2
            float wNewX = vOrigin.X + (wCos * (vTarget.X - vOrigin.X)) + ((-1) * wSin * (vTarget.Y - vOrigin.Y));
            float wNewY = vOrigin.Y + (wSin * (vTarget.X - vOrigin.X)) + (       wCos * (vTarget.Y - vOrigin.Y));
            return new PointF(wNewX, wNewY);
        }

        /// <summary>
        /// 指定した点を中心に拡大縮小するアフィン変換行列の計算
        /// </summary>
        /// <param name="vMatrix">アフィン変換行列</param>
        /// <param name="vScale">拡大縮小の倍率</param>
        /// <param name="vCenter">拡大縮小の中心座標</param>
        public static void ScaleAt(this Matrix vMatrix, float vScaleX, float vScaleY, PointF vCenter) {
            // 原点へ移動
            vMatrix.Translate(-vCenter.X, -vCenter.Y, MatrixOrder.Append);

            // 拡大縮小
            vMatrix.Scale(vScaleX, vScaleY, MatrixOrder.Append);

            // 元へ戻す
            vMatrix.Translate(vCenter.X, vCenter.Y, MatrixOrder.Append);
        }
    }
}
