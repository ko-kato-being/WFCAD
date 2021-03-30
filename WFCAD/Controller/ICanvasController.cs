﻿using System.Drawing;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// キャンバスコントローラーインターフェース
    /// </summary>
    public interface ICanvasController {

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
        /// 描画色
        /// </summary>
        Color Color { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 図形を選択します
        /// </summary>
        void SelectShapes(Point vMouseLocation, bool vIsMultiple);

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
        /// 図形を追加します
        /// </summary>
        void AddShape(IShape vShape);

        /// <summary>
        /// 図形を編集します
        /// </summary>
        void EditShapes(Point vMouseLocation);

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
