using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class SqlServerLogOnDialog : TMT.Controls.WinForms.BaseDialog
    {
        public SqlServerLogOnDialog()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.None;

                ValidateForm();

                Properties.Settings.Default.Save();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, "Error Validating");
            }
        }

        private void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxUserId.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_UserIdEmpty,
                    Properties.Resources.ERROR_LoginDialog_Header,
                    MessageDialogIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_PasswordEmpty,
                    Properties.Resources.ERROR_LoginDialog_Header,
                    MessageDialogIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxServer.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_ServerUrlEmpty,
                    Properties.Resources.ERROR_LoginDialog_Header,
                    MessageDialogIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxDatabaseName.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_DatabaseNameEmpty,
                    Properties.Resources.ERROR_LoginDialog_Header,
                    MessageDialogIcon.Error);
                return;
            }
        }

        private void DatabaseLoginDialog_Load(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.None;
                this.Image = IconChar.SignIn.ToBitmap(72, Color.DarkBlue);
            }
            catch
            {
            }
        }

        private void LinkLabelPasswordRest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ValidateForm();

                var passwordChangeDialog = new SqlServerPasswordChangeDialog();
                passwordChangeDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, "Password Reset Error");
            }
        }
    }
}