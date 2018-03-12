using FontAwesome.Sharp;
using NLog;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class ErrorDialog : BaseDialog
    {
        private Exception _error;

        private ErrorDialog()
        {
            InitializeComponent();
        }

        public Exception Error
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                this._error = value;
                textBoxMessage.Text = (this._error.InnerException != null) ? this._error.InnerException.Message : this._error.Message;

                LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(this._error);
            }
            get
            {
                return this._error;
            }
        }

        public static DialogResult Show(IWin32Window owner, Exception ex, string caption)
        {
            ErrorDialog errorDialog = null;
            try
            {
                errorDialog = new ErrorDialog
                {
                    Error = ex,
                    Text = caption
                };

                if (owner == null)
                {
                    return errorDialog.ShowDialog();
                }
                else
                {
                    return errorDialog.ShowDialog(owner);
                }
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

        private void LinkLabelCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetText($"{this.Error.Message}\r\n{this.Error.InnerException}\r\n{this.Error.StackTrace}");
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(ex);
            }
        }

        private void ErrorDialog_Load(object sender, EventArgs e)
        {
            try
            {

                this.Image = IconChar.ExclamationTriangle.ToBitmap(72, Color.FromArgb(143, 7, 24));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(ex);
            }
        }
    }
}