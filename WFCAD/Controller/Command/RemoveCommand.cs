using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 削除コマンド
    /// </summary>
    public class RemoveCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RemoveCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.Remove();
    }
}
