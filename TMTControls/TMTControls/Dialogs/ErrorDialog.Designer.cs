namespace TMT.Controls.WinForms.Dialogs
{
    partial class ErrorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
            this.buttonOK = new TMT.Controls.WinForms.BaseButton();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.linkLabelCopyToClipboard = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.linkLabelCopyToClipboard);
            this.panelMain.Controls.Add(this.textBoxMessage);
            this.panelMain.Controls.Add(this.buttonOK);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = false;
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
            // ErrorDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Name = "ErrorDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ErrorDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BaseButton buttonOK;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.LinkLabel linkLabelCopyToClipboard;
    }
}