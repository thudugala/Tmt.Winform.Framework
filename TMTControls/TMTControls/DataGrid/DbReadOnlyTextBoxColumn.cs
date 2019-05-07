using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    public class DbReadOnlyTextBoxColumn : DataGridViewTextBoxColumn, IDbColumn
    {
        public DbReadOnlyTextBoxColumn()
        {
            base.ValueType = typeof(string);
            this.TabStop = true;
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyEditAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyIsFuntion { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

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

        public override DataGridViewCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                if (value != null)
                {
                    value.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
                }
                base.DefaultCellStyle = value;
            }
        }

        [Category("LOV Data"), DefaultValue("")]
        public string ListOfValueViewName { get; set; }

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

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        public override object Clone()
        {
            DbReadOnlyTextBoxColumn that = (DbReadOnlyTextBoxColumn)base.Clone();

            that.ListOfValueViewName = this.ListOfValueViewName;
            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyEditAllowed = this.DataPropertyEditAllowed;
            that.DataPropertyIsFuntion = this.DataPropertyIsFuntion;
            that.TabStop = this.TabStop;

            return that;
        }
    }
}