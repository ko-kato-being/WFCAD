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
            this.FToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.FButtonRotate = new System.Windows.Forms.ToolStripButton();
            this.FButtonForeground = new System.Windows.Forms.ToolStripButton();
            this.FButtonBackground = new System.Windows.Forms.ToolStripButton();
            this.FToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FButtonClone = new System.Windows.Forms.ToolStripButton();
            this.FButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.FButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.FButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.FButtonReset = new System.Windows.Forms.ToolStripButton();
            this.FButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.FButtonRectangle = new System.Windows.Forms.ToolStripButton();
            this.FButtonEllipse = new System.Windows.Forms.ToolStripButton();
            this.FButtonLine = new System.Windows.Forms.ToolStripButton();
            this.FMainPictureBox = new System.Windows.Forms.PictureBox();
            this.FSubPictureBox = new System.Windows.Forms.PictureBox();
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
            this.FButtonClone,
            this.FButtonRotate,
            this.FButtonForeground,
            this.FButtonBackground,
            this.FButtonUndo,
            this.FButtonRedo,
            this.FButtonRemove,
            this.FButtonReset});
            this.FToolStrip2.Location = new System.Drawing.Point(0, 54);
            this.FToolStrip2.Name = "FToolStrip2";
            this.FToolStrip2.Size = new System.Drawing.Size(467, 54);
            this.FToolStrip2.TabIndex = 1;
            this.FToolStrip2.Text = "toolStrip2";
            // 
            // FButtonRotate
            // 
            this.FButtonRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonRotate.Name = "FButtonRotate";
            this.FButtonRotate.Size = new System.Drawing.Size(35, 51);
            this.FButtonRotate.Text = "回転";
            // 
            // FButtonForeground
            // 
            this.FButtonForeground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonForeground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonForeground.Name = "FButtonForeground";
            this.FButtonForeground.Size = new System.Drawing.Size(47, 51);
            this.FButtonForeground.Text = "最前面";
            // 
            // FButtonBackground
            // 
            this.FButtonBackground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FButtonBackground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonBackground.Name = "FButtonBackground";
            this.FButtonBackground.Size = new System.Drawing.Size(47, 51);
            this.FButtonBackground.Text = "最背面";
            // 
            // FToolStrip1
            // 
            this.FToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.FToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FButtonSelect,
            this.FButtonRectangle,
            this.FButtonEllipse,
            this.FButtonLine});
            this.FToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.FToolStrip1.Name = "FToolStrip1";
            this.FToolStrip1.Size = new System.Drawing.Size(467, 54);
            this.FToolStrip1.TabIndex = 0;
            this.FToolStrip1.Text = "toolStrip1";
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
            // FButtonSelect
            // 
            this.FButtonSelect.Image = global::WFCAD.Properties.Resources.選択;
            this.FButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FButtonSelect.Name = "FButtonSelect";
            this.FButtonSelect.Size = new System.Drawing.Size(36, 51);
            this.FButtonSelect.Text = "選択";
            this.FButtonSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 423);
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
    }
}

