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
        public FramePoint(PointF vPoint, FramePointLocationKindEnum vLocationKind, PointF vOppositePoint, bool vScalingX = true, bool vScalingY = true) {
            this.Points = new PointF[2] {
                vPoint,
                vOppositePoint
            };
            this.LocationKind = vLocationKind;
            this.CurrentLocationKind = vLocationKind;
            this.ScalingX = vScalingX;
            this.ScalingY = vScalingY;
            this.InitializePath();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FramePoint() { }

        #region プロパティ

        /// <summary>
        /// パス
        /// </summary>
        public GraphicsPath Path { get; private set; } = new GraphicsPath();

        /// <summary>
        /// 座標リスト
        /// </summary>
        public PointF[] Points { get; private set; }

        /// <summary>
        /// 座標
        /// </summary>
        public PointF MainPoint => this.Points[0];

        /// <summary>
        /// 対極の座標
        /// </summary>
        public PointF OppositePoint => this.Points[1];

        /// <summary>
        /// 位置種類
        /// </summary>
        public FramePointLocationKindEnum LocationKind { get; private set; }

        /// <summary>
        /// 現在の位置種類
        /// </summary>
        public FramePointLocationKindEnum? CurrentLocationKind { get; set; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// X方向の倍率
        /// </summary>
        public bool ScalingX { get; private set; }

        /// <summary>
        /// Y方向の倍率
        /// </summary>
        public bool ScalingY { get; private set; }

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
            //switch (this.LocationKind) {
            //    case FramePointLocationKindEnum.Top:
            //        wColor = Color.Red;
            //        break;
            //    case FramePointLocationKindEnum.TopRight:
            //        wColor = Color.Orange;
            //        break;
            //    case FramePointLocationKindEnum.Right:
            //        wColor = Color.Yellow;
            //        break;
            //    case FramePointLocationKindEnum.RightBottom:
            //        wColor = Color.GreenYellow;
            //        break;
            //    case FramePointLocationKindEnum.Bottom:
            //        wColor = Color.Green;
            //        break;
            //    case FramePointLocationKindEnum.BottomLeft:
            //        wColor = Color.LightSkyBlue;
            //        break;
            //    case FramePointLocationKindEnum.Left:
            //        wColor = Color.Blue;
            //        break;
            //    case FramePointLocationKindEnum.LeftTop:
            //        wColor = Color.Purple;
            //        break;
            //}
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
            vMatrix.TransformPoints(this.Points);
            this.InitializePath();
        }

        /// <summary>
        /// 拡大縮小します
        /// </summary>
        public void Zoom(Matrix vMatrix, PointF vStartPoint, PointF vEndPoint, PointF vCenterPoint, float vCurrentAngle) {
            // 相対座標に変換する
            PointF wNewEndPoint = (vStartPoint == this.MainPoint) ? vEndPoint : new PointF(vEndPoint.X + (this.MainPoint.X - vStartPoint.X), vEndPoint.Y + (this.MainPoint.Y - vStartPoint.Y));

            // 角度がついていると正しく座標計算ができないので一旦角度を無くす
            var wInvertedPoints = new PointF[3] {
                this.MainPoint,
                wNewEndPoint,
                this.OppositePoint
            };

            vMatrix.RotateAt(vCurrentAngle * -1, vCenterPoint, MatrixOrder.Append); // 角度を0度にする
            vMatrix.TransformPoints(wInvertedPoints); // 使用する座標すべてに反転行列を適用する

            float wScaleX = this.ScalingX ? (wInvertedPoints[1].X - wInvertedPoints[2].X) / (wInvertedPoints[0].X - wInvertedPoints[2].X) : 1;
            float wScaleY = this.ScalingY ? (wInvertedPoints[1].Y - wInvertedPoints[2].Y) / (wInvertedPoints[0].Y - wInvertedPoints[2].Y) : 1;

            vMatrix.ScaleAt(wScaleX, wScaleY, wInvertedPoints[2]);
            vMatrix.RotateAt(vCurrentAngle, vCenterPoint, MatrixOrder.Append); // 角度を元に戻す
        }

        /// <summary>
        /// 複製します
        /// </summary>
        public IFramePoint DeepClone() {
            var wClone = new FramePoint();
            wClone.Path = (GraphicsPath)this.Path.Clone();
            wClone.Points = this.Points.ToArray();
            wClone.LocationKind = this.LocationKind;
            wClone.CurrentLocationKind = this.CurrentLocationKind;
            wClone.IsSelected = this.IsSelected;
            wClone.ScalingX = this.ScalingX;
            wClone.ScalingY = this.ScalingY;
            return wClone;
        }

        #endregion メソッド

    }
}
