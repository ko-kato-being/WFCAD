using System;
using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle : Shape {

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public override void Draw(Graphics vGraphics)
            => vGraphics.DrawRectangle(new Pen(Color.Black), Math.Min(this.StartPoint.X, this.EndPoint.X), Math.Min(this.StartPoint.Y, this.EndPoint.Y), this.Width, this.Height);

        /// <summary>
        /// 複製します
        /// </summary>
        protected override IShape DeepCloneCore() => new Rectangle();

        #endregion　メソッド

    }
}
