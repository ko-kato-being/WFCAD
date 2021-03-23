using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WFCAD.Controller;
using WFCAD.Model;

namespace WFCAD.View {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly Canvas FCanvas;
        private readonly ICanvasController FCanvasController;
        private readonly List<ToolStripButton> FGroupButtons;
        private Color FColor = Color.Orange;
        private AddCommand FAddCommand;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvas = new Canvas {
                Bitmap = new Bitmap(FMainPictureBox.Width, FMainPictureBox.Height)
            };
            FCanvasController = new CanvasController(FCanvas);
            // 色の設定ボタンのImageには黒一色の画像を使用しています。
            this.SetColor(Color.Black, FColor);
            FGroupButtons = new List<ToolStripButton> {
                FButtonRectangle,
                FButtonEllipse,
                FButtonLine,
            };

            #region イベントハンドラの設定

            // キャンバス更新
            FCanvas.Updated += (Bitmap vBitmap) => {
                FMainPictureBox.Image = vBitmap;

                // プレビューをクリアする
                // Image は Dispose されたままだと例外が発生するため null を設定しておく必要がある
                FSubPictureBox.Image?.Dispose();
                FSubPictureBox.Image = null;
            };
            FCanvas.Preview += (Bitmap vBitmap) => {
                FSubPictureBox.Image = vBitmap;
            };

            void SetShapeButton(ToolStripButton vButton, IShape vShape) {
                vButton.Click += (sender, e) => {
                    this.SetGroupButtonsChecked(sender as ToolStripButton);
                    FAddCommand = new AddCommand(FCanvas) { Shape = vShape };
                };
            }
            SetShapeButton(FButtonRectangle, new Model.Rectangle(FColor));
            SetShapeButton(FButtonEllipse, new Ellipse(FColor));
            SetShapeButton(FButtonLine, new Line(FColor));

            // 色の設定ボタン
            FButtonColor.Click += (sender, e) => {
                using (var wColorDialog = new ColorDialog()) {
                    wColorDialog.Color = FColor;
                    wColorDialog.AllowFullOpen = false;
                    if (wColorDialog.ShowDialog(this) != DialogResult.OK) return;

                    this.SetColor(FColor, wColorDialog.Color);
                    FColor = wColorDialog.Color;
                }
            };

            // Undo
            FButtonUndo.Click += (sender, e) => FCanvasController.Undo();
            // Redo
            FButtonRedo.Click += (sender, e) => FCanvasController.Redo();
            // 複製ボタン
            FButtonClone.Click += (sender, e) => FCanvasController.CloneShapes();
            // 回転
            FButtonRotate.Click += (sender, e) => FCanvasController.RotateRightShapes();
            // 最前面に移動
            FButtonForeground.Click += (sender, e) => FCanvasController.MoveToFrontShapes();
            // 最背面に移動
            FButtonBackground.Click += (sender, e) => FCanvasController.MoveToBackShapes();
            // 削除ボタン
            FButtonRemove.Click += (sender, e) => FCanvasController.RemoveShapes();
            // リセットボタン
            FButtonReset.Click += (sender, e) => FCanvasController.Clear();

            // マウスイベントは前面に配置している FSubPictureBox で処理する
            var wSelectCommand = new SelectCommand(FCanvas);
            FSubPictureBox.MouseDown += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FAddCommand == null) {
                    wSelectCommand.Point = e.Location;
                    wSelectCommand.IsMultiple = (ModifierKeys & Keys.Control) == Keys.Control;
                    wSelectCommand.Execute();
                } else {
                    FAddCommand.StartPoint = e.Location;
                }
            };
            FSubPictureBox.MouseUp += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FAddCommand == null) return;
                FAddCommand.EndPoint = e.Location;
                FAddCommand.Execute();
                foreach (ToolStripButton wButton in FGroupButtons) {
                    wButton.Checked = false;
                }
                FAddCommand = null;
            };
            FSubPictureBox.MouseMove += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            };

            // キー入力をハンドリング
            this.KeyDown += (sender, e) => {
                if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left) return;
                if (e.Control) {
                    switch (e.KeyCode) {
                        case Keys.A:
                            FButtonSelect.PerformClick();
                            FCanvasController.AllSelectShapes();
                            break;
                        case Keys.C:
                            FCanvasController.CopyShapes();
                            break;
                        case Keys.V:
                            FCanvasController.PasteShapes();
                            break;
                        case Keys.X:
                            FCanvasController.CopyShapes(true);
                            break;
                        case Keys.Y:
                            FButtonRedo.PerformClick();
                            break;
                        case Keys.Z:
                            FButtonUndo.PerformClick();
                            break;
                    }
                } else {
                    switch (e.KeyCode) {
                        case Keys.Delete:
                            FButtonRemove.PerformClick();
                            break;
                        case Keys.Escape:
                            FCanvasController.UnselectShapes();
                            break;
                    }
                }
            };

            #endregion イベントハンドラの設定

        }

        /// <summary>
        /// 色の設定ボタンの画像を設定します
        /// </summary>
        /// <remarks>
        /// ボタンのImageには一色の画像を設定しています。
        /// その色を置換することで画像自体を加工してアイコンが変わったように見せています
        /// </remarks>
        private void SetColor(Color vOldColor, Color vNewColor) {
            using (var wGraphics = Graphics.FromImage(FButtonColor.Image)) {
                var wRectangle = new System.Drawing.Rectangle(0, 0, FButtonColor.Image.Width, FButtonColor.Image.Height);
                using (var wImageAttributes = new ImageAttributes()) {
                    wImageAttributes.SetRemapTable(new ColorMap[] { new ColorMap {
                        OldColor = vOldColor,
                        NewColor = vNewColor
                    } });
                    wGraphics.DrawImage(FButtonColor.Image, wRectangle, 0, 0, FButtonColor.Image.Width, FButtonColor.Image.Height, GraphicsUnit.Pixel, wImageAttributes);
                }
                using (var wPen = new Pen(Color.Black)) {
                    wGraphics.DrawRectangle(wPen, wRectangle);
                }
            }
        }

        /// <summary>
        /// ボタングループのチェック状態を制御する
        /// </summary>
        private void SetGroupButtonsChecked(ToolStripButton vTarget) {
            foreach (ToolStripButton wButton in FGroupButtons) {
                if (wButton == vTarget) {
                    wButton.Checked = true;
                } else {
                    wButton.Checked = false;
                }
            }
        }
    }
}
