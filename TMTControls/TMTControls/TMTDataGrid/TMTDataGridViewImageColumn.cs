using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewImageColumn : DataGridViewImageColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewImageColumn()
        {
            base.ValueType = typeof(byte[]);
        }

        [Category("Data"), DefaultValue(TypeCode.Byte)]
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
            TMTDataGridViewImageColumn that = (TMTDataGridViewImageColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;

            return that;
        }
    }
}