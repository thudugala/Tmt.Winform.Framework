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
            this.labelHeader.AutoSize = true;
            this.labelHeader.Image = global::TMTControls.Properties.Resources.login;
            this.labelHeader.MinimumSize = new System.Drawing.Size(484, 60);
            this.labelHeader.Size = new System.Drawing.Size(484, 60);
            this.labelHeader.Text = "Login";
            // 
            // panelMain
            // 
            this.panelMain.AutoSize = true;
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            this.panelMain.Size = new System.Drawing.Size(484, 186);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.AutoSize = true;
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Location = new System.Drawing.Point(391, 67);
            this.buttonLogin.MinimumSize = new System.Drawing.Size(70, 30);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(70, 30);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "OK";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxUserId
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxUserId, 3);
            this.textBoxUserId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserId.Location = new System.Drawing.Point(91, 3);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(370, 26);
            this.textBoxUserId.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxPassword, 3);
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Location = new System.Drawing.Point(91, 35);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(370, 26);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(17, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 32);
            this.label5.TabIndex = 14;
            this.label5.Text = "User ID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelPassword.Location = new System.Drawing.Point(3, 32);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(82, 32);
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password:";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxServerUrl
            // 
            this.textBoxServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxServerUrl, 3);
            this.textBoxServerUrl.Location = new System.Drawing.Point(91, 103);
            this.textBoxServerUrl.Name = "textBoxServerUrl";
            this.textBoxServerUrl.Size = new System.Drawing.Size(370, 26);
            this.textBoxServerUrl.TabIndex = 3;
            this.textBoxServerUrl.Visible = false;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelServer.Location = new System.Drawing.Point(26, 100);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(59, 32);
            this.labelServer.TabIndex = 18;
            this.labelServer.Text = "Server:";
            this.labelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelServer.Visible = false;
            // 
            // comboBoxAuthSource
            // 
            this.comboBoxAuthSource.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.comboBoxAuthSource.ConnectedLabel = null;
            this.comboBoxAuthSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAuthSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuthSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxAuthSource.FormattingEnabled = true;
            this.comboBoxAuthSource.Items.AddRange(new object[] {
            "DB",
            "LDAP"});
            this.comboBoxAuthSource.Location = new System.Drawing.Point(91, 135);
            this.comboBoxAuthSource.Name = "comboBoxAuthSource";
            this.comboBoxAuthSource.Size = new System.Drawing.Size(121, 28);
            this.comboBoxAuthSource.TabIndex = 19;
            tmtDataSourceInformation1.DbColumnName = "";
            tmtDataSourceInformation1.DbColumnType = System.TypeCode.String;
            tmtDataSourceInformation1.DbLabelText = "";
            tmtDataSourceInformation1.LovText = "";
            tmtDataSourceInformation1.LovViewName = "";
            this.comboBoxAuthSource.Tag = tmtDataSourceInformation1;
            this.comboBoxAuthSource.Visible = false;
            this.comboBoxAuthSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthSource_SelectedIndexChanged);
            // 
            // textBoxLdapServerUrl
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxLdapServerUrl, 2);
            this.textBoxLdapServerUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLdapServerUrl.Location = new System.Drawing.Point(218, 135);
            this.textBoxLdapServerUrl.Name = "textBoxLdapServerUrl";
            this.textBoxLdapServerUrl.Size = new System.Drawing.Size(243, 26);
            this.textBoxLdapServerUrl.TabIndex = 20;
            this.textBoxLdapServerUrl.Visible = false;
            // 
            // checkBoxShowDetails
            // 
            this.checkBoxShowDetails.AutoSize = true;
            this.checkBoxShowDetails.Location = new System.Drawing.Point(91, 67);
            this.checkBoxShowDetails.Name = "checkBoxShowDetails";
            this.checkBoxShowDetails.Size = new System.Drawing.Size(121, 24);
            this.checkBoxShowDetails.TabIndex = 21;
            this.checkBoxShowDetails.Text = "Show Details";
            this.checkBoxShowDetails.UseVisualStyleBackColor = true;
            this.checkBoxShowDetails.CheckedChanged += new System.EventHandler(this.checkBoxShowDetails_CheckedChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSize = true;
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
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(464, 166);
            this.tableLayoutPanelMain.TabIndex = 22;
            // 
            // TMTWebAPILoginDialog
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(484, 246);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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