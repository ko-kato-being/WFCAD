using System;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// コマンドクラス
    /// </summary>
    public class Command {
        protected event Action FDo;
        protected event Action FUndo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Command(Action vDo, Action vUndo) {
            FDo = vDo;
            FUndo = vUndo;
        }
        /// <summary>
        /// 実行します
        /// </summary>
        public void Execute() => FDo?.Invoke();
        /// <summary>
        /// 元に戻します
        /// </summary>
        public void Undo() => FUndo?.Invoke();
    }
}
