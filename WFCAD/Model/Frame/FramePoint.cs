using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {
        private PointF[] FPoints;
        private readonly bool FScalingX;
        private readonly bool FScalingY;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, PointF vOppositePoint, bool vScalingX = true, bool vScalingY = true) {
            FPoints = new PointF[2] {
                vPoint,
                vOppositePoint
            };
            this.LocationKind = vLocationKind;
            FScalingX = vScalingX;
            FScalingY = vScalingY;
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
        public PointF MainPoint => FPoints[0];

        /// <summary>
        /// 対極の座標
        /// </summary>
        public PointF OppositePoint => FPoints[1];

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
            const int C_Radius = 6; // 円の半径
            var wTopLeft = new PointF(this.MainPoint.X - C_Radius, this.MainPoint.Y - C_Radius);
            this.Path.AddEllipse(wTopLeft.X, wTopLeft.Y, C_Radius * 2, C_Radius * 2);
        }

        /// <summary>
        /// 描画します。
        /// </summary>
        public void Draw(Graphics vGraphics, Pen vPen) {
            Color wColor = Color.White;
            
            #region デバッグ用
            switch (this.LocationKind) {
                case FramePointLocationKindEnum.TopLeft:
                    wColor = Color.Red;
                    break;
                case FramePointLocationKindEnum.Top:
                    wColor = Color.Orange;
                    break;
                case FramePointLocationKindEnum.TopRight:
                    wColor = Color.Yellow;
                    break;
                case FramePointLocationKindEnum.Right:
                    wColor = Color.GreenYellow;
                    break;
                case FramePointLocationKindEnum.BottomRight:
                    wColor = Color.Green;
                    break;
                case FramePointLocationKindEnum.Bottom:
                    wColor = Color.LightSkyBlue;
                    break;
                case FramePointLocationKindEnum.BottomLeft:
                    wColor = Color.Blue;
                    break;
                case FramePointLocationKindEnum.Left:
                    wColor = Color.Purple;
                    break;
            }
            #endregion デバッグ用

            using (var wBrush = new SolidBrush(wColor)) {
                vGraphics.FillPath(wBrush, this.Path);
                vGraphics.DrawPath(vPen, this.Path);
            }
        }

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        public bool IsHit(PointF vCoordinate) => this.Path.IsVisible(vCoordinate.X, vCoordinate.Y);

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        public void ApplyAffine(Matrix vMatrix) {
            this.Path.Transform(vMatrix);
            vMatrix.TransformPoints(FPoints);
            this.InitializePath();
        }

        /// <summary>
        /// 拡大時の倍率を取得します。
        /// </summary>
        public (float, float) GetScale(PointF vStartPoint, PointF vEndPoint) {
            float wScaleX = FScalingX ? (vEndPoint.X - this.OppositePoint.X) / (vStartPoint.X - this.OppositePoint.X) : 1;
            float wScaleY = FScalingY ? (vEndPoint.Y - this.OppositePoint.Y) / (vStartPoint.Y - this.OppositePoint.Y) : 1;
            return (wScaleX, wScaleY);
        }

        #endregion メソッド

    }
}
