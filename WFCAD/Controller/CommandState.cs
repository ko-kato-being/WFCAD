namespace WFCAD.Controller {
    /// <summary>
    /// コマンド状態
    /// </summary>
    public class CommandState {
        /// <summary>
        /// 前の状態
        /// </summary>
        public CommandState PrevState { get; set; }
        /// <summary>
        /// 次の状態
        /// </summary>
        public CommandState NextState { get; set; }
        /// <summary>
        /// 前のコマンド
        /// </summary>
        public ICommand PrevCommand { get; set; }
        /// <summary>
        /// 次のコマンド
        /// </summary>
        public ICommand NextCommand { get; set; }
    }
}
