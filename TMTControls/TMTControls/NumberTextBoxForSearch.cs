using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    [ToolboxItem(false)]
    public class NumberTextBoxForSearch : TextBox
    {
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.', '<', '=', '>', '!', ';' };

        public NumberTextBoxForSearch()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // NumberTextBoxForSearch
            //
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberTextBoxForSearch_KeyPress);
            this.ResumeLayout(false);
        }

        private void NumberTextBoxForSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) ||
                (this.allowedKeySet.Contains(e.KeyChar)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}