using System;
using System.Windows.Forms;
using TinyIoC;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class SqlServerPasswordChangeDialog : TMT.Controls.WinForms.BaseDialog
    {
        public SqlServerPasswordChangeDialog()
        {
            InitializeComponent();
        }

        private void BaseButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNewPassword.Text))
                {
                    MessageDialog.Show(this, Properties.Resources.ERROR_PasswordDialog_NewPasswordEmpty,
                        Properties.Resources.ERROR_PasswordDialog,
                        MessageDialogIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBoxConfirmPassword.Text))
                {
                    MessageDialog.Show(this, Properties.Resources.ERROR_PasswordDialog_ConfirmPasswordEmpty,
                        Properties.Resources.ERROR_PasswordDialog,
                        MessageDialogIcon.Error);
                    return;
                }

                if (textBoxNewPassword.Text != textBoxConfirmPassword.Text)
                {
                    MessageDialog.Show(this, Properties.Resources.ERROR_PasswordDialog_PasswordNoMatch,
                        Properties.Resources.ERROR_PasswordDialog,
                        MessageDialogIcon.Error);
                    return;
                }

                var dataManager = TinyIoCContainer.Current.Resolve<IDataManager>();
                if (dataManager != null)
                {
                    dataManager.ChangePassword(textBoxConfirmPassword.Text);
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, "Password Reset Error");
            }
        }
    }
}