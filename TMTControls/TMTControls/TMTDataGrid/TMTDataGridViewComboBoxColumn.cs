using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewComboBoxColumn : DataGridViewComboBoxColumn
    {
        public TMTDataGridViewComboBoxColumn()
        {
            this.FlatStyle = FlatStyle.Flat;
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