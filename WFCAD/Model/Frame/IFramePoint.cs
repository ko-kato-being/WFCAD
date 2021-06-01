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
        /// 指定した変換行列をすべての点に適用します
        /// </summary>
        void TransformPoints(Matrix vMatrix);

        /// <summary>
        /// 拡大時の倍率を取得します。
        /// </summary>
        (float, float) GetScale(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 複製します
        /// </summary>
        IFramePoint DeepClone();

        #endregion メソッド

    }
}
