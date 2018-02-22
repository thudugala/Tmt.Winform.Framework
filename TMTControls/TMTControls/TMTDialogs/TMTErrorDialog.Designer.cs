namespace TMTControls.TMTDialogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTErrorDialog));
            this.tmtButtonOK = new TMTControls.TMTButton();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.linkLabelCopyToClipboard = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.linkLabelCopyToClipboard);
            this.panelMain.Controls.Add(this.textBoxMessage);
            this.panelMain.Controls.Add(this.tmtButtonOK);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // tmtButtonOK
            // 
            resources.ApplyResources(this.tmtButtonOK, "tmtButtonOK");
            this.tmtButtonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tmtButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonOK.FlatAppearance.BorderSize = 0;
            this.tmtButtonOK.Name = "tmtButtonOK";
            this.tmtButtonOK.UseVisualStyleBackColor = false;
            // 
            // textBoxMessage
            // 
            resources.ApplyResources(this.textBoxMessage, "textBoxMessage");
            this.textBoxMessage.Name = "textBoxMessage";
            // 
            // linkLabelCopyToClipboard
            // 
            resources.ApplyResources(this.linkLabelCopyToClipboard, "linkLabelCopyToClipboard");
            this.linkLabelCopyToClipboard.Name = "linkLabelCopyToClipboard";
            this.linkLabelCopyToClipboard.TabStop = true;
            this.linkLabelCopyToClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCopyToClipboard_LinkClicked);
            // 
            // TMTErrorDialog
            // 
            this.AcceptButton = this.tmtButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Name = "TMTErrorDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TMTErrorDialog_Load);
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