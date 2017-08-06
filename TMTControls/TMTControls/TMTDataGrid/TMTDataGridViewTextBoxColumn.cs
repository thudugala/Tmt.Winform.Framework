using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextBoxColumn : DataGridViewTextBoxColumn, ITMTDataGridViewColumn
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

        [Category("Data"), DefaultValue(TypeCode.String), RefreshProperties(RefreshProperties.All)]
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

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

        public override object Clone()
        {
            TMTDataGridViewTextBoxColumn that = (TMTDataGridViewTextBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;

            that.ValidateType = this.ValidateType;
            that.CountryCode = this.CountryCode;

            return that;
        }
    }
}