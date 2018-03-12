using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public enum MessageDialogButton
    {
        OK,
        YesNo
    }

    public enum MessageDialogIcon
    {
        Warning,
        Asterisk,
        Information,
        Exclamation,
        Error,
        Question
    }

    public partial class MessageDialog : TMT.Controls.WinForms.BaseDialog
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageDialogIcon icon)
        {
            return Show(owner, text, caption, MessageDialogButton.OK, icon);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageDialogButton button, MessageDialogIcon icon)
        {
            MessageDialog messageDialog = null;
            try
            {
                messageDialog = new MessageDialog
                {
                    Text = caption
                };
                messageDialog.textBoxText.Text = text;
                messageDialog.SetButton(button);
                messageDialog.SetIcon(icon);

                if (owner == null)
                {
                    return messageDialog.ShowDialog();
                }
                else
                {
                    return messageDialog.ShowDialog(owner);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (messageDialog != null)
                {
                    messageDialog.Dispose();
                }
            }
        }

        public static DialogResult ShowQuestion(IWin32Window owner, string question, string caption)
        {
            return Show(owner, question, caption, MessageDialogButton.YesNo, MessageDialogIcon.Question);
        }

        private void SetButton(MessageDialogButton button)
        {
            switch (button)
            {
                case MessageDialogButton.OK:
                    buttonOK.Visible = true;
                    BaseButtonYes.Visible = BaseButtonNo.Visible = false;
                    break;

                case MessageDialogButton.YesNo:
                    buttonOK.Visible = false;
                    BaseButtonYes.Visible = BaseButtonNo.Visible = true;
                    break;
            }
        }

        private void SetIcon(MessageDialogIcon icon)
        {            
            switch (icon)
            {
                case MessageDialogIcon.Asterisk:
                    this.pictureBoxImage.IconChar = IconChar.Asterisk;
                    this.pictureBoxImage.IconColor = Color.FromArgb(248, 178, 3);
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;

                case MessageDialogIcon.Information:
                    this.pictureBoxImage.IconChar = IconChar.InfoCircle;
                    this.pictureBoxImage.IconColor = Color.FromArgb(66, 134, 244);
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;

                case MessageDialogIcon.Warning:
                    this.pictureBoxImage.IconChar = IconChar.ExclamationTriangle;
                    this.pictureBoxImage.IconColor = Color.Orange;
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;

                case MessageDialogIcon.Exclamation:
                    this.pictureBoxImage.IconChar = IconChar.ExclamationCircle;
                    this.pictureBoxImage.IconColor = Color.Yellow;
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;

                case MessageDialogIcon.Error:
                    this.pictureBoxImage.IconChar = IconChar.ExclamationTriangle;
                    this.pictureBoxImage.IconColor = Color.FromArgb(143, 7, 24);
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;

                case MessageDialogIcon.Question:
                    this.pictureBoxImage.IconChar = IconChar.QuestionCircle;
                    this.pictureBoxImage.IconColor = Color.FromArgb(243, 57, 57);
                    this.Image = this.pictureBoxImage.IconChar.ToBitmap(72, this.pictureBoxImage.IconColor);
                    break;
            }
        }
    }
}