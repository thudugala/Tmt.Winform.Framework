using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class MessageDialog : TMTControls.TMTDialog
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        private void SetIcon(MessageDialogIcon icon)
        {
            switch (icon)
            {
                case MessageDialogIcon.Asterisk:
                    this.Image = Properties.Resources.asterisk;
                    break;

                case MessageDialogIcon.Information:
                    this.Image = Properties.Resources.Information;
                    break;

                case MessageDialogIcon.Warning:
                    this.Image = Properties.Resources.warning;
                    break;

                case MessageDialogIcon.Exclamation:
                    this.Image = Properties.Resources.exclamation;
                    break;

                case MessageDialogIcon.Error:
                    this.Image = Properties.Resources.error;
                    break;

                case MessageDialogIcon.Question:
                    this.Image = Properties.Resources.question;
                    break;
            }
            this.pictureBoxImage.Image = this.Image;
        }

        private void SetButton(MessageDialogButton button)
        {
            switch (button)
            {
                case MessageDialogButton.OK:
                    tmtButtonOK.Visible = true;
                    tmtButtonYes.Visible = tmtButtonNo.Visible = false;
                    break;

                case MessageDialogButton.YesNo:
                    tmtButtonOK.Visible = false;
                    tmtButtonYes.Visible = tmtButtonNo.Visible = true;
                    break;
            }
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageDialogIcon icon)
        {
            return Show(owner, text, caption, MessageDialogButton.OK, icon);
        }

        public static DialogResult ShowQuestion(IWin32Window owner, string question, string caption)
        {
            return Show(owner, question, caption, MessageDialogButton.YesNo, MessageDialogIcon.Question);
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

    public enum MessageDialogButton
    {
        OK,
        YesNo
    }
}