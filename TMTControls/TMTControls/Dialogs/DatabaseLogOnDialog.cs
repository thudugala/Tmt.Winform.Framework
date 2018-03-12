using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class DatabaseLogOnDialog : Form
    {
        public DatabaseLogOnDialog()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
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
            if (string.IsNullOrWhiteSpace(textBoxPort.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_DatabaseServerPort,
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

            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
        }

        private void DatabaseLoginDialog_Load(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.None;
                labelHeader.Image = IconChar.SignIn.ToBitmap(72, Color.DarkBlue);
            }
            catch
            {
            }
        }
    }
}