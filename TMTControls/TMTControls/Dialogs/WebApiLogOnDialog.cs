using FontAwesome.Sharp;
using System;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class WebApiLogOnDialog : BaseDialog
    {
        private static string DB = "DB";
        private static string LDAP = "LDAP";

        private bool showLdapServerUrl = false;

        public WebApiLogOnDialog()
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
                catch
                {
                    MessageDialog.Show(this, Properties.Resources.ERROR_LoginDialog_LDAP,
                        Properties.Resources.ERROR_LoginDialog_Header,
                        MessageDialogIcon.Error);
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

        private void CheckBoxShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
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
                            textBoxLdapServerUrl.Visible = showLdapServerUrl;
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
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ApplicationLogin);
            }
        }

        private void ComboBoxAuthSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxAuthSource.SelectedItem.ToString() == LDAP)
                {
                    if (checkBoxShowDetails.Checked)
                    {
                        textBoxLdapServerUrl.Visible = showLdapServerUrl;
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
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ApplicationLogin);
            }
        }
        
        private void WebAPILoginDialog_Load(object sender, EventArgs e)
        {
            try
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

                this.Image = IconChar.SignIn.ToBitmap(72, Color.FromArgb(82, 124, 182));
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ApplicationLogin);
            }
        }

        private void TableLayoutPanelMain_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                this.showLdapServerUrl = true;
            }
        }
    }
}