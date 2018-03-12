using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    public class DbImageColumn : DataGridViewImageColumn, IDbColumn
    {
        public DbImageColumn()
        {
            base.ValueType = typeof(byte[]);
            this.TabStop = true;
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

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

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        public override object Clone()
        {
            DbImageColumn that = (DbImageColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.TabStop = this.TabStop;

            return that;
        }
    }
}