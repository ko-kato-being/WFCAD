using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 枠の点を表すインターフェース
    /// </summary>
    public interface IFramePoint {

        #region プロパティ

        /// <summary>
        /// パス
        /// </summary>
        GraphicsPath Path { get; }

        /// <summary>
        /// 座標
        /// </summary>
        PointF MainPoint { get; }

        /// <summary>
        /// 基準点
        /// </summary>
        PointF OppositePoint { get; }

        /// <summary>
        /// 位置種類
        /// </summary>
        FramePointLocationKindEnum LocationKind { get; }

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
        bool IsHit(PointF vCoordinate);

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        void ApplyAffine(Matrix vMatrix);

        /// <summary>
        /// 拡大時の倍率を取得します。
        /// </summary>
        void Zoom(Matrix vMatrix, PointF vStartPoint, PointF vEndPoint, PointF vCenterPoint, float vCurrentAngle);

        #endregion メソッド

    }
}
