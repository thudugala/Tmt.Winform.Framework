namespace TMT.Controls.WinForms.Dialogs
{
    partial class MessageDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.buttonOK = new TMT.Controls.WinForms.BaseButton();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.BaseButtonNo = new TMT.Controls.WinForms.BaseButton();
            this.BaseButtonYes = new TMT.Controls.WinForms.BaseButton();
            this.pictureBoxImage = new FontAwesome.Sharp.IconPictureBox();
            this.panelMain.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.pictureBoxImage);
            this.panelMain.Controls.Add(this.flowLayoutPanelButtons);
            this.panelMain.Controls.Add(this.textBoxText);
            this.panelMain.Size = new System.Drawing.Size(525, 194);
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxText.Location = new System.Drawing.Point(154, 13);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(358, 135);
            this.textBoxText.TabIndex = 1;
            this.textBoxText.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(399, 0);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.buttonOK);
            this.flowLayoutPanelButtons.Controls.Add(this.BaseButtonNo);
            this.flowLayoutPanelButtons.Controls.Add(this.BaseButtonYes);
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(13, 154);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(499, 28);
            this.flowLayoutPanelButtons.TabIndex = 2;
            // 
            // BaseButtonNo
            // 
            this.BaseButtonNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseButtonNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BaseButtonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.BaseButtonNo.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BaseButtonNo.FlatAppearance.BorderSize = 0;
            this.BaseButtonNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BaseButtonNo.Location = new System.Drawing.Point(296, 0);
            this.BaseButtonNo.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BaseButtonNo.Name = "BaseButtonNo";
            this.BaseButtonNo.Size = new System.Drawing.Size(100, 27);
            this.BaseButtonNo.TabIndex = 1;
            this.BaseButtonNo.Text = "No";
            this.BaseButtonNo.UseVisualStyleBackColor = false;
            this.BaseButtonNo.Visible = false;
            // 
            // BaseButtonYes
            // 
            this.BaseButtonYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseButtonYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BaseButtonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.BaseButtonYes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BaseButtonYes.FlatAppearance.BorderSize = 0;
            this.BaseButtonYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BaseButtonYes.Location = new System.Drawing.Point(193, 0);
            this.BaseButtonYes.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BaseButtonYes.Name = "BaseButtonYes";
            this.BaseButtonYes.Size = new System.Drawing.Size(100, 27);
            this.BaseButtonYes.TabIndex = 2;
            this.BaseButtonYes.Text = "Yes";
            this.BaseButtonYes.UseVisualStyleBackColor = false;
            this.BaseButtonYes.Visible = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxImage.BackColor = System.Drawing.Color.White;
            this.pictureBoxImage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxImage.IconColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxImage.IconSize = 170;
            this.pictureBoxImage.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxImage.TabIndex = 3;
            this.pictureBoxImage.TabStop = false;
            // 
            // MessageDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.ClientSize = new System.Drawing.Size(525, 258);
            this.Name = "MessageDialog";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxText;
        private BaseButton buttonOK;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private BaseButton BaseButtonNo;
        private BaseButton BaseButtonYes;
        private FontAwesome.Sharp.IconPictureBox pictureBoxImage;
    }
}
