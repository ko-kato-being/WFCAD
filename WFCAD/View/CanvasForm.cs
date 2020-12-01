using System.Windows.Forms;

namespace WFCAD {
    /// <summary>
    /// キャンバスフォーム
    /// </summary>
    public partial class CanvasForm : Form {
        private readonly ICanvasControl FCanvasControl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanvasForm() {
            InitializeComponent();
            FCanvasControl = new CanvasControl(FPictureBox);
            this.buttonEllipse.Click += (sender, e) => FCanvasControl.CurrentShape = new Ellipse();
            this.buttonRectangle.Click += (sender, e) => FCanvasControl.CurrentShape = new Rectangle();
            this.buttonReset.Click += (sender, e) => FCanvasControl.Clear();
        }

        private void FPictureBox_MouseDown(object sender, MouseEventArgs e) {
            FCanvasControl.MouseDownLocation = e.Location;
        }

        private void FPictureBox_MouseUp(object sender, MouseEventArgs e) {
            FCanvasControl.MouseUpLocation = e.Location;
            FCanvasControl.AddShape();
        }

    }
}
