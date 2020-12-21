using System;
using System.Drawing;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 図形クラス
    /// </summary>
    public abstract class Shape : IShape {
        private bool FIsSelected;
        protected Color FPrevColor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Shape(Color vColor) {
            this.Color = vColor;
            FPrevColor = vColor;
        }

        #region プロパティ

        /// <summary>
        /// 始点
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// 終点
        /// </summary>
        public Point EndPoint { get; set; }

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
        public bool IsSelected {
            get => FIsSelected;
            set {
                FIsSelected = value;
                if (FIsSelected) this.Select();
                else this.UnSelect();
            }
        }

        /// <summary>
        /// 表示状態
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 表示色
        /// </summary>
        public Color Color { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            if (this.Visible) {
                using (var wGraphics = Graphics.FromImage(vBitmap)) {
                    this.DrawCore(wGraphics);
                }
            }
            return vBitmap;
        }

        /// <summary>
        /// 派生クラスごとの描画処理
        /// </summary>
        protected abstract void DrawCore(Graphics vGraphics);

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(Size vSize) {
            this.StartPoint += vSize;
            this.EndPoint += vSize;
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public abstract bool IsHit(Point vCoordinate);

        /// <summary>
        /// 選択状態にします
        /// </summary>
        protected void Select() {
            FPrevColor = this.Color;
            this.Color = Color.Blue;
        }

        /// <summary>
        /// 選択状態を解除します
        /// </summary>
        protected void UnSelect() => this.Color = FPrevColor;

        /// <summary>
        /// 複製します
        /// </summary>
        public IShape DeepClone() {
            IShape wShape = this.DeepCloneCore();
            wShape.StartPoint = this.StartPoint;
            wShape.EndPoint = this.EndPoint;
            wShape.IsSelected = this.IsSelected;
            wShape.Color = this.Color;
            return wShape;
        }

        /// <summary>
        /// 派生クラスごとのインスタンスを返します
        /// </summary>
        protected abstract IShape DeepCloneCore();

        #endregion メソッド
    }
}
