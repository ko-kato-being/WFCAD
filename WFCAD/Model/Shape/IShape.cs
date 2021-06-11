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
        /// メインパス
        /// </summary>
        GraphicsPath MainPath { get; }

        /// <summary>
        /// サブパス
        /// </summary>
        GraphicsPath SubPath { get; }

        /// <summary>
        /// 変換行列
        /// </summary>
        Matrix Matrix { get; set; }

        /// <summary>
        /// 次元数
        /// </summary>
        int Dimensionality { get; }

        /// <summary>
        /// 中心点
        /// </summary>
        PointF CenterPoint { get; }

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
        void Draw(Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        void DrawFrame(Graphics vGraphics);

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

        #endregion メソッド

    }
}