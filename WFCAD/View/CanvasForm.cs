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
    public partial class CanvasForm : Form, ICanvasView {
        private readonly ICanvasController FCanvasController;
        private readonly List<ToolStripButton> FGroupButtons;
        private Action<MouseEventArgs> FMouseDownAction;
        private Action<MouseEventArgs> FMouseUpAction;
        private Action<MouseEventArgs> FMouseMoveAction;

        int ICanvasView.Width => FMainPictureBox.Width;
        int ICanvasView.Height => FMainPictureBox.Height;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasController = new CanvasController(this);
            // 色の設定ボタンのImageには黒一色の画像を使用しています。
            this.SetColor(Color.Black, FCanvasController.Color);
            FGroupButtons = new List<ToolStripButton> {
                FButtonSelect,
                FButtonRectangle,
                FButtonEllipse,
                FButtonLine,
            };

            #region イベントハンドラの設定

            // 選択ボタン
            FButtonSelect.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FMouseDownAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.SelectShapes(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.EditShapes(vMouseEventArgs.Location);
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.ShowPreview(vMouseEventArgs.Location);
            };
            // 矩形ボタン
            FButtonRectangle.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasController.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.AddShape(new Model.Rectangle(FCanvasController.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.ShowPreview(new Model.Rectangle(FCanvasController.Color), vMouseEventArgs.Location);
            };
            // 円ボタン
            FButtonEllipse.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasController.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.AddShape(new Ellipse(FCanvasController.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.ShowPreview(new Ellipse(FCanvasController.Color), vMouseEventArgs.Location);
            };
            // 線ボタン
            FButtonLine.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasController.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.AddShape(new Line(FCanvasController.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasController.ShowPreview(new Line(FCanvasController.Color), vMouseEventArgs.Location);
            };
            // 色の設定ボタン
            FButtonColor.Click += (sender, e) => {
                using (var wColorDialog = new ColorDialog()) {
                    wColorDialog.Color = FCanvasController.Color;
                    wColorDialog.AllowFullOpen = false;
                    if (wColorDialog.ShowDialog(this) != DialogResult.OK) return;

                    this.SetColor(FCanvasController.Color, wColorDialog.Color);
                    FCanvasController.Color = wColorDialog.Color;
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
            FSubPictureBox.MouseDown += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FCanvasController.MouseDownLocation = e.Location;
                FMouseDownAction?.Invoke(e);
            };
            FSubPictureBox.MouseUp += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FCanvasController.MouseUpLocation = e.Location;
                FMouseUpAction?.Invoke(e);
            };
            FSubPictureBox.MouseMove += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FMouseMoveAction?.Invoke(e);
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
        /// すべて再描画します
        /// </summary>
        public void RefreshAll(Bitmap vBitmap) {
            FMainPictureBox.Image = vBitmap;

            // プレビューをクリアする
            // Image は Dispose されたままだと例外が発生するため null を設定しておく必要がある
            FSubPictureBox.Image?.Dispose();
            FSubPictureBox.Image = null;
        }

        /// <summary>
        /// プレビューを再描画します
        /// </summary>
        public void RefreshPreview(Bitmap vBitmap) {
            FSubPictureBox.Image = vBitmap;
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
