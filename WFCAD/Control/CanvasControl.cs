using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスコントロール
    /// </summary>
    public class CanvasControl : ICanvasControl {
        private readonly PictureBox FMainPictureBox;
        private readonly PictureBox FSubPictureBox;
        private List<IShape> FShapes;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasControl(PictureBox vMainPictureBox, PictureBox vSubPictureBox) {
            FShapes = new List<IShape>();
            FMainPictureBox = vMainPictureBox;
            FSubPictureBox = vSubPictureBox;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// マウスダウン位置
        /// </summary>
        public Point MouseDownLocation { get; set; }

        /// <summary>
        /// マウスアップ位置
        /// </summary>
        public Point MouseUpLocation { get; set; }

        /// <summary>
        /// 現在のマウスカーソル位置
        /// </summary>
        public Point CurrentMouseLocation { get; set; }

        /// <summary>
        /// 描画色
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 再描画します
        /// </summary>
        public void Refresh() {
            Image wOldImage = FMainPictureBox.Image;
            FMainPictureBox.Image = new Bitmap(FMainPictureBox.Width, FMainPictureBox.Height);
            wOldImage?.Dispose();
            using (var wGraphics = Graphics.FromImage(FMainPictureBox.Image)) {
                FShapes.ForEach(x => x.Draw(wGraphics));
            }

            // プレビューをクリアする
            // Image を直接 Dispose すると例外が発生する
            // そのため Dispose した後に null を設定しておく必要がある
            wOldImage = FSubPictureBox.Image;
            wOldImage?.Dispose();
            FSubPictureBox.Image = null;

        }

        /// <summary>
        /// 図形を選択します
        /// </summary>
        public void SelectShapes(Point vMouseLocation, bool vIsMultiple) {
            bool wHasSelected = false;
            foreach (IShape wShape in Enumerable.Reverse(FShapes)) {
                if (wHasSelected) {
                    wShape.IsSelected = false;
                } else {
                    bool wIsHit = wShape.IsHit(vMouseLocation);
                    if (vIsMultiple) {
                        wShape.IsSelected = wShape.IsSelected || wIsHit;
                    } else {
                        wShape.IsSelected = wIsHit;
                        if (wShape.IsSelected) {
                            wHasSelected = true;
                        }
                    }
                }
            }
            this.Refresh();
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(IShape vShape, Point vMouseLocation) {
            IShape wShape = vShape.DeepClone();
            wShape.StartPoint = this.MouseDownLocation;
            wShape.EndPoint = vMouseLocation;
            wShape.Option = new Pen(this.Color);
            Image wOldImage = FSubPictureBox.Image;
            FSubPictureBox.Image = new Bitmap(FSubPictureBox.Width, FSubPictureBox.Height);
            wOldImage?.Dispose();
            using (var wGraphics = Graphics.FromImage(FSubPictureBox.Image)) {
                wShape.Draw(wGraphics);
            }
        }

        /// <summary>
        /// 図形を追加します
        /// </summary>
        public void AddShape(IShape vShape) {
            // 二点間の距離が10以下の図形は意図していないとみなして追加しない。
            double wDistance = Utilities.GetDistance(this.MouseDownLocation, this.MouseUpLocation);
            if (wDistance < 10) return;

            IShape wShape = vShape.DeepClone();
            wShape.StartPoint = this.MouseDownLocation;
            wShape.EndPoint = this.MouseUpLocation;
            wShape.Option = new Pen(this.Color);
            FShapes.Add(wShape);
            this.Refresh();
        }

        /// <summary>
        /// 図形を移動します
        /// </summary>
        public void MoveShapes(Point vMouseLocation) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                var wMovingSize = new Size(vMouseLocation.X - this.CurrentMouseLocation.X, vMouseLocation.Y - this.CurrentMouseLocation.Y);
                wShape.StartPoint += wMovingSize;
                wShape.EndPoint += wMovingSize;
            }
            this.Refresh();
        }

        /// <summary>
        /// 図形を複製します
        /// </summary>
        public void CloneShapes() {
            var wClonedShapes = new List<IShape>();
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                IShape wClone = wShape.DeepClone();

                // 選択状態をスイッチします
                wShape.IsSelected = false;
                wClone.IsSelected = true;

                // 右下方向
                var wMovingSize = new Size(10, 10);
                wClone.StartPoint += wMovingSize;
                wClone.EndPoint += wMovingSize;
                wClonedShapes.Add(wClone);
            }
            FShapes.AddRange(wClonedShapes);
            this.Refresh();
        }

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        public void RemoveShapes() {
            FShapes.RemoveAll(x => x.IsSelected);
            this.Refresh();
        }

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        public void Clear() {
            FShapes.Clear();
            this.Refresh();
        }

        #endregion メソッド

    }
}
