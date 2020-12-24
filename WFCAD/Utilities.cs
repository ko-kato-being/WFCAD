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
        /// 指定した座標を指定した点を中心にして右に90度回転させます
        /// </summary>
        public static Point RotateRight90(Point vTarget, Point vOrigin) {
            int wSin = -1; // -π/2
            int wCos =  0; // -π/2
            int wNewX = vOrigin.X + (wCos * (vTarget.X - vOrigin.X)) + ((-1) * wSin * (vTarget.Y - vOrigin.Y));
            int wNewY = vOrigin.Y + (wSin * (vTarget.X - vOrigin.X)) + (       wCos * (vTarget.Y - vOrigin.Y));
            return new Point(wNewX, wNewY);
        }
    }
}
