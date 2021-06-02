using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 図形クラス
    /// </summary>
    public abstract class Shape : IShape {
        protected PointF[] FPoints;
        private float FCurrentAngle;

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
            (float wScaleX, float wScaleY) = wFramePoint.GetScale(vStartPoint, vEndPoint);

            this.Matrix.RotateAt(FCurrentAngle * -1, this.CenterPoint, MatrixOrder.Append);
            this.Matrix.ScaleAt(wScaleX, wScaleY, wFramePoint.OppositePoint);
            this.Matrix.RotateAt(FCurrentAngle, this.CenterPoint, MatrixOrder.Append);

            wFramePoint.IsSelected = false;
        }

        /// <summary>
        /// 回転します
        /// </summary>
        public void Rotate(float vAngle) {
            FCurrentAngle += vAngle;
            this.Matrix.RotateAt(vAngle, this.CenterPoint, MatrixOrder.Append);
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(PointF vCoordinate);

        #endregion メソッド
    }
}