using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewComboBoxColumn : DataGridViewComboBoxColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewComboBoxColumn()
        {
            this.FlatStyle = FlatStyle.Flat;
            base.ValueType = typeof(string);
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

        public override object Clone()
        {
            TMTDataGridViewComboBoxColumn that = (TMTDataGridViewComboBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.DisplayMember = this.DisplayMember;
            that.ValueMember = this.ValueMember;

            return that;
        }
    }
}