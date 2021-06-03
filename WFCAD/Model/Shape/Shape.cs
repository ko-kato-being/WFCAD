using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 図形クラス
    /// </summary>
    public abstract class Shape : IShape {
        private float FCurrentAngle;
        protected PointF[] FPoints;

        #region 定数

        protected static readonly Color C_BorderColor = Color.Black;
        protected static readonly Color C_FrameColor = Color.Gray;

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Shape(PointF vStartPoint, PointF vEndPoint, Color vColor) {
            this.InitializePath(vStartPoint, vEndPoint);
            this.CreateFrame(vStartPoint, vEndPoint);
            this.SetPoints(vStartPoint, vEndPoint);
            this.Color = vColor;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// メインパス
        /// </summary>
        public GraphicsPath MainPath { get; } = new GraphicsPath();

        /// <summary>
        /// サブパス
        /// </summary>
        public GraphicsPath SubPath { get; } = new GraphicsPath();

        /// <summary>
        /// 変換行列
        /// </summary>
        public Matrix Matrix { get; set; } = new Matrix();

        /// <summary>
        /// 次元数
        /// </summary>
        public abstract int Dimensionality { get; }

        /// <summary>
        /// 中心点
        /// </summary>
        public PointF CenterPoint => FPoints[0];

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 表示色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 枠点リスト
        /// </summary>
        public IEnumerable<IFramePoint> FramePoints { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// パスを初期化します
        /// </summary>
        protected abstract void InitializePath(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 枠を生成します
        /// </summary>
        protected abstract void CreateFrame(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 点を設定します
        /// </summary>
        protected abstract void SetPoints(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 描画します
        /// </summary>
        public abstract void Draw(Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        public abstract void DrawFrame(Graphics vGraphics);

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        public void ApplyAffine() {
            this.MainPath.Transform(this.Matrix);
            this.SubPath.Transform(this.Matrix);
            this.Matrix.TransformPoints(FPoints);
            foreach (IFramePoint wPoint in this.FramePoints) {
                wPoint.ApplyAffine(this.Matrix);
            }
            this.Matrix.Reset();
        }

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(SizeF vSize) => this.Matrix.Translate(vSize.Width, vSize.Height, MatrixOrder.Append);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(PointF vStartPoint, PointF vEndPoint) {
            IFramePoint wFramePoint = this.FramePoints.SingleOrDefault(x => x.IsSelected);
            if (wFramePoint == null) return;

            wFramePoint.Zoom(this.Matrix, vStartPoint, vEndPoint, this.CenterPoint, FCurrentAngle);
            wFramePoint.IsSelected = false;
            this.SetLocation();
        }

        /// <summary>
        /// 回転します
        /// </summary>
        public void Rotate(float vAngle) {
            FCurrentAngle += vAngle;
            this.Matrix.RotateAt(vAngle, this.CenterPoint, MatrixOrder.Append);
            this.SetLocation();
        }

        /// <summary>
        /// 現在の位置種類を設定します
        /// </summary>
        private void SetLocation() {
            foreach (IFramePoint wFramPoint in this.FramePoints) {
                wFramPoint.CurrentLocationKind = null;
            }
            // float型で計算すると小数点以下の誤差でずれてしまうことがあるのでint型で比較する

            // 頂点は自身の座標を元に判定
            // ただし、複数存在する時があるので別方向の座標で確認する
            IFramePoint wTop = this.FramePoints.OrderBy(x => (int)x.MainPoint.Y).ThenBy(x => (int)x.MainPoint.X).First();
            IFramePoint wRight = this.FramePoints.OrderByDescending(x => (int)x.MainPoint.X).ThenBy(x => (int)x.MainPoint.Y).First();
            IFramePoint wBottom = this.FramePoints.OrderByDescending(x => (int)x.MainPoint.Y).ThenByDescending(x => (int)x.MainPoint.X).First();
            IFramePoint wLeft = this.FramePoints.OrderBy(x => (int)x.MainPoint.X).ThenByDescending(x => (int)x.MainPoint.Y).First();
            wTop.CurrentLocationKind = FramePointLocationKindEnum.Top;
            wRight.CurrentLocationKind = FramePointLocationKindEnum.Right;
            wBottom.CurrentLocationKind = FramePointLocationKindEnum.Bottom;
            wLeft.CurrentLocationKind = FramePointLocationKindEnum.Left;

            // 中点は中心点との位置を比較してどの象限にいるかを判定
            int wCenterX = (int)this.CenterPoint.X;
            int wCenterY = (int)this.CenterPoint.Y;
            IFramePoint wTopRight = this.FramePoints.Where(x => !x.CurrentLocationKind.HasValue && (int)x.MainPoint.X > wCenterX && (int)x.MainPoint.Y <= wCenterY).Single();
            IFramePoint wRightBottom = this.FramePoints.Where(x => !x.CurrentLocationKind.HasValue && (int)x.MainPoint.X >= wCenterX && (int)x.MainPoint.Y > wCenterY).Single();
            IFramePoint wBottomLeft = this.FramePoints.Where(x => !x.CurrentLocationKind.HasValue && (int)x.MainPoint.X < wCenterX && (int)x.MainPoint.Y >= wCenterY).Single();
            IFramePoint wLeftTop = this.FramePoints.Where(x => !x.CurrentLocationKind.HasValue && (int)x.MainPoint.X <= wCenterX && (int)x.MainPoint.Y < wCenterY).Single();
            wTopRight.CurrentLocationKind = FramePointLocationKindEnum.TopRight;
            wRightBottom.CurrentLocationKind = FramePointLocationKindEnum.RightBottom;
            wBottomLeft.CurrentLocationKind = FramePointLocationKindEnum.BottomLeft;
            wLeftTop.CurrentLocationKind = FramePointLocationKindEnum.LeftTop;
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(PointF vCoordinate);

        #endregion メソッド
    }
}