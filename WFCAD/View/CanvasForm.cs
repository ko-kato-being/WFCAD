using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WFCAD.Control;
using WFCAD.Model.Shape;

namespace WFCAD.View {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly ICanvasControl FCanvasControl;
        private Action<MouseEventArgs> FMouseDownAction;
        private Action<MouseEventArgs> FMouseUpAction;
        private Action<MouseEventArgs> FMouseMoveAction;
        private readonly List<ToolStripButton> FGroupButtons;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasControl = new CanvasControl(FMainPictureBox, FSubPictureBox);
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
                FMouseDownAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.SelectShapes(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.EditShapes(vMouseEventArgs.Location);
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.ShowPreview(vMouseEventArgs.Location);
            };
            // 矩形ボタン
            FButtonRectangle.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Model.Shape.Rectangle(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.ShowPreview(new Model.Shape.Rectangle(FCanvasControl.Color), vMouseEventArgs.Location);
            };
            // 円ボタン
            FButtonEllipse.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.ShowPreview(new Ellipse(FCanvasControl.Color), vMouseEventArgs.Location);
            };
            // 線ボタン
            FButtonLine.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.ShowPreview(new Line(FCanvasControl.Color), vMouseEventArgs.Location);
            };
            // 色の設定ボタン
            FButtonColor.Click += (sender, e) => {
                using (var wColorDialog = new ColorDialog()) {
                    wColorDialog.Color = FCanvasControl.Color;
                    wColorDialog.AllowFullOpen = false;
                    if (wColorDialog.ShowDialog(this) != DialogResult.OK) return;

                    using (var wGraphics = Graphics.FromImage(FButtonColor.Image)) {
                        var wRectangle = new System.Drawing.Rectangle(0, 0, FButtonColor.Image.Width, FButtonColor.Image.Height);
                        using (var wImageAttributes = new ImageAttributes()) {
                            wImageAttributes.SetRemapTable(new ColorMap[] { new ColorMap {
                                OldColor = FCanvasControl.Color,
                                NewColor = wColorDialog.Color,
                            }});
                            wGraphics.DrawImage(FButtonColor.Image, wRectangle, 0, 0, FButtonColor.Image.Width, FButtonColor.Image.Height, GraphicsUnit.Pixel, wImageAttributes);
                        }
                        using (var wPen = new Pen(Color.Black)) {
                            wGraphics.DrawRectangle(wPen, wRectangle);
                        }
                    }
                    FCanvasControl.Color = wColorDialog.Color;
                }
            };

            // Undo
            FButtonUndo.Click += (sender, e) => FCanvasControl.Undo();
            // Redo
            FButtonRedo.Click += (sender, e) => FCanvasControl.Redo();
            // 複製ボタン
            FButtonClone.Click += (sender, e) => FCanvasControl.CloneShapes();
            // 回転
            FButtonRotate.Click += (sender, e) => FCanvasControl.RotateRightShapes();
            // 最前面に移動
            FButtonForeground.Click += (sender, e) => FCanvasControl.MoveToFrontShapes();
            // 最背面に移動
            FButtonBackground.Click += (sender, e) => FCanvasControl.MoveToBackShapes();
            // 削除ボタン
            FButtonRemove.Click += (sender, e) => FCanvasControl.RemoveShapes();
            // リセットボタン
            FButtonReset.Click += (sender, e) => FCanvasControl.Clear();

            // マウスイベントは前面に配置している FSubPictureBox で処理する
            FSubPictureBox.MouseDown += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FCanvasControl.MouseDownLocation = e.Location;
                FMouseDownAction?.Invoke(e);
            };
            FSubPictureBox.MouseUp += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                FCanvasControl.MouseUpLocation = e.Location;
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
                            FCanvasControl.AllSelectShapes();
                            break;
                        case Keys.C:
                            FCanvasControl.CopyShapes();
                            break;
                        case Keys.V:
                            FCanvasControl.PasteShapes();
                            break;
                        case Keys.X:
                            FCanvasControl.CopyShapes(true);
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
                            FCanvasControl.UnselectShapes();
                            break;
                    }
                }
            };

            #endregion イベントハンドラの設定

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
