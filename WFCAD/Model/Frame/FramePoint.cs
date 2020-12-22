using System.Collections.Generic;
using System.Drawing;

namespace WFCAD.Model.Frame {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FramePoint(Point vPoint, params Point[] vBasePoints) {
            this.Point = vPoint;
            this.BasePoints = vBasePoints;
        }

        #region プロパティ

        /// <summary>
        /// 座標
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// 基準点
        /// </summary>
        public IEnumerable<Point> BasePoints { get; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します。
        /// </summary>
        public void Draw(Graphics vGraphics, Pen vPen) {
            const int C_Radius = 5;
            var wTopLeft = new Point(this.Point.X - C_Radius, this.Point.Y - C_Radius);
            var wBottomRight = new Point(this.Point.X + C_Radius, this.Point.Y + C_Radius);

            var wFrameRectangle = new Rectangle(wTopLeft.X, wTopLeft.Y, wBottomRight.X - wTopLeft.X, wBottomRight.Y - wTopLeft.Y);
            vGraphics.DrawEllipse(vPen, wFrameRectangle);
        }

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        public bool IsHit(Point vCoordinate) {
            return false;
        }

        #endregion メソッド

    }
}
