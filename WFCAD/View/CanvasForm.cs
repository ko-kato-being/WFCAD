﻿using System;
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
            this.buttonEllipse.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Ellipse());
            this.buttonRectangle.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Rectangle());
            this.buttonLine.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.AddShape(new Line());
            this.buttonSelect.Click += (sender, e) => FMouseUpAction = (MouseEventArgs vMouseEventArgs) => FCanvasControl.SelectShape(vMouseEventArgs.Location, (ModifierKeys & Keys.Control) == Keys.Control);
            this.buttonRemove.Click += (sender, e) => FCanvasControl.RemoveShopes();
            this.buttonReset.Click += (sender, e) => FCanvasControl.Clear();
        }

        private void FPictureBox_MouseDown(object sender, MouseEventArgs e) {
            FCanvasControl.MouseDownLocation = e.Location;
        }

        private void FPictureBox_MouseUp(object sender, MouseEventArgs e) {
            FCanvasControl.MouseUpLocation = e.Location;
            FMouseUpAction?.Invoke(e);
        }
    }
}
