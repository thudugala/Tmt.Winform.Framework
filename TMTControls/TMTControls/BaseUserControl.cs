using FontAwesome.Sharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms
{
    [ToolboxItem(false)]
    public partial class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
        }

        [Category("Behavior")]
        public event EventHandler NavigateBack;

        protected async Task<T> NavigateToPanel<T>() where T : UserControl
        {
            var usercontrol = await (this.ParentForm as FormMain)?.LoadPanel(typeof(T));          
            return usercontrol as T;
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

        private void BaseUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                this.toolStripStatusLabelWindowName.Text = this.Name;
                this.SetServerInformation();

                toolStripSplitButtonOptions.Image = IconChar.Cog.ToBitmap(16, Color.FromArgb(154, 189, 224));
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void BaseUserControl_Validated(object sender, EventArgs e)
        {
            this.SetServerInformation();
        }

        private void ButtonNavigateBack_Click(object sender, EventArgs e)
        {
            try
            {
                NavigateBack?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_NavigationBack);
            }
        }

        private void SetServerInformation()
        {
            string server = Properties.Settings.Default.ServerURL;
            if (string.IsNullOrWhiteSpace(server))
            {
                server = $"{Properties.Settings.Default.DatabaseServerName}:{Properties.Settings.Default.DatabaseServerPort}";
            }
            toolStripStatusLabelFill.Text = $"{Properties.Settings.Default.LogInUserId} at {server}";
        }

        private void ToolStripSplitButtonOptions_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (captureScreenToolStripMenuItem.Checked)
                {
                    var bounds = this.ParentForm.Bounds;
                    using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
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
    }
}