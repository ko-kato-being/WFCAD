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

        #region フィールド

        private readonly Canvas FCanvas;
        private readonly List<ToolStripButton> FGroupButtons;
        private Color FColor = Color.Orange;
        private Point FMouseDownPoint;
        private Point FMouseUpPoint;
        private Point FMouseCurrentPoint;
        private CommandHistory FCommandHistory;
        private Func<Command> FCurrentCommand;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FPictureBox.Image = new Bitmap(FPictureBox.Width, FPictureBox.Height);
            FCanvas = new Canvas(FPictureBox.Image, FPictureBox.BackColor);
            FCanvas.Updated += this.CanvasRefresh;
            FCommandHistory = new CommandHistory();

            this.InitializeShapeButton(FButtonRectangle, (PointF vStartPoint, PointF vEndPoint, Color vColor) => new Model.Rectangle(vStartPoint, vEndPoint, vColor));
            this.InitializeShapeButton(FButtonEllipse, (PointF vStartPoint, PointF vEndPoint, Color vColor) => throw new NotImplementedException());
            this.InitializeShapeButton(FButtonLine, (PointF vStartPoint, PointF vEndPoint, Color vColor) => throw new NotImplementedException());

            // 色の設定ボタンのImageには黒一色の画像を使用しています。
            this.SetColorIcon(Color.Black, FColor);
            FGroupButtons = new List<ToolStripButton> {
                FButtonRectangle,
                FButtonEllipse,
                FButtonLine,
            };

            FPictureBox.MouseWheel += this.FPictureBox_MouseWheel;
        }

        #endregion コンストラクタ

        #region イベントハンドラ

        private void InitializeShapeButton(ToolStripButton vButton, Func<PointF, PointF, Color, IShape> vCreateShape) {
            vButton.Click += (sender, e) => {
                this.SetGroupButtonsChecked(sender as ToolStripButton);
                FCurrentCommand = () => {
                    return new Command(() => {
                        if (FMouseDownPoint == FMouseUpPoint) return;
                        FCanvas.Add(vCreateShape(FMouseDownPoint, FMouseUpPoint, FColor));
                    }, () => {
                    });
                };
            };
        }

        private void FButtonUndo_Click(object sender, EventArgs e) => FCommandHistory.Undo();
        private void FButtonRedo_Click(object sender, EventArgs e) => FCommandHistory.Redo();
        private void FButtonClone_Click(object sender, EventArgs e) { }
        private void FButtonRotate_Click(object sender, EventArgs e) { }
        private void FButtonForeground_Click(object sender, EventArgs e) { }
        private void FButtonBackground_Click(object sender, EventArgs e) { }
        private void FButtonRemove_Click(object sender, EventArgs e) { }
        private void FButtonReset_Click(object sender, EventArgs e) { }

        private void FPictureBox_MouseDown(object sender, MouseEventArgs e) {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            FMouseDownPoint = e.Location;
            if (FCurrentCommand == null) {
                FCanvas.Select(e.Location, (ModifierKeys & Keys.Control) == Keys.Control);
                if (FCanvas.IsFramePointSelected) {
                    FCurrentCommand = () => {
                        Point wStartPoint = FMouseDownPoint;
                        Point wEndPoint = FMouseUpPoint;
                        return new Command(() => {
                            FCanvas.Select(e.Location, true);
                            FCanvas.Zoom(wStartPoint, wEndPoint);
                        }, () => {
                            var wSize = new Size(wEndPoint.X - wStartPoint.X, wEndPoint.Y - wStartPoint.Y);
                            FCanvas.Select(e.Location + wSize, true);
                            FCanvas.Zoom(wEndPoint, wStartPoint);
                        });
                    };
                } else {
                    FCurrentCommand = () => {
                        Point wStartPoint = FMouseDownPoint;
                        Point wEndPoint = FMouseUpPoint;
                        return new Command(() => {
                            FCanvas.Move(wStartPoint, wEndPoint);
                        }, () => {
                            FCanvas.Move(wEndPoint, wStartPoint);
                        });
                    };
                }
            }
        }

        private void FPictureBox_MouseUp(object sender, MouseEventArgs e) {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            if (FCurrentCommand == null) return;

            FMouseUpPoint = e.Location;
            FCommandHistory.Record(FCurrentCommand.Invoke());
            FCurrentCommand = null;
            this.SetGroupButtonsChecked(null);
        }

        private void FPictureBox_MouseMove(object sender, MouseEventArgs e) {
            FStatusLabelMouse.Text = $"Mouse : {e.Location}";
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            FMouseCurrentPoint = e.Location;
        }
        private void FPictureBox_MouseWheel(object sender, MouseEventArgs e) {
            if ((ModifierKeys & Keys.Control) != Keys.Control) return;
            float wAngle = e.Delta > 0 ? 10F : -10f;
            FCanvas.Rotate(wAngle);
        }

        private void FButtonColor_Click(object sender, EventArgs e) {
            using (var wColorDialog = new ColorDialog()) {
                wColorDialog.Color = FColor;
                wColorDialog.AllowFullOpen = false;
                if (wColorDialog.ShowDialog(this) != DialogResult.OK) return;

                this.SetColorIcon(FColor, wColorDialog.Color);
                FColor = wColorDialog.Color;
            }
        }

        private void CanvasForm_Resize(object sender, EventArgs e) {
            FPictureBox.Image.Dispose();
            FPictureBox.Image = new Bitmap(FPictureBox.Width, FPictureBox.Height);
            FCanvas.UpdateGraphics(FPictureBox.Image);
        }

        private void CanvasForm_KeyDown(object sender, KeyEventArgs e) {
            if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left) return;

            if (e.Control) {
                switch (e.KeyCode) {
                    case Keys.A:
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
                        break;
                }
            }
        }

        #endregion イベントハンドラ

        #region メソッド

        /// <summary>
        /// キャンバスの更新
        /// </summary>
        private void CanvasRefresh() => FPictureBox.Refresh();

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

        #endregion メソッド

    }
}
