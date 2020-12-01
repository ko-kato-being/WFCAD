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

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        void Draw(Graphics vGraphics);

        /// <summary>
        /// 複製します
        /// </summary>
        /// <returns></returns>
        IShape DeepClone();

        #endregion メソッド

    }
}
