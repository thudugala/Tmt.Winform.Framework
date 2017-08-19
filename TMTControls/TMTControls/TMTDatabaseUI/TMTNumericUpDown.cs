using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDatabaseUI
{
    public class TMTNumericUpDown : NumericUpDown, ITMTDatabaseUIControl
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
        public bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        public string GetLableText()
        {           
            return this.ConnectedLabel?.Text;
        }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}