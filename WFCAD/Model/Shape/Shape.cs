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
        /// 始点
        /// </summary>
        public PointF StartPoint { get; protected set; }

        /// <summary>
        /// 終点
        /// </summary>
        public PointF EndPoint { get; protected set; }

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
        /// 初期化します
        /// </summary>
        public abstract void Initialize(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public abstract void SetPoints(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 描画します
        /// </summary>
        public abstract void Draw(Bitmap vBitmap, Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        public abstract void DrawFrame(Bitmap vBitmap, Graphics vGraphics);

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(float vOffsetX, float vOffsetY) {
            this.Matrix.Reset();
            this.Matrix.Translate(vOffsetX, vOffsetY);
            this.AppleyAffine();
        }

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(float vScaleX, float vScaleY) {
            IFramePoint wFramePoint = this.FramePoints.SingleOrDefault(x => x.IsSelected);
            if (wFramePoint == null) return;

            this.Matrix.Reset();
            var wBasePoints = wFramePoint.BasePoints.ToList();
            this.Matrix.ScaleAt(vScaleX, vScaleY, wBasePoints.First());
            this.AppleyAffine();
            wFramePoint.IsSelected = false;
        }

        /// <summary>
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected abstract (PointF StartPoint, PointF EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, SizeF vSize);

        /// <summary>
        /// 回転します
        /// </summary>
        public void Rotate(float vAngle) {
            this.Matrix.Reset();
            float wCenterX = this.SubPath.PathPoints.Select(x => x.X).Sum() / 4f;
            float wCenterY = this.SubPath.PathPoints.Select(x => x.Y).Sum() / 4f;
            var wCenterPoint = new PointF(wCenterX, wCenterY);
            this.Matrix.RotateAt(vAngle, wCenterPoint, MatrixOrder.Append);
            this.AppleyAffine();
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(PointF vCoordinate);

        /// <summary>
        /// 複製します
        /// </summary>
        public IShape DeepClone() {
            Shape wShape = this.DeepCloneCore();
            wShape.SetPoints(this.StartPoint, this.EndPoint);
            wShape.IsSelected = this.IsSelected;
            wShape.Color = this.Color;
            wShape.FramePoints = this.FramePoints?.Select(x => x.DeepClone());
            return wShape;
        }

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        private void AppleyAffine() {
            this.MainPath.Transform(this.Matrix);
            this.SubPath.Transform(this.Matrix);
            foreach (IFramePoint wPoint in this.FramePoints) {
                wPoint.Path.Transform(this.Matrix);
            }
        }

        /// <summary>
        /// 派生クラスごとのインスタンスを返します
        /// </summary>
        protected abstract Shape DeepCloneCore();

        #endregion メソッド
    }
}