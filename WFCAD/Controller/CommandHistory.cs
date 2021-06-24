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
        public CommandHistory() {
            FStates.Add(new CommandState());
        }

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
            this.AddNewState(wNewState);
        }
        public void Undo() {
            if (this.CurrentState?.PrevState == null) return;
            if (this.CurrentState?.PrevCommand == null) return;
            this.CurrentState.PrevCommand.Undo();
            FCurrentStateIndex = FStates.IndexOf(this.CurrentState.PrevState);
        }
        public void Redo() {
            if (this.CurrentState?.NextState == null) return;
            if (this.CurrentState?.NextCommand == null) return;
            this.CurrentState.NextCommand.Execute();
            FCurrentStateIndex = FStates.IndexOf(this.CurrentState.NextState);
        }

        private void AddNewState(CommandState vNewState) {
            EliminateStates(this.CurrentState.NextState);

            this.CurrentState.NextState = vNewState;
            this.CurrentState.NextCommand = vNewState.PrevCommand;
            vNewState.PrevState = this.CurrentState;
            FStates.Add(vNewState);
            FCurrentStateIndex = FStates.IndexOf(vNewState);
        }
        private void EliminateStates(CommandState vTarget) {
            if (vTarget == null) return;
            int wIndex = FStates.IndexOf(vTarget);
            FStates.RemoveRange(wIndex, FStates.Count - wIndex);
        }
    }
}
