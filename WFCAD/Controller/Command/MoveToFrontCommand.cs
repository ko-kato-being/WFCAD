using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 最前面へ移動コマンド
    /// </summary>
    public class MoveToFrontCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MoveToFrontCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.MoveToFront();
    }
}
