using System;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class TMTDatabaseLogOnDialog : Form
    {
        public TMTDatabaseLogOnDialog()
        {
            InitializeComponent();
        }

        private void TMTDatabaseLoginDialog_Load(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserId.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_UserIdEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_PasswordEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxServer.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_ServerUrlEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxPort.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_DatabaseServerPort, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxDatabaseName.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_DatabaseNameEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.Save();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}