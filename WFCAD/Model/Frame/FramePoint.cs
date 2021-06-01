using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {
        private readonly int FXDirection;
        private readonly int FYDirection;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, PointF vOppositePoint, int vXDirection, int vYDirection) {
            this.MainPoint = vPoint;
            this.LocationKind = vLocationKind;
            this.OppositePoint = vOppositePoint;
            FXDirection = vXDirection;
            FYDirection = vYDirection;
            this.InitializePath();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, PointF vOppositePoint, bool vIsSelected) {
            this.MainPoint = vPoint;
            this.LocationKind = vLocationKind;
            this.OppositePoint = vOppositePoint;
            this.IsSelected = vIsSelected;
            this.InitializePath();
        }

        #region プロパティ

        /// <summary>
        /// パス
        /// </summary>
        public GraphicsPath Path { get; } = new GraphicsPath();

        /// <summary>
        /// 座標
        /// </summary>
        public PointF MainPoint { get; private set; }

        /// <summary>
        /// 基準点
        /// </summary>
        public PointF OppositePoint { get; private set; }

        /// <summary>
        /// 位置種類
        /// </summary>
        public FramePointLocationKindEnum LocationKind { get; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion プロパティ

        #region メソッド

        private void InitializePath() {
            this.Path.Reset();
            const int C_Radius = 5; // 円の半径
            var wTopLeft = new PointF(this.MainPoint.X - C_Radius, this.MainPoint.Y - C_Radius);
            this.Path.AddEllipse(wTopLeft.X, wTopLeft.Y, C_Radius * 2, C_Radius * 2);
        }

        /// <summary>
        /// 描画します。
        /// </summary>
        public void Draw(Graphics vGraphics, Pen vPen) {
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
        /// 指定した変換行列をすべての点に適用します
        /// </summary>
        public void TransformPoints(Matrix vMatrix) {
            var wAllPoints = new PointF[2];
            wAllPoints[0] = this.MainPoint;
            wAllPoints[1] = this.OppositePoint;
            vMatrix.TransformPoints(wAllPoints);
            this.MainPoint = wAllPoints[0];
            this.OppositePoint = wAllPoints[1];
            this.InitializePath();
        }

        /// <summary>
        /// 拡大時の倍率を取得します。
        /// </summary>
        public (float, float) GetScale(PointF vStartPoint, PointF vEndPoint) {
            float wScaleX = (vEndPoint.X - this.OppositePoint.X) / (vStartPoint.X - this.OppositePoint.X) * FXDirection;
            float wScaleY = (vEndPoint.Y - this.OppositePoint.Y) / (vStartPoint.Y - this.OppositePoint.Y) * FYDirection;
            if (wScaleX == 0) wScaleX = 1;
            if (wScaleY == 0) wScaleY = 1;
            return (wScaleX, wScaleY);
        }

        /// <summary>
        /// 複製します
        /// </summary>
        public IFramePoint DeepClone() => new FramePoint(this.MainPoint, this.LocationKind, this.OppositePoint, this.IsSelected);

        #endregion メソッド

    }
}
