﻿using System;
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
        private readonly List<ToolStripButton> FGroupButtons;
        private Color FColor = Color.Orange;
        private EditCommand FEditCommand;
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
            FCanvas = new Canvas {
                Bitmap = new Bitmap(FMainPictureBox.Width, FMainPictureBox.Height),
            };
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

                // プレビューをクリアする
                // Image は Dispose されたままだと例外が発生するため null を設定しておく必要がある
                FSubPictureBox.Image?.Dispose();
                FSubPictureBox.Image = null;
            };
            //FCanvas.Preview += (Bitmap vBitmap) => {
            //    FSubPictureBox.Image = vBitmap;
            //};

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

            // マウスイベントは前面に配置している FSubPictureBox で処理する
            var wSelectCommand = new SelectCommand(FCanvas);
            FSubPictureBox.MouseDown += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FEditCommand == null) {
                    wSelectCommand.Point = e.Location;
                    wSelectCommand.IsMultiple = (ModifierKeys & Keys.Control) == Keys.Control;
                    wSelectCommand.Execute();
                    if (FCanvas.IsFramePointSelected) {
                        FEditCommand = new ZoomCommand(FCanvas);
                    } else {
                        FEditCommand = new MoveCommand(FCanvas);
                    }
                }
                FEditCommand.StartPoint = e.Location;
            };
            FSubPictureBox.MouseUp += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (FEditCommand == null) return;
                FEditCommand.EndPoint = e.Location;
                FEditCommand.Execute();
                foreach (ToolStripButton wButton in FGroupButtons) {
                    wButton.Checked = false;
                }
                FEditCommand = null;
            };
            FSubPictureBox.MouseMove += (sender, e) => {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                Canvas wPreviewCanvas = FCanvas.DeepClone();
                wPreviewCanvas.Updated += (Bitmap vBitmap) => {
                    FSubPictureBox.Image = vBitmap;
                };
                EditCommand wPreviewCommand;
                if (wPreviewCanvas.IsFramePointSelected) {
                    wPreviewCommand = new ZoomCommand(wPreviewCanvas);
                } else {
                    wPreviewCommand = new MoveCommand(wPreviewCanvas);
                }
                wPreviewCommand.StartPoint = FEditCommand.StartPoint;
                wPreviewCommand.EndPoint = e.Location;
                wPreviewCommand.Execute();
            };

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
