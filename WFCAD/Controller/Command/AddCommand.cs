using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// 追加コマンド
    /// </summary>
    public class AddCommand : Command {
        /// <summary>
        /// 始点
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// 終点
        /// </summary>
        public Point EndPoint { get; set; }
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
                IShape wShape = this.Shape.DeepClone();
                wShape.SetPoints(this.StartPoint, this.EndPoint);
                FCanvas.Add(wShape);
            }
        }
    }
}
