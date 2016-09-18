using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    [ToolboxBitmap(typeof(UserControl))]
    [DefaultEvent("TextChanged")]
    public partial class TMTTextButtonBoxBase : UserControl
    {
        public TMTTextButtonBoxBase()
        {
            InitializeComponent();

            InnerTextBox.GotFocus += InnerTextBox_GotFocus;
        }

        public void InnerTextBox_GotFocus(object sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }

        public event EventHandler ButtonClick;

        public override string Text
        {
            get
            {
                return InnerTextBox.Text;
            }
            set
            {
                InnerTextBox.Text = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                return InnerTextBox.BackColor;
            }
            set
            {
                InnerTextBox.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return InnerTextBox.ForeColor;
            }
            set
            {
                InnerTextBox.ForeColor = value;
            }
        }

        public HorizontalAlignment TextAlign
        {
            get
            {
                return InnerTextBox.TextAlign;
            }
            set
            {
                InnerTextBox.TextAlign = value;
            }
        }

        public void ClearUndo()
        {
            InnerTextBox.ClearUndo();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(this, e);
                if (InnerTextBox.Focused == false)
                {
                    InnerTextBox.Focus();
                }
            }
        }

        private void InnerTextBox_Validated(object sender, EventArgs e)
        {
            this.OnValidated(e);
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }
    }
}