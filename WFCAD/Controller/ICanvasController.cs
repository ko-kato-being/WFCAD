using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// キャンバスコントローラーインターフェース
    /// </summary>
    public interface ICanvasController {

        #region メソッド

        /// <summary>
        /// すべての図形を選択します
        /// </summary>
        void AllSelectShapes();

        /// <summary>
        /// すべての図形の選択を解除します
        /// </summary>
        void UnselectShapes();

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        void ShowPreview(Point vMouseLocation);

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        void ShowPreview(IShape vShape, Point vMouseLocation);

        /// <summary>
        /// 図形を右に回転させます
        /// </summary>
        void RotateRightShapes();

        /// <summary>
        /// 図形を最前面に移動します
        /// </summary>
        void MoveToFrontShapes();

        /// <summary>
        /// 図形を最背面に移動します
        /// </summary>
        void MoveToBackShapes();

        /// <summary>
        /// 図形を複製します
        /// </summary>
        void CloneShapes();

        /// <summary>
        /// 図形をクリップボードにコピーします
        /// </summary>
        void CopyShapes(bool vIsCut = false);

        /// <summary>
        /// 図形を貼り付けます
        /// </summary>
        void PasteShapes();

        /// <summary>
        /// 元に戻します
        /// </summary>
        void Undo();

        /// <summary>
        /// やり直します
        /// </summary>
        void Redo();

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
