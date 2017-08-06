using System.Collections.Generic;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTNumberTextBoxForSearch : TextBox
    {
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.', '<', '=', '>', '!', ';' };

        public TMTNumberTextBoxForSearch()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // TMTNumberTextBoxForSearch
            //
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TMTNumberTextBoxForSearch_KeyPress);
            this.ResumeLayout(false);
        }

        private void TMTNumberTextBoxForSearch_KeyPress(object sender, KeyPressEventArgs e)
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