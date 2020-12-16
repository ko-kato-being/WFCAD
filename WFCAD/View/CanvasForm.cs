using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly ICanvasControl FCanvasControl;
        private Action<MouseEventArgs> FMouseDownAction;
        private Action<MouseEventArgs> FMouseUpAction;
        private Action<MouseEventArgs> FMouseMoveAction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasControl = new CanvasControl(FMainPictureBox, FSubPictureBox);

            #region イベントハンドラの設定

            // 選択ボタン
            FButtonSelect.Click += (sender, e) => {
                FMouseDownAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.SelectShapes(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.MoveShapes(vMouseEventArgs.Location);
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(vMouseEventArgs.Location);
                };
            };
            // 矩形ボタン
            FButtonRectangle.Click += (sender, e) => {
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Rectangle(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Rectangle(FCanvasControl.Color), vMouseEventArgs.Location);
                };
            };
            // 円ボタン
            FButtonEllipse.Click += (sender, e) => {
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Ellipse(FCanvasControl.Color), vMouseEventArgs.Location);
                };
            };
            // 線ボタン
            FButtonLine.Click += (sender, e) => {
                FCanvasControl.UnselectShapes();
                FMouseDownAction = null;
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line(FCanvasControl.Color));
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Line(FCanvasControl.Color), vMouseEventArgs.Location);
                };
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

            // 最前面に移動
            FButtonForeground.Click += (sender, e) => FCanvasControl.MoveToFrontShapes();
            // 最背面に移動
            FButtonBackground.Click += (sender, e) => FCanvasControl.MoveToBackShapes();
            // 複製ボタン
            FButtonClone.Click += (sender, e) => FCanvasControl.CloneShapes();
            // Undo
            FButtonUndo.Click += (sender, e) => FCanvasControl.Undo();
            // Redo
            FButtonRedo.Click += (sender, e) => FCanvasControl.Redo();
            // 削除ボタン
            FButtonRemove.Click += (sender, e) => FCanvasControl.RemoveShapes();
            // リセットボタン
            FButtonReset.Click += (sender, e) => FCanvasControl.Clear();

            // マウスイベントは前面に配置している FSubPictureBox で処理する
            FSubPictureBox.MouseDown += (sender, e) => {
                FCanvasControl.MouseDownLocation = e.Location;
                FMouseDownAction?.Invoke(e);
            };
            FSubPictureBox.MouseUp += (sender, e) => {
                FCanvasControl.MouseUpLocation = e.Location;
                FMouseUpAction?.Invoke(e);
            };
            FSubPictureBox.MouseMove += (sender, e) => FMouseMoveAction?.Invoke(e);

            // キー入力をハンドリング
            this.KeyDown += (sender, e) => {
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
    }
}
