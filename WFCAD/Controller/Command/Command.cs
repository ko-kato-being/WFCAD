using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// コマンドクラス
    /// </summary>
    public abstract class Command : ICommand {
        protected readonly Canvas FCanvas;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Command(Canvas vCanvas) => FCanvas = vCanvas;
        /// <summary>
        /// 実行します
        /// </summary>
        public abstract void Execute();
    }
}
