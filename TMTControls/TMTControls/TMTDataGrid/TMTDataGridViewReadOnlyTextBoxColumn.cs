using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewReadOnlyTextBoxColumn : DataGridViewTextBoxColumn
    {
        public TMTDataGridViewReadOnlyTextBoxColumn()
        {
            base.ValueType = typeof(string);
        }

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
            set
            {
                base.ReadOnly = true;
            }
        }

        public override DataGridViewCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                value.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
                base.DefaultCellStyle = value;
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
                if (value == TypeCode.Decimal)
                {
                    this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.DefaultCellStyle.Format = "N2";
                }
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyIsFuntion
        {
            get
            {
                return this.GetDataSourceInformation().IsFuntion;
            }
            set
            {
                this.GetDataSourceInformation().IsFuntion = value;
            }
        }

        public override object Clone()
        {
            TMTDataGridViewReadOnlyTextBoxColumn that = (TMTDataGridViewReadOnlyTextBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.DataPropertyIsFuntion = this.DataPropertyIsFuntion;

            return that;
        }
    }
}