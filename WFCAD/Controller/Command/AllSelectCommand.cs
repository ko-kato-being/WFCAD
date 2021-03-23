using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 全選択コマンド
    /// </summary>
    public class AllSelectCommand : Command {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AllSelectCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() => FCanvas.AllSelect();
    }
}
