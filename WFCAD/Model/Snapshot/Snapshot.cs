using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// スナップショットクラス
    /// </summary>
    public class Snapshot : ISnapshot {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Snapshot(Bitmap vBitmap, ICanvas vCanvas) {
            this.Bitmap = vBitmap;
            this.Canvas = vCanvas;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// ビットマップ
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// キャンバス
        /// </summary>
        public ICanvas Canvas { get; set; }

        #endregion プロパティ

    }
}
