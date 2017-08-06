using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewLinkColumn : DataGridViewLinkColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewLinkColumn()
        {
            base.ValueType = typeof(string);
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyEditAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyIsFuntion { get; set; }

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
                if (value == TypeCode.Decimal ||
                    value == TypeCode.Double)
                {
                    this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.DefaultCellStyle.Format = "N2";
                }
            }
        }

        public override object Clone()
        {
            TMTDataGridViewLinkColumn that = (TMTDataGridViewLinkColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyEditAllowed = this.DataPropertyEditAllowed;
            that.DataPropertyIsFuntion = this.DataPropertyIsFuntion;

            return that;
        }
    }
}