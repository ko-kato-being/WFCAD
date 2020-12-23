using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

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
            this.BasePoints = vBasePoints.ToList();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FramePoint(Point vPoint, IEnumerable<Point> vBasePoints, bool vIsSelected) {
            this.Point = vPoint;
            this.BasePoints = vBasePoints.ToList();
            this.IsSelected = vIsSelected;
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
            // 円の半径
            const int C_Radius = 4;
            var wTopLeft = new Point(this.Point.X - C_Radius, this.Point.Y - C_Radius);

            FFrameRectangle = new Rectangle(wTopLeft.X, wTopLeft.Y, C_Radius * 2, C_Radius * 2);
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

        /// <summary>
        /// 複製します
        /// </summary>
        public IFramePoint DeepClone() => new FramePoint(this.Point, this.BasePoints, this.IsSelected);

        #endregion メソッド

    }
}
