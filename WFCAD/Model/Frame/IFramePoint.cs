﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD.Model {
    /// <summary>
    /// 枠の点を表すインターフェース
    /// </summary>
    public interface IFramePoint {

        /// <summary>
        /// 選択イベント
        /// </summary>
        event Action Selected;

        #region プロパティ

        /// <summary>
        /// 座標
        /// </summary>
        PointF MainPoint { get; }

        /// <summary>
        /// 現在の位置種類
        /// </summary>
        FramePointLocationKindEnum? CurrentLocationKind { get; set; }

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

        /// <summary>
        /// 複製します
        /// </summary>
        IFramePoint DeepClone();

        #endregion メソッド

    }
}
