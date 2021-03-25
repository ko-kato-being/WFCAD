using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 編集コマンド
    /// </summary>
    public abstract class EditCommand : Command {
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
        public EditCommand(Canvas vCanvas) : base(vCanvas) { }

        /// <summary>
        /// 複製を返します
        /// </summary>
        public abstract EditCommand DeepClone(Canvas vCanvas);
    }
}
