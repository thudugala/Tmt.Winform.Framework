using System;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTWebAPILoginDialog : TMTDialog
    {
        public TMTWebAPILoginDialog()
        {
            InitializeComponent();

            comboBoxAuthSource.SelectedIndex = 0;
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        public bool OnlyDBAuth { get; set; }

        public LoginDataEntity GetLoginData()
        {
            LoginDataEntity myLoginDataEntity = new LoginDataEntity();

            myLoginDataEntity.UserId = textBoxUserId.Text.Trim();
            myLoginDataEntity.Password = textBoxPassword.Text.Trim();
            myLoginDataEntity.ServerUrl = textBoxServerUrl.Text.Trim();
            myLoginDataEntity.AuthSource = comboBoxAuthSource.SelectedItem.ToString().Trim();

            Properties.Settings.Default.LogInUserId = myLoginDataEntity.UserId;
            Properties.Settings.Default.LogInPassword = myLoginDataEntity.Password;
            Properties.Settings.Default.ServerURL = myLoginDataEntity.ServerUrl;
            Properties.Settings.Default.LDAPServerURL = textBoxLdapServerUrl.Text.Trim();

            return myLoginDataEntity;
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
            if (string.IsNullOrWhiteSpace(textBoxServerUrl.Text))
            {
                MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_ServerUrlEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAuthSource.SelectedItem.ToString() == "LDAP")
            {
                if (string.IsNullOrWhiteSpace(textBoxLdapServerUrl.Text))
                {
                    MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_LDAPServerUrlEmpty, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //ersruk
                    using (var context = new PrincipalContext(ContextType.Domain, textBoxLdapServerUrl.Text))
                    {
                        if (context.ValidateCredentials(textBoxUserId.Text, textBoxPassword.Text) == false)
                        {
                            MessageBox.Show(this, Properties.Resources.ERROR_LoginDialog_LDAPUserNotFound, Properties.Resources.ERROR_LoginDialog_Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_LoginDialog_Header);
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void comboBoxAuthSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAuthSource.SelectedItem.ToString() == "LDAP")
            {
                if (checkBoxShowDetails.Checked)
                {
                    textBoxLdapServerUrl.Visible = true;
                }

                textBoxUserId.Text = Environment.UserName;
            }
            else
            {
                textBoxLdapServerUrl.Visible = false;

                if (textBoxUserId.Tag != null)
                {
                    textBoxUserId.Text = textBoxUserId.Tag.ToString();
                }
            }
        }

        private void TMTWebAPILoginDialog_Load(object sender, EventArgs e)
        {
            textBoxUserId.Text = Properties.Settings.Default.LogInUserId;
            textBoxUserId.Tag = textBoxUserId.Text;
            textBoxPassword.Text = Properties.Settings.Default.LogInPassword;
            textBoxServerUrl.Text = Properties.Settings.Default.ServerURL;
            comboBoxAuthSource.Text = (Properties.Settings.Default.LDAPAuth) ? "LDAP" : "DB";
            textBoxLdapServerUrl.Text = Properties.Settings.Default.LDAPServerURL;

            if (this.OnlyDBAuth)
            {
                comboBoxAuthSource.Visible = false;
                textBoxLdapServerUrl.Visible = false;
            }
        }

        public class LoginDataEntity
        {
            public string AuthSource { get; set; }
            public string Password { get; set; }

            public string ServerUrl { get; set; }
            public string UserId { get; set; }
        }

        private void checkBoxShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDetails.Checked)
            {
                labelServer.Visible = true;
                textBoxServerUrl.Visible = true;
                if (this.OnlyDBAuth == false)
                {
                    comboBoxAuthSource.Visible = true;
                    if (comboBoxAuthSource.SelectedItem.ToString() == "LDAP")
                    {
                        textBoxLdapServerUrl.Visible = true;
                    }
                }
            }
            else
            {
                labelServer.Visible = false;
                textBoxServerUrl.Visible = false;
                comboBoxAuthSource.Visible = false;
                textBoxLdapServerUrl.Visible = false;
            }
        }
    }
}