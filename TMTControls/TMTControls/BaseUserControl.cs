using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TMTControls.TMTDialogs;

namespace TMTControls
{
    [ToolboxItem(false)]
    public partial class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
        }

        private void BaseUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                this.toolStripStatusLabelWindowName.Text = this.Name;
                this.SetServerInformation();
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void SetServerInformation()
        {
            toolStripStatusLabelFill.Text = $"{Properties.Settings.Default.LogInUserId} at {Properties.Settings.Default.ServerURL}";
        }

        protected T NavigateToPanel<T>() where T : UserControl
        {
            return (this.ParentForm as TMTFormMain)?.LoadPanel(typeof(T)) as T;
        }

        [Category("TMT")]
        public event EventHandler NavigateBack;

        private void ButtonNavigateBack_Click(object sender, EventArgs e)
        {
            try
            {
                NavigateBack?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_NavigationBack);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    this.buttonNavigateBack.PerformClick();
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToolStripSplitButtonOptions_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (captureScreenToolStripMenuItem.Checked)
                {
                    Rectangle bounds = this.ParentForm.Bounds;
                    using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                    {
                        using (var g = Graphics.FromImage(bitmap))
                        {
                            g.CopyFromScreen(this.ParentForm.Location, Point.Empty, bounds.Size);
                        }
                        Clipboard.SetImage(bitmap);
                        toolStripStatusLabelFill.Text = "Window Screen Caputred to Clipboard";
                    }
                }
            }
            catch
            { }
        }

        private void BaseUserControl_Validated(object sender, EventArgs e)
        {
            this.SetServerInformation();
        }
    }
}