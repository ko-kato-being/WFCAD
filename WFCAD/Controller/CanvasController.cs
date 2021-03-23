using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFCAD.Model;

namespace WFCAD.Controller {
    /// <summary>
    /// キャンバスコントローラー
    /// </summary>
    public class CanvasController : ICanvasController {
        private ICanvas FCanvas;
        private readonly ISnapshots FSnapshots;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasController(ICanvas vCanvas) {
            FCanvas = vCanvas;
            FSnapshots = new Snapshots();
        }

        #endregion コンストラクタ

        #region メソッド

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(Point vMouseLocation) {
            //FCanvas.Edit(new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y), true);
        }

        /// <summary>
        /// 図形のプレビューを表示します
        /// </summary>
        public void ShowPreview(IShape vShape, Point vMouseLocation) {
            //IShape wShape = vShape.DeepClone();
            //wShape.SetPoints(this.MouseDownLocation, vMouseLocation);
            //wShape.Color = this.Color;
            //FCanvas.Add(wShape, true);
        }

        /// <summary>
        /// すべての図形を選択します
        /// </summary>
        public void AllSelectShapes() => FCanvas.AllSelect();

        /// <summary>
        /// すべての図形の選択を解除します
        /// </summary>
        public void UnselectShapes() => FCanvas.Unselect();

        /// <summary>
        /// 図形を編集します
        /// </summary>
        public void EditShapes(Point vMouseLocation) {
            //var wSize = new Size(vMouseLocation.X - this.MouseDownLocation.X, vMouseLocation.Y - this.MouseDownLocation.Y);
            //if (wSize.IsEmpty) return;
            //FCanvas.Edit(wSize);
        }

        /// <summary>
        /// 図形を右に回転させます
        /// </summary>
        public void RotateRightShapes() => FCanvas.RotateRight();

        /// <summary>
        /// 図形を最前面に移動します
        /// </summary>
        public void MoveToFrontShapes() => FCanvas.MoveToFront();

        /// <summary>
        /// 図形を最背面に移動します
        /// </summary>
        public void MoveToBackShapes() => FCanvas.MoveToBack();

        /// <summary>
        /// 図形を複製します
        /// </summary>
        public void CloneShapes() => FCanvas.Clone();

        /// <summary>
        /// 図形をクリップボードにコピーします
        /// </summary>
        public void CopyShapes(bool vIsCut = false) {
            FCanvas.Copy(vIsCut);
            ISnapshot wSnapshot = FSnapshots.GetLatest();
            if (wSnapshot == null) return;
            wSnapshot.Canvas.Clipboard = new List<IShape>();
            wSnapshot.Canvas.Clipboard.AddRange(FCanvas.Clipboard.Select(x => x.DeepClone()));
        }

        /// <summary>
        /// 図形を貼り付けます
        /// </summary>
        public void PasteShapes() {
            if (FCanvas.Clipboard.Count == 0) return;
            FCanvas.Paste();
        }

        /// <summary>
        /// 元に戻します
        /// </summary>
        public void Undo() {}

        /// <summary>
        /// やり直します
        /// </summary>
        public void Redo() {}

        /// <summary>
        /// 選択中の図形を削除します
        /// </summary>
        public void RemoveShapes() => FCanvas.Remove();

        /// <summary>
        /// キャンバスをクリアします
        /// </summary>
        public void Clear() => FCanvas.Clear();

        #endregion メソッド

    }
}
