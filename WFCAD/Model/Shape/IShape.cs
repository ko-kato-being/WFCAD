using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 図形を表すインターフェース
    /// </summary>
    public interface IShape {

        #region プロパティ

        /// <summary>
        /// 次元数
        /// </summary>
        int Dimensionality { get; }

        /// <summary>
        /// 選択されているか
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 表示色
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// 枠点リスト
        /// </summary>
        IEnumerable<IFramePoint> FramePoints { get; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        void Draw(Graphics vGraphics, bool vIsDrawFrame);

        /// <summary>
        /// アフィン変換を適用します
        /// </summary>
        void ApplyAffine();

        /// <summary>
        /// 移動します
        /// </summary>
        void Move(SizeF vSize);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        void Zoom(PointF vStartPoint, PointF vEndPoint, bool vIsPreview = false);

        /// <summary>
        /// 回転します
        /// </summary>
        void Rotate(float vAngle);

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        bool IsHit(PointF vCoordinate);

        /// <summary>
        /// 複製します
        /// </summary>
        IShape DeepClone();

        #endregion メソッド

    }
}