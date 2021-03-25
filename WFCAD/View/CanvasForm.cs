using System;
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
        private Canvas FPreviewCanvas;
        private readonly List<ToolStripButton> FGroupButtons;
        private Color FColor = Color.Orange;
        private EditCommand FEditCommand;
        private EditCommand FPreviewCommand;
        private SelectCommand FSelectCommand;
        private ICommand FCloneCommand;
        private ICommand FMoveToFrontCommand;
        private ICommand FMoveToBackCommand;
        private ICommand FRemoveCommand;
        private ICommand FClearCommand;
        private ICommand FAllSelectCommand;
        private ICommand FUnselectCommand;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvas = new Canvas(FMainPictureBox.Width, FMainPictureBox.Height);
            FPreviewCanvas = new Canvas(FMainPictureBox.Width, FMainPictureBox.Height);
            FSelectCommand = new SelectCommand(FCanvas);
            FCloneCommand = new CloneCommand(FCanvas);
            FRemoveCommand = new RemoveCommand(FCanvas);
            FClearCommand = new ClearCommand(FCanvas);
            FMoveToFrontCommand = new MoveToFrontCommand(FCanvas);
            FMoveToBackCommand = new MoveToBackCommand(FCanvas);
            FAllSelectCommand = new AllSelectCommand(FCanvas);
            FUnselectCommand = new UnselectCommand(FCanvas);

            // 色の設定ボタンのImageには黒一色の画像を使用しています。
            this.SetColorIcon(Color.Black, FColor);
            FGroupButtons = new List<ToolStripButton> {
                FButtonRectangle,
                FButtonEllipse,
                FButtonLine,
            };

            #region イベントハンドラの設定

            // キャンバス更新
            FCanvas.Updated += (Bitmap vBitmap) => {
                FMainPictureBox.Image = vBitmap;
                FMainPictureBox.Refresh();
            };
            FPreviewCanvas.Updated += (Bitmap vBitmap) => {
                FMainPictureBox.Image = vBitmap;
                FMainPictureBox.Refresh();
            };

            void SetShapeButton(ToolStripButton vButton, Func<Color, IShape> vCreateShape) {
                vButton.Click += (sender, e) => {
                    this.SetGroupButtonsChecked(sender as ToolStripButton);
                    FUnselectCommand.Execute();
                    FEditCommand = new AddCommand(FCanvas) { Shape = vCreateShape(FColor) };
                };
            }
            SetShapeButton(FButtonRectangle, (Color vColor) => new Model.Rectangle(vColor));
            SetShapeButton(FButtonEllipse, (Color vColor) => new Ellipse(vColor));
            SetShapeButton(FButtonLine, (Color vColor) => new Line(vColor));

            // 色の設定ボタン
            FButtonColor.Click += (sender, e) => {
                using (var wColorDialog = new ColorDialog()) {
                    wColorDialog.Color = FColor;
                    wColorDialog.AllowFullOpen = false;
                    if (wColorDialog.ShowDialog(this) != DialogResult.OK) return;

                    this.SetColorIcon(FColor, wColorDialog.Color);
                    FColor = wColorDialog.Color;
                }
            };

            // Undo
            FButtonUndo.Click += (sender, e) => { };
            // Redo
            FButtonRedo.Click += (sender, e) => { };
            // 複製ボタン
            FButtonClone.Click += (sender, e) => FCloneCommand.Execute();
            // 回転
            FButtonRotate.Click += (sender, e) => { };
            // 最前面に移動
            FButtonForeground.Click += (sender, e) => FMoveToFrontCommand.Execute();
            // 最背面に移動
            FButtonBackground.Click += (sender, e) => FMoveToBackCommand.Execute();
            // 削除ボタン
            FButtonRemove.Click += (sender, e) => FRemoveCommand.Execute();
            // リセットボタン
            FButtonReset.Click += (sender, e) => FClearCommand.Execute();

            FMainPictureBox.MouseDown += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FEditCommand == null) {
                    FSelectCommand.Point = e.Location;
                    FSelectCommand.IsMultiple = (ModifierKeys & Keys.Control) == Keys.Control;
                    FSelectCommand.Execute();
                    if (FCanvas.IsFramePointSelected) {
                        FEditCommand = new ZoomCommand(FCanvas);
                    } else {
                        FEditCommand = new MoveCommand(FCanvas);
                    }
                }
                FEditCommand.StartPoint = e.Location;
                FPreviewCanvas.Bitmap?.Dispose();
                FPreviewCanvas.Bitmap = new Bitmap((Image)FCanvas.Bitmap.Clone());
                FPreviewCanvas.Width = FMainPictureBox.Width;
                FPreviewCanvas.Height = FMainPictureBox.Height;
                FPreviewCommand = FEditCommand.DeepClone(FPreviewCanvas);
            };
            FMainPictureBox.MouseUp += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FEditCommand == null) return;
                FEditCommand.EndPoint = e.Location;
                FEditCommand.Execute();
                this.SetGroupButtonsChecked(null);
                FEditCommand = null;
                FPreviewCanvas.Bitmap.Dispose();
                FPreviewCanvas.Bitmap = null;
            };
            FMainPictureBox.MouseMove += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FPreviewCommand.EndPoint = e.Location;
                FPreviewCommand.Execute();
            };
            FMainPictureBox.Resize += (sender, e) => {
                FCanvas.Width = FMainPictureBox.Width;
                FCanvas.Height = FMainPictureBox.Height;
            };

            #region キー入力

            // キー入力をハンドリング
            this.KeyDown += (sender, e) => {
                if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left) return;
                if (e.Control) {
                    switch (e.KeyCode) {
                        case Keys.A:
                            FAllSelectCommand.Execute();
                            break;
                        case Keys.C:
                            break;
                        case Keys.V:
                            break;
                        case Keys.X:
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
                            FUnselectCommand.Execute();
                            break;
                    }
                }
            };

            #endregion キー入力

            #endregion イベントハンドラの設定

        }

        /// <summary>
        /// 色の設定ボタンの画像を設定します
        /// </summary>
        /// <remarks>
        /// ボタンのImageには一色の画像を設定しています。
        /// その色を置換することで画像自体を加工してアイコンが変わったように見せています
        /// </remarks>
        private void SetColorIcon(Color vOldColor, Color vNewColor) {
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
