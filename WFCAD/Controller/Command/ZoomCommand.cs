using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 拡大・縮小コマンド
    /// </summary>
    public class ZoomCommand : EditCommand {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZoomCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() {
            var wSize = new Size(this.EndPoint.X - this.StartPoint.X, this.EndPoint.Y - this.StartPoint.Y);
            if (wSize.IsEmpty) return;
            FCanvas.Zoom(wSize);
        }
    }
}
