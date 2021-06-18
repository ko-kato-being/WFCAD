namespace WFCAD.Controller {
    public interface ICommand {
        void Execute();
        void Undo();
    }
}
