using System;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class TmtWebApiLogOnDialog : TMTDialog
    {
        private static string LDAP = "LDAP";
        private static string DB = "DB";

        public TmtWebApiLogOnDialog()
        {
            InitializeComponent();

            comboBoxAuthSource.SelectedIndex = 0;
            this.DialogResult = DialogResult.None;
        }

        public bool OnlyDBAuth { get; set; }

        public LogOnDataEntity GetLogOnData()
        {
            var myLogOnDataEntity = new LogOnDataEntity
            {
                UserId = textBoxUserId.Text.Trim(),
                Password = textBoxPassword.Text.Trim(),
                ServerUrl = textBoxServerUrl.Text.Trim(),
                AuthSource = comboBoxAuthSource.SelectedItem.ToString().Trim()
            };

            Properties.Settings.Default.LogInUserId = myLogOnDataEntity.UserId;
            Properties.Settings.Default.LogInPassword = myLogOnDataEntity.Password;
            Properties.Settings.Default.ServerURL = myLogOnDataEntity.ServerUrl;
            Properties.Settings.Default.LDAPServerURL = textBoxLdapServerUrl.Text.Trim();

            return myLogOnDataEntity;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
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
            if (string.IsNullOrWhiteSpace(textBoxServerUrl.Text))
            {
                MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_ServerUrlEmpty,
                    Properties.Resources.ERROR_LoginDialog_Header,
                    MessageDialogIcon.Error);
                return;
            }
            if (comboBoxAuthSource.SelectedItem.ToString() == LDAP)
            {
                if (string.IsNullOrWhiteSpace(textBoxLdapServerUrl.Text))
                {
                    MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_LDAPServerUrlEmpty,
                        Properties.Resources.ERROR_LoginDialog_Header,
                        MessageDialogIcon.Error);
                    return;
                }
                PrincipalContext context = null;
                try
                {
                    context = new PrincipalContext(ContextType.Domain, textBoxLdapServerUrl.Text);

                    if (context.ValidateCredentials(textBoxUserId.Text, textBoxPassword.Text) == false)
                    {
                        MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_LDAPUserNotFound,
                            Properties.Resources.ERROR_LoginDialog_Header,
                            MessageDialogIcon.Error);
                        return;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException || ex is InvalidEnumArgumentException)
                {
                    TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_LoginDialog_Header);
                    return;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ComboBoxAuthSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAuthSource.SelectedItem.ToString() == LDAP)
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
            comboBoxAuthSource.Text = (Properties.Settings.Default.LDAPAuth) ? LDAP : DB;
            textBoxLdapServerUrl.Text = Properties.Settings.Default.LDAPServerURL;

            if (this.OnlyDBAuth)
            {
                comboBoxAuthSource.Visible = false;
                textBoxLdapServerUrl.Visible = false;
            }
        }

        private void CheckBoxShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDetails.Checked)
            {
                labelServer.Visible = true;
                textBoxServerUrl.Visible = true;
                if (this.OnlyDBAuth == false)
                {
                    comboBoxAuthSource.Visible = true;
                    if (comboBoxAuthSource.SelectedItem.ToString() == LDAP)
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