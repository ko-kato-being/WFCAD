using System.Collections.Generic;
using System.Drawing;

namespace WFCAD.Model.Frame {
    /// <summary>
    /// 枠の点を表すインターフェース
    /// </summary>
    public interface IFramePoint {

        #region プロパティ

        /// <summary>
        /// 座標
        /// </summary>
        Point Point { get; }

        /// <summary>
        /// 基準点
        /// </summary>
        IEnumerable<Point> BasePoints { get; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        bool IsSelected { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します。
        /// </summary>
        void Draw(Graphics vGraphics, Pen vPen);

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        bool IsHit(Point vCoordinate);

        #endregion メソッド

    }
}
