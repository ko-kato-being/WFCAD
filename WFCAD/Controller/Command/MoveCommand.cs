using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 移動コマンドコマンド
    /// </summary>
    public class MoveCommand : EditCommand {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MoveCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() {
            var wSize = new Size(this.EndPoint.X - this.StartPoint.X, this.EndPoint.Y - this.StartPoint.Y);
            if (wSize.IsEmpty) return;
            FCanvas.Move(wSize);
        }
    }
}
