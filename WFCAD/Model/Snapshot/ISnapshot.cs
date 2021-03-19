using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// スナップショットを表すインターフェース
    /// </summary>
    public interface ISnapshot {

        #region プロパティ

        /// <summary>
        /// ビットマップ
        /// </summary>
        Bitmap Bitmap { get; set; }

        /// <summary>
        /// キャンバス
        /// </summary>
        ICanvas Canvas { get; set; }

        #endregion プロパティ

    }
}
