using System.Collections.Generic;
using System.Linq;

namespace WFCAD.Controller {
    /// <summary>
    /// コマンド履歴
    /// </summary>
    public class CommandHistory {
        private List<CommandState> FStates = new List<CommandState>();
        private int FCurrentStateIndex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CommandHistory() => FStates.Add(new CommandState());

        /// <summary>
        /// 現在の状態
        /// </summary>
        public CommandState CurrentState {
            get {
                if (!FStates.Any()) return null;
                if (FCurrentStateIndex < 0 || FCurrentStateIndex >= FStates.Count) return null;
                return FStates.ElementAt(FCurrentStateIndex);
            }
        }

        /// <summary>
        /// コマンドを登録します
        /// </summary>
        public void Record(ICommand vCommand) {
            vCommand.Execute();
            var wNewState = new CommandState();
            wNewState.PrevCommand = vCommand;

            // 現在の状態より後ろの状態を破棄します
            if (this.CurrentState.NextState != null) {
                int wIndex = FStates.IndexOf(this.CurrentState.NextState);
                FStates.RemoveRange(wIndex, FStates.Count - wIndex);
            }

            this.CurrentState.NextState = wNewState;
            this.CurrentState.NextCommand = wNewState.PrevCommand;
            wNewState.PrevState = this.CurrentState;
            FStates.Add(wNewState);
            FCurrentStateIndex = FStates.IndexOf(wNewState);
        }

        /// <summary>
        /// 前のコマンドを実行します
        /// </summary>
        public void Undo() {
            if (this.CurrentState?.PrevState == null) return;
            if (this.CurrentState?.PrevCommand == null) return;
            this.CurrentState.PrevCommand.Undo();
            FCurrentStateIndex = FStates.IndexOf(this.CurrentState.PrevState);
        }

        /// <summary>
        /// 次のコマンドを実行します
        /// </summary>
        public void Redo() {
            if (this.CurrentState?.NextState == null) return;
            if (this.CurrentState?.NextCommand == null) return;
            this.CurrentState.NextCommand.Execute();
            FCurrentStateIndex = FStates.IndexOf(this.CurrentState.NextState);
        }
    }
}
