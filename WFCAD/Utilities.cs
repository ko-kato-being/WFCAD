using System;
using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// ユーティリティクラス
    /// </summary>
    public static class Utilities {
        /// <summary>
        /// 二点間の座標の距離を取得します
        /// </summary>
        public static double GetDistance(Point vPoint1, Point vPoint2) {
            double wWidth = Math.Abs(vPoint1.X - vPoint2.X);
            double wHeight = Math.Abs(vPoint1.Y - vPoint2.Y);
            return Math.Sqrt(wWidth * wWidth + wHeight * wHeight);
        }

        /// <summary>
        /// 指定した座標を指定した点を中心にして指定した角度だけ回転させます
        /// </summary>
        public static Point RotatePoint(Point vTarget, Point vOrigin, double vRadians) {
            double wSin = Math.Sin(vRadians);
            double wCos = Math.Cos(vRadians);
            int wNewX = (int)((wCos * (vTarget.X - vOrigin.X)) + (wSin * (-1) * (vTarget.Y - vOrigin.Y)) + vOrigin.X);
            int wNewY = (int)((wSin * (vTarget.X - vOrigin.X)) + (wCos * (vTarget.Y - vOrigin.Y)) + vOrigin.Y);
            return new Point(wNewX, wNewY);
        }
    }
}
