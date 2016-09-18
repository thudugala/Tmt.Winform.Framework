using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public TMTDataGridViewCheckBoxColumn()
        {
            base.ValueType = typeof(string);
        }

        [Category("Data")]
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

        public override object Clone()
        {
            TMTDataGridViewCheckBoxColumn that = (TMTDataGridViewCheckBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;

            return that;
        }
    }
}