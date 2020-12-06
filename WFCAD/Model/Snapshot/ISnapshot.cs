using System.Drawing;

namespace WFCAD {
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
        /// 図形群
        /// </summary>
        IShapes Shapes { get; set; }

        #endregion プロパティ

    }
}
