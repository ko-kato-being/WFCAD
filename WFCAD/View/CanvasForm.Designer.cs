namespace WFCAD.View {
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
            this.FToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.FButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.FButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonClone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonForeground = new System.Windows.Forms.ToolStripButton();
            this.FButtonBackground = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.FButtonReset = new System.Windows.Forms.ToolStripButton();
            this.FToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonRectangle = new System.Windows.Forms.ToolStripButton();
            this.FButtonEllipse = new System.Windows.Forms.ToolStripButton();
            this.FButtonLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FButtonColor = new System.Windows.Forms.ToolStripButton();
            this.FMainPictureBox = new System.Windows.Forms.PictureBox();
            this.FSubPictureBox = new System.Windows.Forms.PictureBox();
            this.FButtonSave = new System.Windows.Forms.ToolStripButton();
            this.FButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.FToolStrip2.SuspendLayout();
            this.FToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FMainPictureBox)).BeginInit();
            this.FMainPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FSubPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FToolStrip2
            // 
            this.FToolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.FToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FButtonUndo,
            this.FButtonRedo,
            this.toolStripSeparator3,
            this.FButtonClone,
            this.toolStripSeparator4,
            this.FButtonRotate,
            this.toolStripSeparator5,
            this.FButtonForeground,
            this.FButtonBackground,
            this.toolStripSeparator6,
            this.FButtonSave,
            this.FButtonLoad,
            this.toolStripSeparator7,
            this.FButtonRemove,
            this.FButtonReset});
            this.FToolStrip2.Location = new System.Drawing.Point(0, 54);
            this.FToolStrip2.Name = "FToolStrip2";
            this.FToolStrip2.Size = new System.Drawing.Size(558, 54);
            this.FToolStrip2.TabIndex = 1;
            this.FToolStrip2.Text = "toolStrip2";
            // 
            // FButtonUndo
            // 
            this.FButtonUndo.Image = global::WFCAD.Properties.Resources.元に戻す;
            this.FButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonUndo.Name = "FButtonUndo";
            this.FButtonUndo.Size = new System.Drawing.Size(54, 51);
            this.FButtonUndo.Text = "元に戻す";
            this.FButtonUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FButtonRedo
            // 
            this.FButtonRedo.Image = global::WFCAD.Properties.Resources.やり直し;
            this.FButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRedo.Name = "FButtonRedo";
            this.FButtonRedo.Size = new System.Drawing.Size(49, 51);
            this.FButtonRedo.Text = "やり直し";
            this.FButtonRedo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonClone
            // 
            this.FButtonClone.Image = global::WFCAD.Properties.Resources.複製;
            this.FButtonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonClone.Name = "FButtonClone";
            this.FButtonClone.Size = new System.Drawing.Size(36, 51);
            this.FButtonClone.Text = "複製";
            this.FButtonClone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonRotate
            // 
            this.FButtonRotate.Image = global::WFCAD.Properties.Resources.回転;
            this.FButtonRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRotate.Name = "FButtonRotate";
            this.FButtonRotate.Size = new System.Drawing.Size(36, 51);
            this.FButtonRotate.Text = "回転";
            this.FButtonRotate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonForeground
            // 
            this.FButtonForeground.Image = global::WFCAD.Properties.Resources.最前面;
            this.FButtonForeground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonForeground.Name = "FButtonForeground";
            this.FButtonForeground.Size = new System.Drawing.Size(47, 51);
            this.FButtonForeground.Text = "最前面";
            this.FButtonForeground.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FButtonBackground
            // 
            this.FButtonBackground.Image = global::WFCAD.Properties.Resources.最背面;
            this.FButtonBackground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonBackground.Name = "FButtonBackground";
            this.FButtonBackground.Size = new System.Drawing.Size(47, 51);
            this.FButtonBackground.Text = "最背面";
            this.FButtonBackground.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonRemove
            // 
            this.FButtonRemove.Image = global::WFCAD.Properties.Resources.削除;
            this.FButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRemove.Name = "FButtonRemove";
            this.FButtonRemove.Size = new System.Drawing.Size(36, 51);
            this.FButtonRemove.Text = "削除";
            this.FButtonRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FButtonReset
            // 
            this.FButtonReset.Image = global::WFCAD.Properties.Resources.リセット;
            this.FButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonReset.Name = "FButtonReset";
            this.FButtonReset.Size = new System.Drawing.Size(45, 51);
            this.FButtonReset.Text = "リセット";
            this.FButtonReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FToolStrip1
            // 
            this.FToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.FToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FButtonSelect,
            this.toolStripSeparator1,
            this.FButtonRectangle,
            this.FButtonEllipse,
            this.FButtonLine,
            this.toolStripSeparator2,
            this.FButtonColor});
            this.FToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.FToolStrip1.Name = "FToolStrip1";
            this.FToolStrip1.Size = new System.Drawing.Size(558, 54);
            this.FToolStrip1.TabIndex = 0;
            this.FToolStrip1.Text = "toolStrip1";
            // 
            // FButtonSelect
            // 
            this.FButtonSelect.Image = global::WFCAD.Properties.Resources.選択;
            this.FButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonSelect.Name = "FButtonSelect";
            this.FButtonSelect.Size = new System.Drawing.Size(36, 51);
            this.FButtonSelect.Text = "選択";
            this.FButtonSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonRectangle
            // 
            this.FButtonRectangle.Image = global::WFCAD.Properties.Resources.矩形;
            this.FButtonRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRectangle.Name = "FButtonRectangle";
            this.FButtonRectangle.Size = new System.Drawing.Size(36, 51);
            this.FButtonRectangle.Text = "矩形";
            this.FButtonRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FButtonEllipse
            // 
            this.FButtonEllipse.Image = global::WFCAD.Properties.Resources.円;
            this.FButtonEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonEllipse.Name = "FButtonEllipse";
            this.FButtonEllipse.Size = new System.Drawing.Size(36, 51);
            this.FButtonEllipse.Text = "円";
            this.FButtonEllipse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FButtonLine
            // 
            this.FButtonLine.Image = global::WFCAD.Properties.Resources.線;
            this.FButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonLine.Name = "FButtonLine";
            this.FButtonLine.Size = new System.Drawing.Size(36, 51);
            this.FButtonLine.Text = "線";
            this.FButtonLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // FButtonColor
            // 
            this.FButtonColor.Image = global::WFCAD.Properties.Resources.色;
            this.FButtonColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonColor.Name = "FButtonColor";
            this.FButtonColor.Size = new System.Drawing.Size(57, 51);
            this.FButtonColor.Text = "色の設定";
            this.FButtonColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FMainPictureBox
            // 
            this.FMainPictureBox.BackColor = System.Drawing.Color.White;
            this.FMainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FMainPictureBox.Controls.Add(this.FSubPictureBox);
            this.FMainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FMainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FMainPictureBox.Name = "FMainPictureBox";
            this.FMainPictureBox.Size = new System.Drawing.Size(558, 483);
            this.FMainPictureBox.TabIndex = 0;
            this.FMainPictureBox.TabStop = false;
            // 
            // FSubPictureBox
            // 
            this.FSubPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.FSubPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSubPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FSubPictureBox.Name = "FSubPictureBox";
            this.FSubPictureBox.Size = new System.Drawing.Size(556, 481);
            this.FSubPictureBox.TabIndex = 1;
            this.FSubPictureBox.TabStop = false;
            // 
            // FButtonSave
            // 
            this.FButtonSave.Enabled = false;
            this.FButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonSave.Name = "FButtonSave";
            this.FButtonSave.Size = new System.Drawing.Size(35, 51);
            this.FButtonSave.Text = "保存";
            this.FButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FButtonSave.Visible = false;
            // 
            // FButtonLoad
            // 
            this.FButtonLoad.Enabled = false;
            this.FButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonLoad.Name = "FButtonLoad";
            this.FButtonLoad.Size = new System.Drawing.Size(35, 51);
            this.FButtonLoad.Text = "読込";
            this.FButtonLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FButtonLoad.Visible = false;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 54);
            this.toolStripSeparator7.Visible = false;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 483);
            this.Controls.Add(this.FToolStrip2);
            this.Controls.Add(this.FToolStrip1);
            this.Controls.Add(this.FMainPictureBox);
            this.KeyPreview = true;
            this.Name = "CanvasForm";
            this.Text = "WFCAD";
            this.FToolStrip2.ResumeLayout(false);
            this.FToolStrip2.PerformLayout();
            this.FToolStrip1.ResumeLayout(false);
            this.FToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FMainPictureBox)).EndInit();
            this.FMainPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FSubPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FMainPictureBox;
        private System.Windows.Forms.PictureBox FSubPictureBox;
        private System.Windows.Forms.ToolStrip FToolStrip2;
        private System.Windows.Forms.ToolStripButton FButtonClone;
        private System.Windows.Forms.ToolStripButton FButtonRotate;
        private System.Windows.Forms.ToolStripButton FButtonForeground;
        private System.Windows.Forms.ToolStripButton FButtonBackground;
        private System.Windows.Forms.ToolStripButton FButtonUndo;
        private System.Windows.Forms.ToolStripButton FButtonRedo;
        private System.Windows.Forms.ToolStripButton FButtonRemove;
        private System.Windows.Forms.ToolStripButton FButtonReset;
        private System.Windows.Forms.ToolStrip FToolStrip1;
        private System.Windows.Forms.ToolStripButton FButtonSelect;
        private System.Windows.Forms.ToolStripButton FButtonRectangle;
        private System.Windows.Forms.ToolStripButton FButtonEllipse;
        private System.Windows.Forms.ToolStripButton FButtonLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton FButtonColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton FButtonSave;
        private System.Windows.Forms.ToolStripButton FButtonLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}

