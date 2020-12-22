using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model.Frame {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {
        private Rectangle FFrameRectangle;

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
            const int C_Radius = 4;
            var wTopLeft = new Point(this.Point.X - C_Radius, this.Point.Y - C_Radius);
            var wBottomRight = new Point(this.Point.X + C_Radius, this.Point.Y + C_Radius);

            FFrameRectangle = new Rectangle(wTopLeft.X, wTopLeft.Y, wBottomRight.X - wTopLeft.X, wBottomRight.Y - wTopLeft.Y);
            using (var wBrush = new SolidBrush(Color.White)) {
                vGraphics.FillEllipse(wBrush, FFrameRectangle);
            }
            vGraphics.DrawEllipse(vPen, FFrameRectangle);
        }

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        public bool IsHit(Point vCoordinate) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddEllipse(FFrameRectangle);
                return wPath.IsVisible(vCoordinate.X, vCoordinate.Y);
            }
        }

        #endregion メソッド

    }
}
