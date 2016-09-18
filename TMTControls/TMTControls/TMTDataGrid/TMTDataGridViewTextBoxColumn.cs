using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextBoxColumn : DataGridViewTextBoxColumn
    {
        public TMTDataGridViewTextBoxColumn()
        {
            base.ValueType = typeof(string);
            this.ValidateType = MaskValidateType.None;
            this.CountryCode = "lk";
        }

        [Category("Design"), DefaultValue(MaskValidateType.None)]
        public MaskValidateType ValidateType { get; set; }

        [Category("Design"), DefaultValue("lk")]
        public string CountryCode { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DataPropertyType
        {
            get
            {
                return Type.GetTypeCode(base.ValueType);
            }
            set
            {
                base.ValueType = Type.GetType("System." + value);
            }
        }

        [Category("LOV Data")]
        public string LOVViewName
        {
            get
            {
                return this.GetDataSourceInformation().LovViewName;
            }
            set
            {
                this.GetDataSourceInformation().LovViewName = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory
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
        public bool DataPropertyPrimaryKey
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

        public override object Clone()
        {
            TMTDataGridViewTextBoxColumn that = (TMTDataGridViewTextBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.LOVViewName = this.LOVViewName;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.ValidateType = this.ValidateType;
            that.CountryCode = this.CountryCode;

            return that;
        }
    }
}