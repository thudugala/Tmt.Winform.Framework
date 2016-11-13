namespace TMTControls
{
    partial class TMTErrorDialog
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
            this.tmtButtonOK = new TMTControls.TMTButton();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.linkLabelCopyToClipboard = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Image = global::TMTControls.Properties.Resources.error;
            this.labelHeader.Size = new System.Drawing.Size(476, 60);
            this.labelHeader.Text = "Error";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.linkLabelCopyToClipboard);
            this.panelMain.Controls.Add(this.textBoxMessage);
            this.panelMain.Controls.Add(this.tmtButtonOK);
            this.panelMain.Size = new System.Drawing.Size(476, 178);
            // 
            // tmtButtonOK
            // 
            this.tmtButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonOK.AutoSize = true;
            this.tmtButtonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tmtButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonOK.FlatAppearance.BorderSize = 0;
            this.tmtButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonOK.Location = new System.Drawing.Point(388, 135);
            this.tmtButtonOK.Name = "tmtButtonOK";
            this.tmtButtonOK.Size = new System.Drawing.Size(75, 30);
            this.tmtButtonOK.TabIndex = 0;
            this.tmtButtonOK.Text = "OK";
            this.tmtButtonOK.UseVisualStyleBackColor = false;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Location = new System.Drawing.Point(13, 13);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(450, 116);
            this.textBoxMessage.TabIndex = 1;
            // 
            // linkLabelCopyToClipboard
            // 
            this.linkLabelCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelCopyToClipboard.AutoSize = true;
            this.linkLabelCopyToClipboard.Location = new System.Drawing.Point(13, 148);
            this.linkLabelCopyToClipboard.Name = "linkLabelCopyToClipboard";
            this.linkLabelCopyToClipboard.Size = new System.Drawing.Size(134, 20);
            this.linkLabelCopyToClipboard.TabIndex = 2;
            this.linkLabelCopyToClipboard.TabStop = true;
            this.linkLabelCopyToClipboard.Text = "Copy to Clipboard";
            this.linkLabelCopyToClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCopyToClipboard_LinkClicked);
            // 
            // TMTErrorDialog
            // 
            this.AcceptButton = this.tmtButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 238);
            this.Name = "TMTErrorDialog";
            this.ShowInTaskbar = false;
            this.Text = "Error";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TMTButton tmtButtonOK;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.LinkLabel linkLabelCopyToClipboard;
    }
}