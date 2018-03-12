using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DatabaseUI
{
    [ToolboxBitmap(typeof(NumericUpDown))]
    public class DbCurrencyBox : DbNumericUpDown
    {
        public DbCurrencyBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.Decimal;
            this.Maximum = 1000000;
            this.TextAlign = HorizontalAlignment.Right;
            this.DecimalPlaces = 2;
            this.ThousandsSeparator = true;

            this.ResumeLayout(false);
        }
    }
}