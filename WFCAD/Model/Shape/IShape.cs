using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// 図形を表すインターフェース
    /// </summary>
    public interface IShape {

        #region プロパティ

        /// <summary>
        /// 始点
        /// </summary>
        Point StartPoint { get; set; }

        /// <summary>
        /// 終点
        /// </summary>
        Point EndPoint { get; set; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 表示状態
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// 表示色
        /// </summary>
        Color Color { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        Bitmap Draw(Bitmap vBitmap);

        /// <summary>
        /// 移動します
        /// </summary>
        void Move(Size vSize);

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        bool IsHit(Point vCoordinate);

        /// <summary>
        /// 複製します
        /// </summary>
        IShape DeepClone();

        #endregion メソッド

    }
}
