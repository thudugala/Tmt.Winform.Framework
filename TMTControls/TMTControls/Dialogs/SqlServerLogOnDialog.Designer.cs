namespace TMT.Controls.WinForms.Dialogs
{
    partial class SqlServerLogOnDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerLogOnDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BaseButtonLogin = new TMT.Controls.WinForms.BaseButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.linkLabelPasswordRest = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.linkLabelPasswordRest);
            this.panelMain.Controls.Add(this.textBoxServer);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.BaseButtonLogin);
            this.panelMain.Controls.Add(this.textBoxUserId);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.textBoxDatabaseName);
            this.panelMain.Controls.Add(this.textBoxPassword);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.label2);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxDatabaseName
            // 
            resources.ApplyResources(this.textBoxDatabaseName, "textBoxDatabaseName");
            this.textBoxDatabaseName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TMT.Controls.WinForms.Properties.Settings.Default, "DatabaseName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Text = global::TMT.Controls.WinForms.Properties.Settings.Default.DatabaseName;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BaseButtonLogin
            // 
            resources.ApplyResources(this.BaseButtonLogin, "BaseButtonLogin");
            this.BaseButtonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BaseButtonLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BaseButtonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BaseButtonLogin.FlatAppearance.BorderSize = 0;
            this.BaseButtonLogin.Name = "BaseButtonLogin";
            this.BaseButtonLogin.UseVisualStyleBackColor = false;
            this.BaseButtonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxServer
            // 
            resources.ApplyResources(this.textBoxServer, "textBoxServer");
            this.textBoxServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TMT.Controls.WinForms.Properties.Settings.Default, "DatabaseServerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Text = global::TMT.Controls.WinForms.Properties.Settings.Default.DatabaseServerName;
            // 
            // textBoxUserId
            // 
            resources.ApplyResources(this.textBoxUserId, "textBoxUserId");
            this.textBoxUserId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TMT.Controls.WinForms.Properties.Settings.Default, "LogInUserId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Text = global::TMT.Controls.WinForms.Properties.Settings.Default.LogInUserId;
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TMT.Controls.WinForms.Properties.Settings.Default, "LogInPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Text = global::TMT.Controls.WinForms.Properties.Settings.Default.LogInPassword;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // linkLabelPasswordRest
            // 
            resources.ApplyResources(this.linkLabelPasswordRest, "linkLabelPasswordRest");
            this.linkLabelPasswordRest.Name = "linkLabelPasswordRest";
            this.linkLabelPasswordRest.TabStop = true;
            this.linkLabelPasswordRest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPasswordRest_LinkClicked);
            // 
            // SqlServerLogOnDialog
            // 
            this.AcceptButton = this.BaseButtonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "SqlServerLogOnDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.DatabaseLoginDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxDatabaseName;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label label1;
        private BaseButton BaseButtonLogin;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabelPasswordRest;
    }
}