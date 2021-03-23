using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 選択コマンド
    /// </summary>
    public class SelectCommand : Command {
        /// <summary>
        /// 座標
        /// </summary>
        public Point Point { get; set; }
        /// <summary>
        /// 複数選択か
        /// </summary>
        public bool IsMultiple { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() {
            FCanvas.Select(this.Point, this.IsMultiple);
        }
    }
}
