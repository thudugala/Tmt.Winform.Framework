using NLog;
using System;
using System.Drawing;
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

        public Exception Error
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                this._error = value;
                textBoxMessage.Text = this._error.Message;

                LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(this._error);
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

        private void TMTErrorDialog_Load(object sender, EventArgs e)
        {
            try
            {
                var pro = new FontAwesome5.Properties(FontAwesome5.Type.ExclamationTriangle)
                {
                    Location = new Point(0, 5),
                    Size = 72,
                    ForeColor = Color.FromArgb(143, 7, 24)
                };
                this.Image = pro.AsImage();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Error(ex);
            }
        }
    }
}