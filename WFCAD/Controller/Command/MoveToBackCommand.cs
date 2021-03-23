using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 最背面へ移動コマンド
    /// </summary>
    public class MoveToBackCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MoveToBackCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.MoveToBack();
    }
}
