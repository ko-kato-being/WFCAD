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
        /// 現在のマウスカーソル位置
        /// </summary>
        Point CurrentMouseLocation { get; set; }

        /// <summary>
        /// 描画色
        /// </summary>
        Color Color { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 再描画します
        /// </summary>
        void Refresh();

        /// <summary>
        /// 図形を選択します
        /// </summary>
        void SelectShape(Point vMouseLocation, bool vIsMultiple);

        /// <summary>
        /// 図形を追加します
        /// </summary>
        void AddShape(IShape vShape);

        /// <summary>
        /// 図形を移動します
        /// </summary>
        void MoveShapes(Point vMouseLocation);

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        void RemoveShapes();

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        void Clear();

        #endregion メソッド
    }
}
