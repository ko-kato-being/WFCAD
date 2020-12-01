using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// キャンバスコントロールインターフェース
    /// </summary>
    public interface ICanvasControl {

        #region プロパティ

        /// <summary>
        /// マウスダウン位置
        /// </summary>
        Point MouseDownLocation { get; set; }

        /// <summary>
        /// マウスアップ位置
        /// </summary>
        Point MouseUpLocation { get; set; }

        /// <summary>
        /// 現在の図形
        /// </summary>
        IShape CurrentShape { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 再描画します
        /// </summary>
        void Refresh();

        /// <summary>
        /// 図形を追加します
        /// </summary>
        void AddShape();

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        void Clear();

        #endregion メソッド
    }
}
