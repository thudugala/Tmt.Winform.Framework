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
            this.GetDataSourceInformation().DbValue = this.IndetetermibateValue;
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

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.GetDataSourceInformation().DbLabelText = base.Text;
            }
        }

        [Category("Data"), DefaultValue("")]
        public string DbValue
        {
            get
            {
                return this.GetDataSourceInformation().DbValue;
            }
            set
            {
                this.GetDataSourceInformation().DbValue = value;
                this._DbValueUpdateLooked = true;
                this.Checked = (value == this.TrueValue) ? true : false;
                this._DbValueUpdateLooked = false;
            }
        }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName
        {
            get
            {
                return this.GetDataSourceInformation().DbColumnName;
            }
            set
            {
                this.GetDataSourceInformation().DbColumnName = value;
            }
        }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType
        {
            get
            {
                return this.GetDataSourceInformation().DbColumnType;
            }
            set
            {
                this.GetDataSourceInformation().DbColumnType = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum
        {
            get
            {
                return this.GetDataSourceInformation().MandatoryColum;
            }
            set
            {
                this.GetDataSourceInformation().MandatoryColum = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum
        {
            get
            {
                return this.GetDataSourceInformation().KeyColum;
            }
            set
            {
                this.GetDataSourceInformation().KeyColum = value;
            }
        }

        [Category("Data"), DefaultValue("FALSE")]
        public string FalseValue
        {
            get
            {
                return this.GetDataSourceInformation().FalseValue;
            }
            set
            {
                this.GetDataSourceInformation().FalseValue = value;
            }
        }

        [Category("Data"), DefaultValue("FALSE")]
        public string IndetetermibateValue
        {
            get
            {
                return this.GetDataSourceInformation().IndetetermibateValue;
            }
            set
            {
                this.GetDataSourceInformation().IndetetermibateValue = value;
            }
        }

        [Category("Data"), DefaultValue("TRUE")]
        public string TrueValue
        {
            get
            {
                return this.GetDataSourceInformation().TrueValue;
            }
            set
            {
                this.GetDataSourceInformation().TrueValue = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
            }
        }
    }
}