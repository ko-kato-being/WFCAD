using System;
using System.Collections.Generic;
using System.Drawing;

namespace WFCAD.Model {
    /// <summary>
    /// キャンバスを表すインターフェース
    /// </summary>
    public interface ICanvas {

        #region イベント

        /// <summary>
        /// 更新イベント
        /// </summary>
        event Action<Bitmap> Updated;

        /// <summary>
        /// プレビューイベント
        /// </summary>
        event Action<Bitmap> Preview;

        #endregion イベント

        #region プロパティ

        /// <summary>
        /// ビットマップ
        /// </summary>
        Bitmap Bitmap { get; set; }

        /// <summary>
        /// クリップボード
        /// </summary>
        List<IShape> Clipboard { get; set; }

        #endregion プロパティ

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        void Draw();

        /// <summary>
        /// 選択します
        /// </summary>
        void Select(Point vCoordinate, bool vIsMultiple);

        /// <summary>
        /// 全選択します
        /// </summary>
        void AllSelect();

        /// <summary>
        /// 選択を解除します
        /// </summary>
        void Unselect();

        /// <summary>
        /// 追加します
        /// </summary>
        void Add(IShape vShape, bool vIsPreview = false);

        /// <summary>
        /// 編集します
        /// </summary>
        void Edit(Size vSize, bool vIsPreview = false);

        /// <summary>
        /// 右に回転させます
        /// </summary>
        void RotateRight();

        /// <summary>
        /// 最前面に移動します
        /// </summary>
        void MoveToFront();

        /// <summary>
        /// 最背面に移動します
        /// </summary>
        void MoveToBack();

        /// <summary>
        /// 複製します
        /// </summary>
        void Clone();

        /// <summary>
        /// コピーします
        /// </summary>
        void Copy(bool vIsCut = false);

        /// <summary>
        /// 貼り付けます
        /// </summary>
        void Paste();

        /// <summary>
        /// 削除します
        /// </summary>
        void Remove();

        /// <summary>
        /// クリアします
        /// </summary>
        void Clear();

        /// <summary>
        /// 自身のインスタンスを複製します
        /// </summary>
        ICanvas DeepClone();

        #endregion　メソッド

    }
}
