using WFCAD.Model;

namespace WFCAD.Controller {
    public abstract class Command : ICommand {
        protected readonly Canvas FCanvas;
        public Command(Canvas vCanvas) => FCanvas = vCanvas;
        public abstract void Execute();
        public abstract void Undo();
    }
}
