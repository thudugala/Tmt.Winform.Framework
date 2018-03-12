using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    public class BaseButton : Button
    {
        public BaseButton()
        {
            this.SuspendLayout();

            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.FromArgb(224, 224, 224);
            this.FlatAppearance.BorderColor = Color.Silver;
            this.FlatAppearance.BorderSize = 0;

            this.ResumeLayout(false);
        }
    }
}