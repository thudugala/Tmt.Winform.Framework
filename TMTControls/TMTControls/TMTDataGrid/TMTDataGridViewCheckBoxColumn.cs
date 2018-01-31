using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewCheckBoxColumn()
        {
            base.ValueType = typeof(string);
            this.TabStop = true;
        }

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

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        public override object Clone()
        {
            TMTDataGridViewCheckBoxColumn that = (TMTDataGridViewCheckBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.TabStop = this.TabStop;

            return that;
        }
    }
}