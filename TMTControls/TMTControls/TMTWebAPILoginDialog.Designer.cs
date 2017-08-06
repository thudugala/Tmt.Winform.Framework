namespace TMTControls
{
    partial class TMTWebAPILoginDialog
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
            TMTControls.TMTDataSourceInformation tmtDataSourceInformation1 = new TMTControls.TMTDataSourceInformation();
            this.buttonLogin = new TMTControls.TMTButton();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxServerUrl = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxAuthSource = new TMTControls.TMTComboBox();
            this.textBoxLdapServerUrl = new System.Windows.Forms.TextBox();
            this.checkBoxShowDetails = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Image = global::TMTControls.Properties.Resources.login;
            this.labelHeader.Size = new System.Drawing.Size(561, 60);
            this.labelHeader.Text = "Login";
            // 
            // panelMain
            // 
            this.panelMain.AutoSize = true;
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3);
            this.panelMain.Size = new System.Drawing.Size(561, 184);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.AutoSize = true;
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Location = new System.Drawing.Point(478, 69);
            this.buttonLogin.MinimumSize = new System.Drawing.Size(62, 24);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(62, 30);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "OK";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxUserId
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxUserId, 3);
            this.textBoxUserId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserId.Location = new System.Drawing.Point(97, 3);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(443, 27);
            this.textBoxUserId.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxPassword, 3);
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Location = new System.Drawing.Point(97, 36);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(443, 27);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(19, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 33);
            this.label5.TabIndex = 14;
            this.label5.Text = "User ID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelPassword.Location = new System.Drawing.Point(3, 33);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(88, 33);
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password:";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxServerUrl
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxServerUrl, 3);
            this.textBoxServerUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxServerUrl.Location = new System.Drawing.Point(97, 105);
            this.textBoxServerUrl.Name = "textBoxServerUrl";
            this.textBoxServerUrl.Size = new System.Drawing.Size(443, 27);
            this.textBoxServerUrl.TabIndex = 3;
            this.textBoxServerUrl.Visible = false;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelServer.Location = new System.Drawing.Point(28, 102);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(63, 33);
            this.labelServer.TabIndex = 18;
            this.labelServer.Text = "Server:";
            this.labelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelServer.Visible = false;
            // 
            // comboBoxAuthSource
            // 
            this.comboBoxAuthSource.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.comboBoxAuthSource.ConnectedLabel = null;
            this.comboBoxAuthSource.DbColumnName = null;
            this.comboBoxAuthSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuthSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxAuthSource.FormattingEnabled = true;
            this.comboBoxAuthSource.Items.AddRange(new object[] {
            "DB",
            "LDAP"});
            this.comboBoxAuthSource.Location = new System.Drawing.Point(97, 138);
            this.comboBoxAuthSource.Name = "comboBoxAuthSource";
            this.comboBoxAuthSource.Size = new System.Drawing.Size(101, 28);
            this.comboBoxAuthSource.TabIndex = 19;
            tmtDataSourceInformation1.DbColumnName = "";
            tmtDataSourceInformation1.DbColumnType = System.TypeCode.String;
            tmtDataSourceInformation1.DbLabelText = "";
            tmtDataSourceInformation1.DbValue = "";
            tmtDataSourceInformation1.FalseValue = "FALSE";
            tmtDataSourceInformation1.IndeterminateValue = "FALSE";
            tmtDataSourceInformation1.LovText = "";
            tmtDataSourceInformation1.LovViewName = "";
            tmtDataSourceInformation1.TrueValue = "TRUE";
            this.comboBoxAuthSource.Tag = tmtDataSourceInformation1;
            this.comboBoxAuthSource.Visible = false;
            this.comboBoxAuthSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthSource_SelectedIndexChanged);
            // 
            // textBoxLdapServerUrl
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxLdapServerUrl, 2);
            this.textBoxLdapServerUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLdapServerUrl.Location = new System.Drawing.Point(233, 138);
            this.textBoxLdapServerUrl.Name = "textBoxLdapServerUrl";
            this.textBoxLdapServerUrl.Size = new System.Drawing.Size(307, 27);
            this.textBoxLdapServerUrl.TabIndex = 20;
            this.textBoxLdapServerUrl.Visible = false;
            // 
            // checkBoxShowDetails
            // 
            this.checkBoxShowDetails.AutoSize = true;
            this.checkBoxShowDetails.Location = new System.Drawing.Point(97, 69);
            this.checkBoxShowDetails.Name = "checkBoxShowDetails";
            this.checkBoxShowDetails.Size = new System.Drawing.Size(130, 24);
            this.checkBoxShowDetails.TabIndex = 21;
            this.checkBoxShowDetails.Text = "Show Details";
            this.checkBoxShowDetails.UseVisualStyleBackColor = true;
            this.checkBoxShowDetails.CheckedChanged += new System.EventHandler(this.checkBoxShowDetails_CheckedChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSize = true;
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
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
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(9, 8);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(543, 168);
            this.tableLayoutPanelMain.TabIndex = 22;
            // 
            // TMTWebAPILoginDialog
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(561, 244);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TMTWebAPILoginDialog";
            this.ShowIcon = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
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
        private TMTControls.TMTComboBox comboBoxAuthSource;
        private System.Windows.Forms.TextBox textBoxLdapServerUrl;
        private System.Windows.Forms.CheckBox checkBoxShowDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
    }
}