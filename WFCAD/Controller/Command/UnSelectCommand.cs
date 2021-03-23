using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 選択解除コマンド
    /// </summary>
    public class UnselectCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnselectCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.Unselect();
    }
}
