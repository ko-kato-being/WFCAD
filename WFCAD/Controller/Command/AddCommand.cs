using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 追加コマンド
    /// </summary>
    public class AddCommand : EditCommand {
        /// <summary>
        /// 現在の図形
        /// </summary>
        public IShape Shape { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddCommand(Canvas vCanvas) : base(vCanvas) { }
        /// <summary>
        /// 実行します
        /// </summary>
        public override void Execute() {
            // 二点間の距離が10以下の図形は意図していないとみなして追加しない。
            double wDistance = Utilities.GetDistance(this.StartPoint, this.EndPoint);
            if (wDistance > 10) {
                this.Shape.SetPoints(this.StartPoint, this.EndPoint);
                FCanvas.Add(this.Shape);
            }
        }
        /// <summary>
        /// 複製を返します
        /// </summary>
        public override EditCommand DeepClone(Canvas vCanvas) => new AddCommand(vCanvas) {
            StartPoint = this.StartPoint,
            EndPoint = this.EndPoint,
            Shape = this.Shape.DeepClone(),
        };
    }
}
