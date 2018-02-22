using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
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

    public partial class MessageDialog : TMTControls.TMTDialog
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
                    tmtButtonOK.Visible = true;
                    tmtButtonYes.Visible = tmtButtonNo.Visible = false;
                    break;

                case MessageDialogButton.YesNo:
                    tmtButtonOK.Visible = false;
                    tmtButtonYes.Visible = tmtButtonNo.Visible = true;
                    break;
            }
        }

        private void SetIcon(MessageDialogIcon icon)
        {
            var pictureBoxImage = new FontAwesome5.Properties(FontAwesome5.Type.None)
            {
                Size = 128
            };
            var labelImage = new FontAwesome5.Properties(FontAwesome5.Type.None)
            {
                Location = new Point(0, 5),
                Size = 72
            };
            switch (icon)
            {
                case MessageDialogIcon.Asterisk:
                    pictureBoxImage.ForeColor = Color.FromArgb(248, 178, 3);
                    labelImage.ForeColor = Color.FromArgb(248, 178, 3);
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.Asterisk, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.Asterisk, labelImage);
                    break;

                case MessageDialogIcon.Information:
                    pictureBoxImage.ForeColor = Color.FromArgb(66, 134, 244);
                    labelImage.ForeColor = Color.FromArgb(66, 134, 244);
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.InfoCircle, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.InfoCircle, labelImage);
                    break;

                case MessageDialogIcon.Warning:
                    pictureBoxImage.ForeColor = Color.Orange;
                    labelImage.ForeColor = Color.Orange;
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationTriangle, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationTriangle, labelImage);
                    break;

                case MessageDialogIcon.Exclamation:
                    pictureBoxImage.ForeColor = Color.Yellow;
                    labelImage.ForeColor = Color.Yellow;
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationCircle, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationCircle, labelImage);
                    break;

                case MessageDialogIcon.Error:
                    pictureBoxImage.ForeColor = Color.FromArgb(143, 7, 24);
                    labelImage.ForeColor = Color.FromArgb(143, 7, 24);
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationTriangle, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.ExclamationTriangle, labelImage);
                    break;

                case MessageDialogIcon.Question:
                    pictureBoxImage.ForeColor = Color.FromArgb(243, 57, 57);
                    labelImage.ForeColor = Color.FromArgb(243, 57, 57);
                    this.pictureBoxImage.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.QuestionCircle, pictureBoxImage);
                    this.Image = FontAwesome5.Instance.GetImage(FontAwesome5.Type.QuestionCircle, labelImage);
                    break;
            }
        }
    }
}