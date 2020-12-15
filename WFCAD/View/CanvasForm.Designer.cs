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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasForm));
            this.FMainPictureBox = new System.Windows.Forms.PictureBox();
            this.FSubPictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.FButtonClone = new System.Windows.Forms.ToolStripButton();
            this.FButtonRotate = new System.Windows.Forms.ToolStripButton();
            this.FButtonForeground = new System.Windows.Forms.ToolStripButton();
            this.FButtonBackground = new System.Windows.Forms.ToolStripButton();
            this.FButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.FButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.FButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.FButtonReset = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.FButtonRectangle = new System.Windows.Forms.ToolStripButton();
            this.FButtonEllipse = new System.Windows.Forms.ToolStripButton();
            this.FButtonLine = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.FMainPictureBox)).BeginInit();
            this.FMainPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FSubPictureBox)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FMainPictureBox
            // 
            this.FMainPictureBox.BackColor = System.Drawing.Color.White;
            this.FMainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FMainPictureBox.Controls.Add(this.FSubPictureBox);
            this.FMainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FMainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FMainPictureBox.Name = "FMainPictureBox";
            this.FMainPictureBox.Size = new System.Drawing.Size(467, 423);
            this.FMainPictureBox.TabIndex = 0;
            this.FMainPictureBox.TabStop = false;
            // 
            // FSubPictureBox
            // 
            this.FSubPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.FSubPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSubPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FSubPictureBox.Name = "FSubPictureBox";
            this.FSubPictureBox.Size = new System.Drawing.Size(465, 421);
            this.FSubPictureBox.TabIndex = 1;
            this.FSubPictureBox.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FButtonClone,
            this.FButtonRotate,
            this.FButtonForeground,
            this.FButtonBackground,
            this.FButtonUndo,
            this.FButtonRedo,
            this.FButtonRemove,
            this.FButtonReset});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(467, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // FButtonClone
            // 
            this.FButtonClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonClone.Image = ((System.Drawing.Image)(resources.GetObject("FButtonClone.Image")));
            this.FButtonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonClone.Name = "FButtonClone";
            this.FButtonClone.Size = new System.Drawing.Size(35, 22);
            this.FButtonClone.Text = "複製";
            // 
            // FButtonRotate
            // 
            this.FButtonRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonRotate.Image = ((System.Drawing.Image)(resources.GetObject("FButtonRotate.Image")));
            this.FButtonRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRotate.Name = "FButtonRotate";
            this.FButtonRotate.Size = new System.Drawing.Size(35, 22);
            this.FButtonRotate.Text = "回転";
            // 
            // FButtonForeground
            // 
            this.FButtonForeground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonForeground.Image = ((System.Drawing.Image)(resources.GetObject("FButtonForeground.Image")));
            this.FButtonForeground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonForeground.Name = "FButtonForeground";
            this.FButtonForeground.Size = new System.Drawing.Size(47, 22);
            this.FButtonForeground.Text = "最前面";
            // 
            // FButtonBackground
            // 
            this.FButtonBackground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonBackground.Image = ((System.Drawing.Image)(resources.GetObject("FButtonBackground.Image")));
            this.FButtonBackground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonBackground.Name = "FButtonBackground";
            this.FButtonBackground.Size = new System.Drawing.Size(47, 22);
            this.FButtonBackground.Text = "最背面";
            // 
            // FButtonUndo
            // 
            this.FButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("FButtonUndo.Image")));
            this.FButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonUndo.Name = "FButtonUndo";
            this.FButtonUndo.Size = new System.Drawing.Size(40, 22);
            this.FButtonUndo.Text = "Undo";
            // 
            // FButtonRedo
            // 
            this.FButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("FButtonRedo.Image")));
            this.FButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRedo.Name = "FButtonRedo";
            this.FButtonRedo.Size = new System.Drawing.Size(38, 22);
            this.FButtonRedo.Text = "Redo";
            // 
            // FButtonRemove
            // 
            this.FButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonRemove.Image = ((System.Drawing.Image)(resources.GetObject("FButtonRemove.Image")));
            this.FButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRemove.Name = "FButtonRemove";
            this.FButtonRemove.Size = new System.Drawing.Size(35, 22);
            this.FButtonRemove.Text = "削除";
            // 
            // FButtonReset
            // 
            this.FButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("FButtonReset.Image")));
            this.FButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonReset.Name = "FButtonReset";
            this.FButtonReset.Size = new System.Drawing.Size(45, 22);
            this.FButtonReset.Text = "リセット";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FButtonSelect,
            this.FButtonRectangle,
            this.FButtonEllipse,
            this.FButtonLine});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(467, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FButtonSelect
            // 
            this.FButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("FButtonSelect.Image")));
            this.FButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonSelect.Name = "FButtonSelect";
            this.FButtonSelect.Size = new System.Drawing.Size(35, 22);
            this.FButtonSelect.Text = "選択";
            // 
            // FButtonRectangle
            // 
            this.FButtonRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonRectangle.Image = ((System.Drawing.Image)(resources.GetObject("FButtonRectangle.Image")));
            this.FButtonRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRectangle.Name = "FButtonRectangle";
            this.FButtonRectangle.Size = new System.Drawing.Size(35, 22);
            this.FButtonRectangle.Text = "矩形";
            // 
            // FButtonEllipse
            // 
            this.FButtonEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonEllipse.Image = ((System.Drawing.Image)(resources.GetObject("FButtonEllipse.Image")));
            this.FButtonEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonEllipse.Name = "FButtonEllipse";
            this.FButtonEllipse.Size = new System.Drawing.Size(23, 22);
            this.FButtonEllipse.Text = "円";
            // 
            // FButtonLine
            // 
            this.FButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonLine.Image = ((System.Drawing.Image)(resources.GetObject("FButtonLine.Image")));
            this.FButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonLine.Name = "FButtonLine";
            this.FButtonLine.Size = new System.Drawing.Size(23, 22);
            this.FButtonLine.Text = "線";
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 423);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.FMainPictureBox);
            this.KeyPreview = true;
            this.Name = "CanvasForm";
            this.Text = "WFCAD";
            ((System.ComponentModel.ISupportInitialize)(this.FMainPictureBox)).EndInit();
            this.FMainPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FSubPictureBox)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FMainPictureBox;
        private System.Windows.Forms.PictureBox FSubPictureBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton FButtonClone;
        private System.Windows.Forms.ToolStripButton FButtonRotate;
        private System.Windows.Forms.ToolStripButton FButtonForeground;
        private System.Windows.Forms.ToolStripButton FButtonBackground;
        private System.Windows.Forms.ToolStripButton FButtonUndo;
        private System.Windows.Forms.ToolStripButton FButtonRedo;
        private System.Windows.Forms.ToolStripButton FButtonRemove;
        private System.Windows.Forms.ToolStripButton FButtonReset;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton FButtonSelect;
        private System.Windows.Forms.ToolStripButton FButtonRectangle;
        private System.Windows.Forms.ToolStripButton FButtonEllipse;
        private System.Windows.Forms.ToolStripButton FButtonLine;
    }
}

