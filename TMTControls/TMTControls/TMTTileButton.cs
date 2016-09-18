using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTTileButton : Button
    {
        public TMTTileButton()
        {
            this.SuspendLayout();

            this.SetTheme();

            this.Size = new System.Drawing.Size(150, 160);
            this.Margin = new Padding(20);

            this.ResumeLayout(false);
        }

        public void SetTheme()
        {
            this.BackColor = Properties.Settings.Default.TileButtonBackColor;
            this.ForeColor = Properties.Settings.Default.TileButtonForeColor;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.BorderColor = this.BackColor;
            this.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        }

        [Category("TMT")]
        public string PanelName { get; set; }
    }
}