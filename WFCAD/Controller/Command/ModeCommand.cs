using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// モードコマンド
    /// </summary>
    public abstract class ModeCommand : Command, IModeCommand {
        /// <summary>
        /// 始点
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// 終点
        /// </summary>
        public Point EndPoint { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ModeCommand(Canvas vCanvas) : base(vCanvas) { }
    }
}
