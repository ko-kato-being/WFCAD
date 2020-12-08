using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// スナップショットクラス
    /// </summary>
    public class Snapshot : ISnapshot {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Snapshot(Bitmap vBitmap, IShapes vShapes) {
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
        public IShapes Shapes { get; set; }

        #endregion プロパティ

    }
}
