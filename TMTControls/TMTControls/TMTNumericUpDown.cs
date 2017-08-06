using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTNumericUpDown : NumericUpDown
    {
        public TMTNumericUpDown()
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
        public bool KeyColum { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum { get; set; }
    }
}