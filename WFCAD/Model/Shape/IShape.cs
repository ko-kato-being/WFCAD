using System.Collections.Generic;
using System.Drawing;
using WFCAD.Model.Frame;

namespace WFCAD.Model.Shape {
    /// <summary>
    /// 図形を表すインターフェース
    /// </summary>
    public interface IShape {

        #region プロパティ

        /// <summary>
        /// 始点
        /// </summary>
        Point StartPoint { get; }

        /// <summary>
        /// 終点
        /// </summary>
        Point EndPoint { get; }

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

        /// <summary>
        /// 枠点リスト
        /// </summary>
        IEnumerable<IFramePoint> FramePoints { get; }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 始点と終点を設定します
        /// </summary>
        void SetPoints(Point vStartPoint, Point vEndPoint);

        /// <summary>
        /// 描画します
        /// </summary>
        Bitmap Draw(Bitmap vBitmap);

        /// <summary>
        /// 移動します
        /// </summary>
        void Move(Size vSize);

        /// <summary>
        /// 拡大・縮小します
        /// </summary>
        void ChangeScale(Size vSize);

        /// <summary>
        /// 右に回転させます
        /// </summary>
        void RotateRight();

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
