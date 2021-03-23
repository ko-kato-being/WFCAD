using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// クリアコマンド
    /// </summary>
    public class ClearCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClearCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.Clear();
    }
}
