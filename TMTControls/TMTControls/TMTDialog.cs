using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTDialog : Form
    {
        public TMTDialog()
        {
            InitializeComponent();
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

        [Category("Appearance")]
        public Image Image
        {
            get => labelHeader.Image;
            set => labelHeader.Image = value;
        }
    }
}