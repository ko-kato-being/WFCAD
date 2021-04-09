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
        /// 始点
        /// </summary>
        PointF StartPoint { get; }

        /// <summary>
        /// 終点
        /// </summary>
        PointF EndPoint { get; }

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
        /// 初期化します
        /// </summary>
        void Initialize(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        void SetPoints(PointF vStartPoint, PointF vEndPoint);

        /// <summary>
        /// 描画します
        /// </summary>
        void Draw(Bitmap vBitmap, Graphics vGraphics);

        /// <summary>
        /// 枠を描画します
        /// </summary>
        void DrawFrame(Bitmap vBitmap, Graphics vGraphics);

        /// <summary>
        /// 移動します
        /// </summary>
        void Move(SizeF vSize);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        void Zoom(SizeF vSize);

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