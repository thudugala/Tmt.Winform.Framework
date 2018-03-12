using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    public partial class BaseDialog : Form
    {
        public BaseDialog()
        {
            InitializeComponent();
        }

        [Category("Appearance")]
        public Image Image
        {
            get => labelHeader.Image;
            set => labelHeader.Image = value;
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                labelHeader.Text = value;
            }
        }
    }
}