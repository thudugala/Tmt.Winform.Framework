using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewCalendarColumn : DataGridViewColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewCalendarColumn()
            : base(new TMTDataGridViewCalendarCell())
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
                    !value.GetType().IsAssignableFrom(typeof(TMTDataGridViewCalendarCell)))
                {
                    throw new InvalidCastException($"Must be a {nameof(TMTDataGridViewCalendarCell)}");
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
            TMTDataGridViewCalendarColumn that = (TMTDataGridViewCalendarColumn)base.Clone();

            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.TabStop = this.TabStop;

            return that;
        }
    }
}