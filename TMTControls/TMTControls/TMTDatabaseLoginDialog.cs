using System;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTDatabaseLoginDialog : Form
    {
        public TMTDatabaseLoginDialog()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void TMTDatabaseLoginDialog_Load(object sender, EventArgs e)
        {
            textBoxUserId.Text = Properties.Settings.Default.LogInUserId;
            textBoxUserId.Tag = textBoxUserId.Text;
            textBoxPassword.Text = Properties.Settings.Default.LogInPassword;

            textBoxDatabaseName.Text = Properties.Settings.Default.DatabaseName;
        }

        public LoginDataEntity LoginData
        {
            get
            {
                LoginDataEntity myLoginDataEntity = new LoginDataEntity();

                myLoginDataEntity.UserId = textBoxUserId.Text.Trim();
                myLoginDataEntity.Password = textBoxPassword.Text.Trim();

                myLoginDataEntity.DatabaseServerName = textBoxServer.Text.Trim();
                myLoginDataEntity.DatabasePort = int.Parse(textBoxPort.Text.Trim());
                myLoginDataEntity.DatabaseName = textBoxDatabaseName.Text.Trim();

                return myLoginDataEntity;
            }
            set
            {
                textBoxPassword.Text = value.Password;
                textBoxDatabaseName.Text = value.DatabaseName;
            }
        }

        public class LoginDataEntity
        {
            public string UserId { get; set; }
            public string Password { get; set; }

            public string DatabaseServerName { get; set; }
            public int DatabasePort { get; set; }
            public string DatabaseName { get; set; }
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

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}