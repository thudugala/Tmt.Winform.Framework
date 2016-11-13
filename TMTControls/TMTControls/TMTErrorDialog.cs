using NLog;
using System;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTErrorDialog : TMTDialog
    {
        private Exception _error;

        public TMTErrorDialog()
        {
            InitializeComponent();
        }

        public string Caption
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    labelHeader.Text = value;
                }
            }
        }

        public Exception Error
        {
            set
            {
                if (value != null)
                {
                    this._error = value;
                    textBoxMessage.Text = this._error.Message;

                    LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(this._error);
                }
            }
            get
            {
                return this._error;
            }
        }

        public static DialogResult Show(IWin32Window owner, Exception ex, string caption)
        {
            TMTErrorDialog dialog = new TMTErrorDialog();
            dialog.Error = ex;
            dialog.Caption = caption;

            return dialog.ShowDialog(owner);
        }

        private void linkLabelCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(this.Error.Message + "\r\n" + this.Error.InnerException + "\r\n" + this.Error.StackTrace);
        }
    }
}