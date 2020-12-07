using System;
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
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Rectangle());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Rectangle(), vMouseEventArgs.Location);
                };
            };
            // 円ボタン
            FButtonEllipse.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Ellipse(), vMouseEventArgs.Location);
                };
            };
            // 線ボタン
            FButtonLine.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Line(), vMouseEventArgs.Location);
                };
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
            FSubPictureBox.MouseMove += (sender, e) => {
                FMouseMoveAction?.Invoke(e);
            };

            // キー入力をハンドリング
            this.KeyDown += (sender, e) => {
                if (e.Control) {
                    switch (e.KeyCode) {
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
                    }
                }
            };

            #endregion イベントハンドラの設定

        }
    }
}
