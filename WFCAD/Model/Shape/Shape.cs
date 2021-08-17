using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace WFCAD.Model {
    /// <summary>
    /// 図形クラス
    /// </summary>
    public abstract class Shape : IShape {

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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Shape() { }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// メインパス
        /// </summary>
        public GraphicsPath MainPath { get; private set; } = new GraphicsPath();

        /// <summary>
        /// サブパス
        /// </summary>
        public GraphicsPath SubPath { get; private set; } = new GraphicsPath();

        /// <summary>
        /// 変換行列
        /// </summary>
        public Matrix Matrix { get; private set; } = new Matrix();

        /// <summary>
        /// 次元数
        /// </summary>
        public abstract int Dimensionality { get; }

        /// <summary>
        /// 座標リスト
        /// </summary>
        public PointF[] Points { get; protected set; }

        /// <summary>
        /// 中心点
        /// </summary>
        public PointF CenterPoint => this.Points[0];

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 枠点が選択されているか
        /// </summary>
        public bool IsFramePointSelected => this.FramePoints.Any(x => x.IsSelected);

        /// <summary>
        /// 表示色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 枠点リスト
        /// </summary>
        public IEnumerable<IFramePoint> FramePoints { get; protected set; }

        /// <summary>
        /// 現在の角度
        /// </summary>
        public float CurrentAngle { get; private set; }

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
        public void Draw(Graphics vGraphics, bool vIsDrawFrame) {
            this.ApplyAffine();
            using (var wBrush = new SolidBrush(this.Color))
            using (var wPen = new Pen(C_BorderColor, 2f)) {
                vGraphics.FillPath(wBrush, this.MainPath);
                vGraphics.DrawPath(wPen, this.MainPath);
            }
            if (!this.IsSelected || !vIsDrawFrame) return;
            this.DrawFrame(vGraphics);
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        protected abstract void DrawFrame(Graphics vGraphics);

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        public void ApplyAffine() {
            this.MainPath.Transform(this.Matrix);
            this.SubPath.Transform(this.Matrix);
            this.Matrix.TransformPoints(this.Points);
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
        /// 移動します
        /// </summary>
        public void Move(PointF vStartPoint, PointF vEndPoint) => this.Matrix.Translate(vEndPoint.X - vStartPoint.X, vEndPoint.Y - vStartPoint.Y, MatrixOrder.Append);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(IFramePoint vFramePoint, PointF vStartPoint, PointF vEndPoint, bool vIsPreview = false) {
            vFramePoint.Zoom(this.Matrix, vStartPoint, vEndPoint, this.CenterPoint, this.CurrentAngle);

            if (vIsPreview) return;
            this.SetLocation();
        }

        /// <summary>
        /// 回転します
        /// </summary>
        public void Rotate(float vAngle) {
            this.CurrentAngle += vAngle;
            this.Matrix.RotateAt(vAngle, this.CenterPoint, MatrixOrder.Append);
            this.SetLocation();
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(PointF vCoordinate);

        /// <summary>
        /// 現在の位置種類を設定します
        /// </summary>
        protected abstract void SetLocation();

        /// <summary>
        /// 複製します
        /// </summary>
        public IShape DeepClone() {
            Shape wClone = this.DeepCloneCore();
            wClone.MainPath = (GraphicsPath)this.MainPath.Clone();
            wClone.SubPath = (GraphicsPath)this.SubPath.Clone();
            wClone.Matrix = this.Matrix.Clone();
            wClone.Points = this.Points.ToArray();
            wClone.IsSelected = this.IsSelected;
            wClone.Color = this.Color;
            wClone.FramePoints = this.FramePoints.Select(x => x.DeepClone()).ToList();
            foreach (IFramePoint wFrame in wClone.FramePoints) {
                wFrame.Selected += () => wClone.IsSelected = true;
            }
            wClone.CurrentAngle = this.CurrentAngle;
            return wClone;
        }

        /// <summary>
        /// 派生クラスごとのインスタンスを返します
        /// </summary>
        protected abstract Shape DeepCloneCore();

        #endregion メソッド
    }
}