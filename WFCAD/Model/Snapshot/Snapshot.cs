using System.Drawing;
using WFCAD.Model.Shape;

namespace WFCAD.Model.Snapshot {
    /// <summary>
    /// スナップショットクラス
    /// </summary>
    public class Snapshot : ISnapshot {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Snapshot(Bitmap vBitmap, ICanvas vShapes) {
            this.Bitmap = vBitmap;
            this.Shapes = vShapes;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// ビットマップ
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// 図形群
        /// </summary>
        public ICanvas Shapes { get; set; }

        #endregion プロパティ

    }
}
