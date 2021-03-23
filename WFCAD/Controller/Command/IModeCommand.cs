using System.Drawing;

namespace WFCAD.Controller {
    /// <summary>
    /// モードコマンドインターフェース
    /// </summary>
    public interface IModeCommand : ICommand {
        /// <summary>
        /// マウスダウン位置
        /// </summary>
        Point StartPoint { get; set; }
        /// <summary>
        /// マウスアップ位置
        /// </summary>
        Point EndPoint { get; set; }
    }
}
