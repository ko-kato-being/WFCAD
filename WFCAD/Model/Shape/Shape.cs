using System;
using System.Collections.Generic;
using System.Drawing;
using WFCAD.Model.Frame;

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
        /// 外枠
        /// </summary>
        protected System.Drawing.Rectangle FrameRectangle => new System.Drawing.Rectangle(this.StartPoint.X, this.StartPoint.Y, this.Width, this.Height);

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

        /// <summary>
        /// 枠点リスト
        /// </summary>
        public IEnumerable<IFramePoint> FramePoints { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        public virtual void SetPoints(Point vStartPoint, Point vEndPoint) {
            // 引数で受け取った始点と終点を対角線とする矩形に対して、
            // 左上の点と右下の点を始点と終点に設定します。
            this.StartPoint = new Point(Math.Min(vStartPoint.X, vEndPoint.X), Math.Min(vStartPoint.Y, vEndPoint.Y));
            this.EndPoint = new Point(Math.Max(vStartPoint.X, vEndPoint.X), Math.Max(vStartPoint.Y, vEndPoint.Y));

            // 枠点の座標
            var wTopLeft = this.StartPoint;
            var wTop = new Point(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.StartPoint.Y);
            var wTopRight = new Point(this.EndPoint.X, this.StartPoint.Y);
            var wLeft = new Point(this.StartPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wRight = new Point(this.EndPoint.X, this.StartPoint.Y + (this.EndPoint.Y - this.StartPoint.Y) / 2);
            var wBottomLeft = new Point(this.StartPoint.X, this.EndPoint.Y);
            var wBottom = new Point(this.StartPoint.X + (this.EndPoint.X - this.StartPoint.X) / 2, this.EndPoint.Y);
            var wBottomRight = this.EndPoint;

            // 枠点と基準点の設定
            this.FramePoints = new List<IFramePoint> {
                new FramePoint(wTopLeft, wBottomRight),
                new FramePoint(wTop, wBottomLeft, wBottomRight),
                new FramePoint(wTopRight, wBottomLeft),
                new FramePoint(wLeft, wTopRight, wBottomRight),
                new FramePoint(wRight, wTopLeft, wBottomLeft),
                new FramePoint(wBottomLeft, wTopRight),
                new FramePoint(wBottom, wTopLeft, wTopRight),
                new FramePoint(wBottomRight, wTopLeft),
            };
        }

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            if (this.Visible) {
                using (var wGraphics = Graphics.FromImage(vBitmap)) {
                    this.DrawCore(wGraphics);
                    if (this.IsSelected) {
                        this.DrawFrame(wGraphics);
                    }
                }
            }
            return vBitmap;
        }

        /// <summary>
        /// 派生クラスごとの描画処理
        /// </summary>
        protected abstract void DrawCore(Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        protected virtual void DrawFrame(Graphics vGraphics) {
            // 枠線は黒色で固定
            using (var wPen = new Pen(Color.Black)) {
                vGraphics.DrawRectangle(wPen, this.FrameRectangle);
                foreach (IFramePoint wFramePoint in this.FramePoints) {
                    wFramePoint.Draw(vGraphics, wPen);
                }
            }
        }

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
            wShape.SetPoints(this.StartPoint, this.EndPoint);
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
