using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 複製コマンド
    /// </summary>
    public class CloneCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CloneCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.Clone();
    }
}
