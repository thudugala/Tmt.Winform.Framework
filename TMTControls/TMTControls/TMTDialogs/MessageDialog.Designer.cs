namespace TMTControls.TMTDialogs
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
            this.tmtButtonOK = new TMTControls.TMTButton();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.tmtButtonNo = new TMTControls.TMTButton();
            this.tmtButtonYes = new TMTControls.TMTButton();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
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
            // tmtButtonOK
            // 
            this.tmtButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tmtButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonOK.FlatAppearance.BorderSize = 0;
            this.tmtButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonOK.Location = new System.Drawing.Point(399, 0);
            this.tmtButtonOK.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tmtButtonOK.Name = "tmtButtonOK";
            this.tmtButtonOK.Size = new System.Drawing.Size(100, 27);
            this.tmtButtonOK.TabIndex = 0;
            this.tmtButtonOK.Text = "OK";
            this.tmtButtonOK.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.tmtButtonOK);
            this.flowLayoutPanelButtons.Controls.Add(this.tmtButtonNo);
            this.flowLayoutPanelButtons.Controls.Add(this.tmtButtonYes);
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(13, 154);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(499, 28);
            this.flowLayoutPanelButtons.TabIndex = 2;
            // 
            // tmtButtonNo
            // 
            this.tmtButtonNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.tmtButtonNo.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonNo.FlatAppearance.BorderSize = 0;
            this.tmtButtonNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonNo.Location = new System.Drawing.Point(296, 0);
            this.tmtButtonNo.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tmtButtonNo.Name = "tmtButtonNo";
            this.tmtButtonNo.Size = new System.Drawing.Size(100, 27);
            this.tmtButtonNo.TabIndex = 1;
            this.tmtButtonNo.Text = "No";
            this.tmtButtonNo.UseVisualStyleBackColor = false;
            this.tmtButtonNo.Visible = false;
            // 
            // tmtButtonYes
            // 
            this.tmtButtonYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.tmtButtonYes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonYes.FlatAppearance.BorderSize = 0;
            this.tmtButtonYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonYes.Location = new System.Drawing.Point(193, 0);
            this.tmtButtonYes.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tmtButtonYes.Name = "tmtButtonYes";
            this.tmtButtonYes.Size = new System.Drawing.Size(100, 27);
            this.tmtButtonYes.TabIndex = 2;
            this.tmtButtonYes.Text = "Yes";
            this.tmtButtonYes.UseVisualStyleBackColor = false;
            this.tmtButtonYes.Visible = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxImage.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 3;
            this.pictureBoxImage.TabStop = false;
            // 
            // MessageDialog
            // 
            this.AcceptButton = this.tmtButtonOK;
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
        private TMTButton tmtButtonOK;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private TMTButton tmtButtonNo;
        private TMTButton tmtButtonYes;
        private System.Windows.Forms.PictureBox pictureBoxImage;
    }
}
