using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DatabaseUI
{
    [ToolboxBitmap(typeof(NumericUpDown))]
    public class DbNumericUpDown : NumericUpDown, IDbControl
    {
        public DbNumericUpDown()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.Int64;
            this.Maximum = 1000000;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.Int64)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }

        public string GetLableText()
        {
            return this.ConnectedLabel?.Text;
        }
    }
}