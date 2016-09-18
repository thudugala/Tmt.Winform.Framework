using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTButton : Button
    {
        public TMTButton()
        {
            this.SuspendLayout();

            //this.AutoSize = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.FromArgb(224, 224, 224);
            this.FlatAppearance.BorderColor = Color.Silver;
            this.FlatAppearance.BorderSize = 0;

            this.ResumeLayout(false);
        }
    }
}