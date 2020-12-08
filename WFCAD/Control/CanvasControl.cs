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
        private IShapes FShapes;
        private readonly ISnapshots FSnapshots;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasControl(PictureBox vMainPictureBox, PictureBox vSubPictureBox) {
            FMainPictureBox = vMainPictureBox;
            FSubPictureBox = vSubPictureBox;
            FShapes = new Shapes();
            FSnapshots = new Snapshots();
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
        /// 描画色
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 再描画します
        /// </summary>
        public void Refresh(bool vTakeSnapshot = true) {
            Bitmap wBitmap = FShapes.Draw(new Bitmap(FMainPictureBox.Width, FMainPictureBox.Height));
            if (vTakeSnapshot) FSnapshots.Add(new Snapshot(wBitmap, FShapes.DeepClone()));
            FMainPictureBox.Image = wBitmap;

            // プレビューをクリアする
            // Image は Dispose されたままだと例外が発生するため null を設定しておく必要がある
            FSubPictureBox.Image?.Dispose();
            FSubPictureBox.Image = null;
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(Point vMouseLocation) {
            IShapes wShapes = FShapes.DeepClone();
            if (FShapes.Visible) {
                FShapes.Visible = false;
                this.Refresh(false);
            }
            wShapes.Move(new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y));
            FSubPictureBox.Image?.Dispose();
            FSubPictureBox.Image = wShapes.Draw(new Bitmap(FSubPictureBox.Width, FSubPictureBox.Height));
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(IShape vShape, Point vMouseLocation) {
            IShape wShape = vShape.DeepClone();
            wShape.StartPoint = this.MouseDownLocation;
            wShape.EndPoint = vMouseLocation;
            wShape.Option = new Pen(this.Color);
            FSubPictureBox.Image?.Dispose();
            FSubPictureBox.Image = wShape.Draw(new Bitmap(FSubPictureBox.Width, FSubPictureBox.Height));
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
        /// 図形を選択します
        /// </summary>
        public void SelectShapes(Point vMouseLocation, bool vIsMultiple) {
            FShapes.Select(vMouseLocation, vIsMultiple);
            this.Refresh(false);
        }

        /// <summary>
        /// 図形を移動します
        /// </summary>
        public void MoveShapes(Point vMouseLocation) {
            var wSize = new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y);
            if (wSize.IsEmpty) return;
            FShapes.Move(wSize);
            FShapes.Visible = true;
            this.Refresh();
        }

        /// <summary>
        /// 図形を最前面に移動します
        /// </summary>
        public void MoveToFrontShapes() {
            FShapes.MoveToFront();
            this.Refresh();
        }

        /// <summary>
        /// 図形を最背面に移動します
        /// </summary>
        public void MoveToBackShapes() {
            FShapes.MoveToBack();
            this.Refresh();
        }

        /// <summary>
        /// 図形を複製します
        /// </summary>
        public void CloneShapes() {
            FShapes.Clone();
            this.Refresh();
        }

        /// <summary>
        /// 図形をクリップボードにコピーします
        /// </summary>
        public void CopyShapes() {
            FShapes.Copy();
            ISnapshot wSnapshot = FSnapshots.GetLatest();
            if (wSnapshot == null) return;
            wSnapshot.Shapes.Clipboard = new List<IShape>();
            wSnapshot.Shapes.Clipboard.AddRange(FShapes.Clipboard.Select(x => x.DeepClone()));
        }

        /// <summary>
        /// 図形を貼り付けます
        /// </summary>
        public void PasteShapes() {
            if (FShapes.Clipboard.Count == 0) return;
            FShapes.Paste();
            this.Refresh();
        }

        /// <summary>
        /// 元に戻します
        /// </summary>
        public void Undo() {
            ISnapshot wSnapshot = FSnapshots.GetBefore();
            if (wSnapshot == null) return;
            FMainPictureBox.Image = wSnapshot.Bitmap;
            FShapes = wSnapshot.Shapes.DeepClone();
        }

        /// <summary>
        /// やり直します
        /// </summary>
        public void Redo() {
            ISnapshot wSnapshot = FSnapshots.GetAfter();
            if (wSnapshot == null) return;
            FMainPictureBox.Image = wSnapshot.Bitmap;
            FShapes = wSnapshot.Shapes.DeepClone();
        }

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        public void RemoveShapes() {
            FShapes.Remove();
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
