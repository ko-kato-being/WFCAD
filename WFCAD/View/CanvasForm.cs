using System;
using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly ICanvasControl FCanvasControl;
        private Action<MouseEventArgs> FMouseUpAction;
        private Action<MouseEventArgs> FMouseMoveAction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasControl = new CanvasControl(FMainPictureBox, FSubPictureBox);
            FButtonEllipse.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Ellipse(), vMouseEventArgs.Location);
                };
            };
            FButtonRectangle.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Rectangle());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Rectangle(), vMouseEventArgs.Location);
                };
            };
            FButtonLine.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line());
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.ShowPreview(new Line(), vMouseEventArgs.Location);
                };
            };
            FButtonSelect.Click += (sender, e) => {
                FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.SelectShapes(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
                FMouseMoveAction = (MouseEventArgs vMouseEventArgs) => {
                    if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left) return;
                    FCanvasControl.MoveShapes(vMouseEventArgs.Location);
                };
            };

            FButtonClone.Click += (sender, e) => FCanvasControl.CloneShapes();
            FButtonRemove.Click += (sender, e) => FCanvasControl.RemoveShapes();
            FButtonReset.Click += (sender, e) => FCanvasControl.Clear();

            // マウスイベントは前面に配置している FSubPictureBox で処理する
            FSubPictureBox.MouseDown += (sender, e) => FCanvasControl.MouseDownLocation = e.Location;
            FSubPictureBox.MouseUp += (sender, e) => {
                FCanvasControl.MouseUpLocation = e.Location;
                FMouseUpAction?.Invoke(e);
            };
            FSubPictureBox.MouseMove += (sender, e) => {
                FMouseMoveAction?.Invoke(e);
                FCanvasControl.CurrentMouseLocation = e.Location;
            };
        }
    }
}
