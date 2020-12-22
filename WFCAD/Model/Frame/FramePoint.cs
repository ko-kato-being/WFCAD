using System.Collections.Generic;
using System.Drawing;

namespace WFCAD.Model.Frame {
    /// <summary>
    /// 枠の点クラス
    /// </summary>
    public class FramePoint : IFramePoint {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FramePoint(Point vPoint, params Point[] vBasePoints) {
            this.Point = vPoint;
            this.BasePoints = vBasePoints;
        }

        #region プロパティ

        /// <summary>
        /// 座標
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// 基準点
        /// </summary>
        public IEnumerable<Point> BasePoints { get; set; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します。
        /// </summary>
        public void Draw() {

        }

        /// <summary>
        /// 指定した座標が円内に存在するか
        /// </summary>
        public bool IsHit(Point vCoordinate) {
            return false;
        }

        #endregion メソッド

    }
}
