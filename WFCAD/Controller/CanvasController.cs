using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;
using WFCAD.View;

namespace WFCAD.Controller {
    /// <summary>
    /// キャンバスコントローラー
    /// </summary>
    public class CanvasController : ICanvasController {
        private ICanvasView FCanvasView;
        private ICanvas FCanvas;
        private readonly ISnapshots FSnapshots;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasController(ICanvasView vCanvasView) {
            FCanvasView = vCanvasView;
            FCanvas = new Canvas();
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
        /// 描画します
        /// </summary>
        private void Draw(bool vTakeSnapshot = true) {
            Bitmap wBitmap = FCanvas.Draw(new Bitmap(FCanvasView.Width, FCanvasView.Height));
            if (vTakeSnapshot) FSnapshots.Add(new Snapshot(wBitmap, FCanvas.DeepClone()));
            FCanvasView.RefreshAll(wBitmap);
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(Point vMouseLocation) {
            ICanvas wCanvas = FCanvas.DeepClone();
            if (!FCanvas.IsPreviewing) {
                FCanvas.IsPreviewing = true;
                this.Draw(false);
            }
            wCanvas.Edit(new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y));
            FCanvasView.RefreshPreview(wCanvas.Draw(new Bitmap(FCanvasView.Width, FCanvasView.Height)));
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(IShape vShape, Point vMouseLocation) {
            IShape wShape = vShape.DeepClone();
            wShape.SetPoints(this.MouseDownLocation, vMouseLocation);
            wShape.Color = this.Color;
            FCanvasView.RefreshPreview(wShape.Draw(new Bitmap(FCanvasView.Width, FCanvasView.Height)));
        }

        /// <summary>
        /// 図形を選択します
        /// </summary>
        public void SelectShapes(Point vMouseLocation, bool vIsMultiple) {
            FCanvas.Select(vMouseLocation, vIsMultiple);
            this.Draw(false);
        }

        /// <summary>
        /// すべての図形を選択します
        /// </summary>
        public void AllSelectShapes() {
            FCanvas.AllSelect();
            this.Draw(false);
        }

        /// <summary>
        /// すべての図形の選択を解除します
        /// </summary>
        public void UnselectShapes() {
            FCanvas.Unselect();
            this.Draw(false);
        }

        /// <summary>
        /// 図形を追加します
        /// </summary>
        public void AddShape(IShape vShape) {
            // 二点間の距離が10以下の図形は意図していないとみなして追加しない。
            double wDistance = Utilities.GetDistance(this.MouseDownLocation, this.MouseUpLocation);
            if (wDistance > 10) {
                IShape wShape = vShape.DeepClone();
                wShape.SetPoints(this.MouseDownLocation, this.MouseUpLocation);
                wShape.Color = this.Color;
                FCanvas.Add(wShape);
            }
            this.Draw();
        }

        /// <summary>
        /// 図形を編集します
        /// </summary>
        public void EditShapes(Point vMouseLocation) {
            var wSize = new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y);
            if (wSize.IsEmpty) return;
            FCanvas.Edit(wSize);
            FCanvas.IsPreviewing = false;
            this.Draw();
        }

        /// <summary>
        /// 図形を右に回転させます
        /// </summary>
        public void RotateRightShapes() {
            FCanvas.RotateRight();
            this.Draw();
        }

        /// <summary>
        /// 図形を最前面に移動します
        /// </summary>
        public void MoveToFrontShapes() {
            FCanvas.MoveToFront();
            this.Draw();
        }

        /// <summary>
        /// 図形を最背面に移動します
        /// </summary>
        public void MoveToBackShapes() {
            FCanvas.MoveToBack();
            this.Draw();
        }

        /// <summary>
        /// 図形を複製します
        /// </summary>
        public void CloneShapes() {
            FCanvas.Clone();
            this.Draw();
        }

        /// <summary>
        /// 図形をクリップボードにコピーします
        /// </summary>
        public void CopyShapes(bool vIsCut = false) {
            FCanvas.Copy(vIsCut);
            ISnapshot wSnapshot = FSnapshots.GetLatest();
            if (wSnapshot == null) return;
            wSnapshot.Canvas.Clipboard = new List<IShape>();
            wSnapshot.Canvas.Clipboard.AddRange(FCanvas.Clipboard.Select(x => x.DeepClone()));
            if (vIsCut) this.Draw(false);
        }

        /// <summary>
        /// 図形を貼り付けます
        /// </summary>
        public void PasteShapes() {
            if (FCanvas.Clipboard.Count == 0) return;
            FCanvas.Paste();
            this.Draw();
        }

        /// <summary>
        /// 元に戻します
        /// </summary>
        public void Undo() {
            ISnapshot wSnapshot = FSnapshots.GetBefore();
            if (wSnapshot == null) return;
            FCanvasView.RefreshAll(wSnapshot.Bitmap);
            FCanvas = wSnapshot.Canvas.DeepClone();
        }

        /// <summary>
        /// やり直します
        /// </summary>
        public void Redo() {
            ISnapshot wSnapshot = FSnapshots.GetAfter();
            if (wSnapshot == null) return;
            FCanvasView.RefreshAll(wSnapshot.Bitmap);
            FCanvas = wSnapshot.Canvas.DeepClone();
        }

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        public void RemoveShapes() {
            FCanvas.Remove();
            this.Draw();
        }

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        public void Clear() {
            FCanvas.Clear();
            this.Draw();
        }

        #endregion メソッド

    }
}
