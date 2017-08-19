using NLog;
using System;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class TMTErrorDialog : TMTDialog
    {
        private Exception _error;

        private TMTErrorDialog()
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
            get
            {
                return labelHeader.Text;
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
            TMTErrorDialog errorDialog = null;
            try
            {
                errorDialog = new TMTErrorDialog
                {
                    Error = ex,
                    Caption = caption
                };

                return errorDialog.ShowDialog(owner);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (errorDialog != null)
                {
                    errorDialog.Dispose();
                }
            }
        }

        private void linkLabelCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(this.Error.Message + "\r\n" + this.Error.InnerException + "\r\n" + this.Error.StackTrace);
        }
    }
}