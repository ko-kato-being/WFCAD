using System.Drawing;

namespace WFCAD.View {
    /// <summary>
    /// キャンバスビューを表すインターフェース
    /// </summary>
    public interface ICanvasView {
        /// <summary>
        /// 幅
        /// </summary>
        int Width { get; }
        /// <summary>
        /// 高さ
        /// </summary>
        int Height { get; }
        /// <summary>
        /// すべて再描画します
        /// </summary>
        void RefreshAll(Bitmap vBitmap);
        /// <summary>
        /// プレビューを再描画します
        /// </summary>
        void RefreshPreview(Bitmap vBitmap);
    }
}
