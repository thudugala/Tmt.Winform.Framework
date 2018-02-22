using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class TMTDatabaseLogOnDialog : Form
    {
        public TMTDatabaseLogOnDialog()
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

        private void TMTDatabaseLoginDialog_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            var pro = new FontAwesome5.Properties(FontAwesome5.Type.SignIn)
            {
                Location = new Point(0, 5),
                Size = 96,
                ForeColor = Color.DarkBlue
            };
            labelHeader.Image = pro.AsImage();
        }
    }
}