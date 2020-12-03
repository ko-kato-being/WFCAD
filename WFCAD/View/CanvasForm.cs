using System;
using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly ICanvasControl FCanvasControl;
        private Action<MouseEventArgs> FMouseUpAction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasControl = new CanvasControl(FPictureBox);
            this.FButtonEllipse.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse());
            this.FButtonRectangle.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Rectangle());
            this.FButtonLine.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line());
            this.FButtonSelect.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.SelectShape(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
            this.FButtonRemove.Click += (sender, e) => FCanvasControl.RemoveShapes();
            this.FButtonReset.Click += (sender, e) => FCanvasControl.Clear();
        }

        private void FPictureBox_MouseDown(object sender, MouseEventArgs e) {
            FCanvasControl.MouseDownLocation = e.Location;
        }

        private void FPictureBox_MouseUp(object sender, MouseEventArgs e) {
            FCanvasControl.MouseUpLocation = e.Location;
            FMouseUpAction?.Invoke(e);
        }

        private void FPictureBox_MouseMove(object sender, MouseEventArgs e) {
            if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left) {
                FCanvasControl.MoveShapes(e.Location);
            }
            FCanvasControl.CurrentMouseLocation = e.Location;
        }
    }
}
