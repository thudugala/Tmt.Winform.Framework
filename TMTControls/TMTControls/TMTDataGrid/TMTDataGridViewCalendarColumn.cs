using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewCalendarColumn : DataGridViewColumn
    {
        public TMTDataGridViewCalendarColumn()
            : base(new TMTDataGridViewCalendarCell())
        {
            base.ValueType = typeof(DateTime);
            base.SortMode = DataGridViewColumnSortMode.Automatic;
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
                    throw new InvalidCastException("Must be a TMTDataGridViewCalendarCell");
                }
                base.CellTemplate = value;
            }
        }

        [Category("LOV Data")]
        public string LOVViewName
        {
            get
            {
                return this.GetDataSourceInformation().LovViewName;
            }
            set
            {
                this.GetDataSourceInformation().LovViewName = value;
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
            TMTDataGridViewCalendarColumn that = (TMTDataGridViewCalendarColumn)base.Clone();

            that.LOVViewName = this.LOVViewName;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;

            return that;
        }
    }
}