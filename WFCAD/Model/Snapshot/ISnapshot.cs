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
        /// 図形群
        /// </summary>
        ICanvas Shapes { get; set; }

        #endregion プロパティ

    }
}
