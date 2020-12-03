namespace WFCAD {
    partial class CanvasForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.FPictureBox = new System.Windows.Forms.PictureBox();
            this.FTopPanel = new System.Windows.Forms.Panel();
            this.FButtonForeground = new System.Windows.Forms.Button();
            this.FButtonBackground = new System.Windows.Forms.Button();
            this.FButtonRedo = new System.Windows.Forms.Button();
            this.FButtonUndo = new System.Windows.Forms.Button();
            this.FButtonRotate = new System.Windows.Forms.Button();
            this.FButtonClone = new System.Windows.Forms.Button();
            this.FButtonSelect = new System.Windows.Forms.RadioButton();
            this.FButtonLine = new System.Windows.Forms.RadioButton();
            this.FButtonRemove = new System.Windows.Forms.Button();
            this.FButtonReset = new System.Windows.Forms.Button();
            this.FButtonRectangle = new System.Windows.Forms.RadioButton();
            this.FButtonEllipse = new System.Windows.Forms.RadioButton();
            this.FGroupBoxMode = new System.Windows.Forms.GroupBox();
            this.FGroupBoxAction = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPictureBox)).BeginInit();
            this.FTopPanel.SuspendLayout();
            this.FGroupBoxMode.SuspendLayout();
            this.FGroupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // FPictureBox
            // 
            this.FPictureBox.BackColor = System.Drawing.Color.White;
            this.FPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FPictureBox.Location = new System.Drawing.Point(0, 177);
            this.FPictureBox.Name = "FPictureBox";
            this.FPictureBox.Size = new System.Drawing.Size(556, 319);
            this.FPictureBox.TabIndex = 0;
            this.FPictureBox.TabStop = false;
            this.FPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FPictureBox_MouseDown);
            this.FPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FPictureBox_MouseUp);
            // 
            // FTopPanel
            // 
            this.FTopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FTopPanel.Controls.Add(this.FGroupBoxAction);
            this.FTopPanel.Controls.Add(this.FGroupBoxMode);
            this.FTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FTopPanel.Location = new System.Drawing.Point(0, 0);
            this.FTopPanel.Name = "FTopPanel";
            this.FTopPanel.Size = new System.Drawing.Size(556, 177);
            this.FTopPanel.TabIndex = 0;
            // 
            // FButtonForeground
            // 
            this.FButtonForeground.Enabled = false;
            this.FButtonForeground.Location = new System.Drawing.Point(168, 18);
            this.FButtonForeground.Name = "FButtonForeground";
            this.FButtonForeground.Size = new System.Drawing.Size(75, 23);
            this.FButtonForeground.TabIndex = 6;
            this.FButtonForeground.Text = "最前面";
            this.FButtonForeground.UseVisualStyleBackColor = true;
            // 
            // FButtonBackground
            // 
            this.FButtonBackground.Enabled = false;
            this.FButtonBackground.Location = new System.Drawing.Point(249, 18);
            this.FButtonBackground.Name = "FButtonBackground";
            this.FButtonBackground.Size = new System.Drawing.Size(75, 23);
            this.FButtonBackground.TabIndex = 7;
            this.FButtonBackground.Text = "最背面";
            this.FButtonBackground.UseVisualStyleBackColor = true;
            // 
            // FButtonRedo
            // 
            this.FButtonRedo.Enabled = false;
            this.FButtonRedo.Location = new System.Drawing.Point(87, 47);
            this.FButtonRedo.Name = "FButtonRedo";
            this.FButtonRedo.Size = new System.Drawing.Size(75, 23);
            this.FButtonRedo.TabIndex = 9;
            this.FButtonRedo.Text = "Redo";
            this.FButtonRedo.UseVisualStyleBackColor = true;
            // 
            // FButtonUndo
            // 
            this.FButtonUndo.Enabled = false;
            this.FButtonUndo.Location = new System.Drawing.Point(6, 47);
            this.FButtonUndo.Name = "FButtonUndo";
            this.FButtonUndo.Size = new System.Drawing.Size(75, 23);
            this.FButtonUndo.TabIndex = 8;
            this.FButtonUndo.Text = "Undo";
            this.FButtonUndo.UseVisualStyleBackColor = true;
            // 
            // FButtonRotate
            // 
            this.FButtonRotate.Enabled = false;
            this.FButtonRotate.Location = new System.Drawing.Point(87, 18);
            this.FButtonRotate.Name = "FButtonRotate";
            this.FButtonRotate.Size = new System.Drawing.Size(75, 23);
            this.FButtonRotate.TabIndex = 5;
            this.FButtonRotate.Text = "回転";
            this.FButtonRotate.UseVisualStyleBackColor = true;
            // 
            // FButtonClone
            // 
            this.FButtonClone.Enabled = false;
            this.FButtonClone.Location = new System.Drawing.Point(6, 18);
            this.FButtonClone.Name = "FButtonClone";
            this.FButtonClone.Size = new System.Drawing.Size(75, 23);
            this.FButtonClone.TabIndex = 4;
            this.FButtonClone.Text = "複製";
            this.FButtonClone.UseVisualStyleBackColor = true;
            // 
            // FButtonSelect
            // 
            this.FButtonSelect.Appearance = System.Windows.Forms.Appearance.Button;
            this.FButtonSelect.Location = new System.Drawing.Point(6, 18);
            this.FButtonSelect.Name = "FButtonSelect";
            this.FButtonSelect.Size = new System.Drawing.Size(75, 23);
            this.FButtonSelect.TabIndex = 3;
            this.FButtonSelect.Text = "選択";
            this.FButtonSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FButtonSelect.UseVisualStyleBackColor = true;
            // 
            // FButtonLine
            // 
            this.FButtonLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.FButtonLine.Location = new System.Drawing.Point(249, 18);
            this.FButtonLine.Name = "FButtonLine";
            this.FButtonLine.Size = new System.Drawing.Size(75, 23);
            this.FButtonLine.TabIndex = 2;
            this.FButtonLine.Text = "線";
            this.FButtonLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FButtonLine.UseVisualStyleBackColor = true;
            // 
            // FButtonRemove
            // 
            this.FButtonRemove.Location = new System.Drawing.Point(168, 47);
            this.FButtonRemove.Name = "FButtonRemove";
            this.FButtonRemove.Size = new System.Drawing.Size(75, 23);
            this.FButtonRemove.TabIndex = 10;
            this.FButtonRemove.Text = "削除";
            this.FButtonRemove.UseVisualStyleBackColor = true;
            // 
            // FButtonReset
            // 
            this.FButtonReset.Location = new System.Drawing.Point(249, 47);
            this.FButtonReset.Name = "FButtonReset";
            this.FButtonReset.Size = new System.Drawing.Size(75, 23);
            this.FButtonReset.TabIndex = 11;
            this.FButtonReset.Text = "リセット";
            this.FButtonReset.UseVisualStyleBackColor = true;
            // 
            // FButtonRectangle
            // 
            this.FButtonRectangle.Appearance = System.Windows.Forms.Appearance.Button;
            this.FButtonRectangle.Location = new System.Drawing.Point(87, 18);
            this.FButtonRectangle.Name = "FButtonRectangle";
            this.FButtonRectangle.Size = new System.Drawing.Size(75, 23);
            this.FButtonRectangle.TabIndex = 1;
            this.FButtonRectangle.Text = "矩形";
            this.FButtonRectangle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FButtonRectangle.UseVisualStyleBackColor = true;
            // 
            // FButtonEllipse
            // 
            this.FButtonEllipse.Appearance = System.Windows.Forms.Appearance.Button;
            this.FButtonEllipse.Location = new System.Drawing.Point(168, 18);
            this.FButtonEllipse.Name = "FButtonEllipse";
            this.FButtonEllipse.Size = new System.Drawing.Size(75, 23);
            this.FButtonEllipse.TabIndex = 0;
            this.FButtonEllipse.Text = "円";
            this.FButtonEllipse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FButtonEllipse.UseVisualStyleBackColor = true;
            // 
            // FGroupBoxMode
            // 
            this.FGroupBoxMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FGroupBoxMode.Controls.Add(this.FButtonEllipse);
            this.FGroupBoxMode.Controls.Add(this.FButtonRectangle);
            this.FGroupBoxMode.Controls.Add(this.FButtonLine);
            this.FGroupBoxMode.Controls.Add(this.FButtonSelect);
            this.FGroupBoxMode.Location = new System.Drawing.Point(11, 11);
            this.FGroupBoxMode.Name = "FGroupBoxMode";
            this.FGroupBoxMode.Size = new System.Drawing.Size(532, 56);
            this.FGroupBoxMode.TabIndex = 12;
            this.FGroupBoxMode.TabStop = false;
            // 
            // FGroupBoxAction
            // 
            this.FGroupBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FGroupBoxAction.Controls.Add(this.FButtonClone);
            this.FGroupBoxAction.Controls.Add(this.FButtonRotate);
            this.FGroupBoxAction.Controls.Add(this.FButtonReset);
            this.FGroupBoxAction.Controls.Add(this.FButtonRemove);
            this.FGroupBoxAction.Controls.Add(this.FButtonRedo);
            this.FGroupBoxAction.Controls.Add(this.FButtonBackground);
            this.FGroupBoxAction.Controls.Add(this.FButtonUndo);
            this.FGroupBoxAction.Controls.Add(this.FButtonForeground);
            this.FGroupBoxAction.Location = new System.Drawing.Point(11, 74);
            this.FGroupBoxAction.Name = "FGroupBoxAction";
            this.FGroupBoxAction.Size = new System.Drawing.Size(532, 84);
            this.FGroupBoxAction.TabIndex = 13;
            this.FGroupBoxAction.TabStop = false;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 496);
            this.Controls.Add(this.FPictureBox);
            this.Controls.Add(this.FTopPanel);
            this.Name = "CanvasForm";
            this.Text = "WFCAD";
            ((System.ComponentModel.ISupportInitialize)(this.FPictureBox)).EndInit();
            this.FTopPanel.ResumeLayout(false);
            this.FGroupBoxMode.ResumeLayout(false);
            this.FGroupBoxAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox FPictureBox;
        private System.Windows.Forms.Panel FTopPanel;
        private System.Windows.Forms.Button FButtonReset;
        private System.Windows.Forms.RadioButton FButtonRectangle;
        private System.Windows.Forms.RadioButton FButtonEllipse;
        private System.Windows.Forms.Button FButtonRemove;
        private System.Windows.Forms.RadioButton FButtonLine;
        private System.Windows.Forms.RadioButton FButtonSelect;
        private System.Windows.Forms.Button FButtonForeground;
        private System.Windows.Forms.Button FButtonBackground;
        private System.Windows.Forms.Button FButtonRedo;
        private System.Windows.Forms.Button FButtonUndo;
        private System.Windows.Forms.Button FButtonRotate;
        private System.Windows.Forms.Button FButtonClone;
        private System.Windows.Forms.GroupBox FGroupBoxAction;
        private System.Windows.Forms.GroupBox FGroupBoxMode;
    }
}

