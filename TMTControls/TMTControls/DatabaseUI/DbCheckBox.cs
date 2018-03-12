using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DatabaseUI
{
    [ToolboxBitmap(typeof(CheckBox))]
    public class DbCheckBox : CheckBox, IDbControl
    {
        private bool _DbValueUpdateLooked = false;

        public DbCheckBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.FalseValue = Boolean.FalseString.ToUpper(CultureInfo.InvariantCulture);
            this.IndeterminateValue = this.FalseValue;
            this.TrueValue = Boolean.TrueString.ToUpper(CultureInfo.InvariantCulture);

            this.DbValue = this.FalseValue;
            this.CheckedChanged += BaseCheckBox_CheckedChanged;

            this.ResumeLayout(false);
        }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Browsable(false), DefaultValue("FALSE")]
        public string DbValue
        {
            get
            {
                return (this.Checked) ? this.TrueValue : this.FalseValue;
            }
            set
            {
                this._DbValueUpdateLooked = true;
                this.Checked = (value == this.TrueValue) ? true : false;
                this._DbValueUpdateLooked = false;
            }
        }

        [Category("Data"), DefaultValue("FALSE")]
        public string FalseValue { get; set; }

        [Category("Data"), DefaultValue("FALSE")]
        public string IndeterminateValue { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        [Category("Data"), DefaultValue("TRUE")]
        public string TrueValue { get; set; }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }

        public string GetLableText()
        {
            return this.Text;
        }

        private void BaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this._DbValueUpdateLooked == false)
            {
                this.DbValue = (this.Checked) ? this.TrueValue : this.FalseValue;
            }
        }
    }
}