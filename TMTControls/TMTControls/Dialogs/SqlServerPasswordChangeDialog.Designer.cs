namespace TMT.Controls.WinForms.Dialogs
{
    partial class SqlServerPasswordChangeDialog
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
            this.BaseButtonLogin = new TMT.Controls.WinForms.BaseButton();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxConfirmPassword);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.BaseButtonLogin);
            this.panelMain.Controls.Add(this.textBoxNewPassword);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Size = new System.Drawing.Size(434, 107);
            // 
            // BaseButtonLogin
            // 
            this.BaseButtonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseButtonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BaseButtonLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BaseButtonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BaseButtonLogin.FlatAppearance.BorderSize = 0;
            this.BaseButtonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BaseButtonLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BaseButtonLogin.Location = new System.Drawing.Point(373, 69);
            this.BaseButtonLogin.Margin = new System.Windows.Forms.Padding(2);
            this.BaseButtonLogin.Name = "BaseButtonLogin";
            this.BaseButtonLogin.Size = new System.Drawing.Size(50, 29);
            this.BaseButtonLogin.TabIndex = 2;
            this.BaseButtonLogin.Text = "OK";
            this.BaseButtonLogin.UseVisualStyleBackColor = false;
            this.BaseButtonLogin.Click += new System.EventHandler(this.BaseButtonLogin_Click);
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewPassword.Location = new System.Drawing.Point(141, 12);
            this.textBoxNewPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.Size = new System.Drawing.Size(281, 23);
            this.textBoxNewPassword.TabIndex = 0;
            this.textBoxNewPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "New Password:";
            // 
            // textBoxConfirmPassword
            // 
            this.textBoxConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfirmPassword.Location = new System.Drawing.Point(141, 39);
            this.textBoxConfirmPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxConfirmPassword.Name = "textBoxConfirmPassword";
            this.textBoxConfirmPassword.Size = new System.Drawing.Size(281, 23);
            this.textBoxConfirmPassword.TabIndex = 1;
            this.textBoxConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Confirm Password:";
            // 
            // SqlServerPasswordChangeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.ClientSize = new System.Drawing.Size(434, 171);
            this.Name = "SqlServerPasswordChangeDialog";
            this.Text = "Change Password";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConfirmPassword;
        private System.Windows.Forms.Label label1;
        private BaseButton BaseButtonLogin;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label label2;
    }
}
