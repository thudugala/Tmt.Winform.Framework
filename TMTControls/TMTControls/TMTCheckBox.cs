using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTCheckBox : CheckBox
    {
        private bool _DbValueUpdateLooked = false;

        public TMTCheckBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.FalseValue = Boolean.FalseString.ToUpper();
            this.IndeterminateValue = this.FalseValue;
            this.TrueValue = Boolean.TrueString.ToUpper();

            this.DbValue = this.FalseValue;
            this.CheckedChanged += TMTCheckBox_CheckedChanged;

            this.ResumeLayout(false);
        }

        private void TMTCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this._DbValueUpdateLooked == false)
            {
                this.DbValue = (this.Checked) ? this.TrueValue : this.FalseValue;
            }
        }

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

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum { get; set; }

        [Category("Data"), DefaultValue("FALSE")]
        public string FalseValue { get; set; }

        [Category("Data"), DefaultValue("FALSE")]
        public string IndeterminateValue { get; set; }

        [Category("Data"), DefaultValue("TRUE")]
        public string TrueValue { get; set; }
    }
}