using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, params PointF[] vBasePoints) {
            this.Point = vPoint;
            this.LocationKind = vLocationKind;
            this.BasePoints = vBasePoints.ToList();

            // 円の半径
            const int C_Radius = 4;
            var wTopLeft = new PointF(this.Point.X - C_Radius, this.Point.Y - C_Radius);
            this.Path.AddEllipse(wTopLeft.X, wTopLeft.Y, C_Radius * 2, C_Radius * 2);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, IEnumerable<PointF> vBasePoints, bool vIsSelected) {
            this.Point = vPoint;
            this.LocationKind = vLocationKind;
            this.BasePoints = vBasePoints.ToList();
            this.IsSelected = vIsSelected;

            // 円の半径
            const int C_Radius = 4;
            var wTopLeft = new PointF(this.Point.X - C_Radius, this.Point.Y - C_Radius);
            this.Path.AddEllipse(wTopLeft.X, wTopLeft.Y, C_Radius * 2, C_Radius * 2);
        }

        #region プロパティ

        /// <summary>
        /// パス
        /// </summary>
        public GraphicsPath Path { get; } = new GraphicsPath();

        /// <summary>
        /// 座標
        /// </summary>
        public PointF Point { get; }

        /// <summary>
        /// 位置種類
        /// </summary>
        public FramePointLocationKindEnum LocationKind { get; }

        /// <summary>
        /// 基準点
        /// </summary>
        public IEnumerable<PointF> BasePoints { get; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します。
        /// </summary>
        public void Draw(Graphics vGraphics, Pen vPen, Matrix vMatrix) {
            using (var wBrush = new SolidBrush(Color.White)) {
                vGraphics.FillPath(wBrush, this.Path);
                vGraphics.DrawPath(vPen, this.Path);
            }
        }

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        public bool IsHit(PointF vCoordinate) => this.Path.IsVisible(vCoordinate.X, vCoordinate.Y);

        /// <summary>
        /// 複製します
        /// </summary>
        public IFramePoint DeepClone() => new FramePoint(this.Point, this.LocationKind, this.BasePoints, this.IsSelected);

        #endregion メソッド

    }
}
