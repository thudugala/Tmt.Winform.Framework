using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    public class DbCalendarColumn : DataGridViewColumn, IDbColumn
    {
        public DbCalendarColumn()
            : base(new DbCalendarCell())
        {
            base.ValueType = typeof(DateTime);
            base.SortMode = DataGridViewColumnSortMode.Automatic;
            this.TabStop = true;
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DbCalendarCell)))
                {
                    throw new InvalidCastException($"Must be a {nameof(DbCalendarCell)}");
                }
                base.CellTemplate = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

        [Category("Data"), DefaultValue(TypeCode.DateTime)]
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
            if (base.Clone() is DbCalendarColumn that)
            {
                that.DataPropertyMandatory = this.DataPropertyMandatory;
                that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
                that.DataPropertyType = this.DataPropertyType;
                that.TabStop = this.TabStop;

                return that;
            }
            return null;
        }
    }
}