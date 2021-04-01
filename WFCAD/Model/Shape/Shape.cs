using System;
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
        /// 次元数
        /// </summary>
        public abstract int Dimensionality { get; }

        /// <summary>
        /// 始点
        /// </summary>
        public Point StartPoint { get; protected set; }

        /// <summary>
        /// 終点
        /// </summary>
        public Point EndPoint { get; protected set; }

        /// <summary>
        /// 幅
        /// </summary>
        protected int Width => Math.Abs(this.StartPoint.X - this.EndPoint.X);

        /// <summary>
        /// 高さ
        /// </summary>
        protected int Height => Math.Abs(this.StartPoint.Y - this.EndPoint.Y);

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
        /// 始点と終点を設定します
        /// </summary>
        public abstract void SetPoints(Point vStartPoint, Point vEndPoint);

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            using (var wGraphics = Graphics.FromImage(vBitmap)) {
                this.DrawCore(wGraphics);
            }
            return vBitmap;
        }

        /// <summary>
        /// 枠を描画します
        /// </summary>
        public void DrawFrame(Bitmap vBitmap) {
            using (var wGraphics = Graphics.FromImage(vBitmap)) {
                wGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                this.DrawFrame(wGraphics);
            }
        }

        /// <summary>
        /// 派生クラスごとの描画処理
        /// </summary>
        protected abstract void DrawCore(Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        protected abstract void DrawFrame(Graphics vGraphics);

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(Size vSize) => this.SetPoints(this.StartPoint + vSize, this.EndPoint + vSize);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        public void Zoom(Size vSize) {
            IFramePoint wFramePoint = this.FramePoints.SingleOrDefault(x => x.IsSelected);
            if (wFramePoint == null) return;

            (Point wStartPoint, Point wEndPoint) = this.GetChangeScalePoints(wFramePoint, vSize);
            this.SetPoints(wStartPoint, wEndPoint);
            wFramePoint.IsSelected = false;
        }

        /// <summary>
        /// 拡大・縮小するための座標取得処理
        /// </summary>
        protected abstract (Point StartPoint, Point EndPoint) GetChangeScalePoints(IFramePoint vFramePoint, Size vSize);

        /// <summary>
        /// 右に回転させます
        /// </summary>
        public void RotateRight() {
            var wOrigin = new Point(Math.Min(this.StartPoint.X, this.EndPoint.X) + this.Width / 2, Math.Min(this.StartPoint.Y, this.EndPoint.Y) + this.Height / 2);
            this.SetPoints(Utilities.RotateRight90(this.StartPoint, wOrigin), Utilities.RotateRight90(this.EndPoint, wOrigin));
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(Point vCoordinate);

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
        /// 派生クラスごとのインスタンスを返します
        /// </summary>
        protected abstract Shape DeepCloneCore();

        #endregion メソッド
    }
}