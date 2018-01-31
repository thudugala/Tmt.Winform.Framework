namespace TMTControls.TMTDialogs
{
    partial class TmtWebApiLogOnDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TmtWebApiLogOnDialog));
            this.buttonLogin = new TMTControls.TMTButton();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxServerUrl = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxAuthSource = new TMTControls.TMTDatabaseUI.TMTComboBox();
            this.textBoxLdapServerUrl = new System.Windows.Forms.TextBox();
            this.checkBoxShowDetails = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxUserId
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxUserId, 3);
            resources.ApplyResources(this.textBoxUserId, "textBoxUserId");
            this.textBoxUserId.Name = "textBoxUserId";
            // 
            // textBoxPassword
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxPassword, 3);
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // textBoxServerUrl
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxServerUrl, 3);
            resources.ApplyResources(this.textBoxServerUrl, "textBoxServerUrl");
            this.textBoxServerUrl.Name = "textBoxServerUrl";
            // 
            // labelServer
            // 
            resources.ApplyResources(this.labelServer, "labelServer");
            this.labelServer.Name = "labelServer";
            // 
            // comboBoxAuthSource
            // 
            this.comboBoxAuthSource.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.comboBoxAuthSource.ConnectedLabel = null;
            this.comboBoxAuthSource.DbColumnName = null;
            this.comboBoxAuthSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxAuthSource, "comboBoxAuthSource");
            this.comboBoxAuthSource.FormattingEnabled = true;
            this.comboBoxAuthSource.Items.AddRange(new object[] {
            resources.GetString("comboBoxAuthSource.Items"),
            resources.GetString("comboBoxAuthSource.Items1")});
            this.comboBoxAuthSource.Name = "comboBoxAuthSource";
            this.comboBoxAuthSource.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAuthSource_SelectedIndexChanged);
            // 
            // textBoxLdapServerUrl
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxLdapServerUrl, 2);
            resources.ApplyResources(this.textBoxLdapServerUrl, "textBoxLdapServerUrl");
            this.textBoxLdapServerUrl.Name = "textBoxLdapServerUrl";
            // 
            // checkBoxShowDetails
            // 
            resources.ApplyResources(this.checkBoxShowDetails, "checkBoxShowDetails");
            this.checkBoxShowDetails.Name = "checkBoxShowDetails";
            this.checkBoxShowDetails.UseVisualStyleBackColor = true;
            this.checkBoxShowDetails.CheckedChanged += new System.EventHandler(this.CheckBoxShowDetails_CheckedChanged);
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxLdapServerUrl, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.checkBoxShowDetails, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxAuthSource, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelPassword, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxServerUrl, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxUserId, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelServer, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxPassword, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonLogin, 3, 2);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            // 
            // TmtWebApiLogOnDialog
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Image = global::TMTControls.Properties.Resources.login;
            this.Name = "TmtWebApiLogOnDialog";
            this.ShowIcon = true;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TMTWebAPILoginDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TMTControls.TMTButton buttonLogin;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxServerUrl;
        private System.Windows.Forms.Label labelServer;
        private TMTControls.TMTDatabaseUI.TMTComboBox comboBoxAuthSource;
        private System.Windows.Forms.TextBox textBoxLdapServerUrl;
        private System.Windows.Forms.CheckBox checkBoxShowDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
    }
}