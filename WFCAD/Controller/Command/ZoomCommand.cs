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
        /// <summary>
        /// 複製を返します
        /// </summary>
        public override EditCommand DeepClone(Canvas vCanvas) => new ZoomCommand(vCanvas) {
            StartPoint = this.StartPoint,
            EndPoint = this.EndPoint,
        };
    }
}
