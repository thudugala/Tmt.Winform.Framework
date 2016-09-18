using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextButtonBoxColumn : DataGridViewColumn
    {
        private EventHandler buttonClick;

        public TMTDataGridViewTextButtonBoxColumn()
            : base(new TMTDataGridViewTextButtonBoxCell())
        {
            base.ValueType = typeof(string);
            base.SortMode = DataGridViewColumnSortMode.Automatic;
            base.DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True };
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public EventHandler ButtonClick
        {
            get
            {
                return this.buttonClick;
            }
            set
            {
                TMTDataGridViewTextButtonBoxCell cell = CellTemplate as TMTDataGridViewTextButtonBoxCell;
                if (cell != null)
                {
                    if (value != null)
                    {
                        cell.ButtonClickHandler += value;
                    }
                    else if (buttonClick != null)
                    {
                        cell.ButtonClickHandler -= this.buttonClick;
                    }
                }
                this.buttonClick = value;
            }
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
                    !value.GetType().IsAssignableFrom(typeof(TMTDataGridViewTextButtonBoxCell)))
                {
                    throw new InvalidCastException("Must be a TMTDataGridViewTextButtonBoxCell");
                }
                base.CellTemplate = value;
                TMTDataGridViewTextButtonBoxCell cell = CellTemplate as TMTDataGridViewTextButtonBoxCell;
                if (cell != null)
                {
                    cell.ButtonClickHandler = this.ButtonClick;
                }
            }
        }

        [Category("Data"), RefreshProperties(RefreshProperties.All)]
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
            TMTDataGridViewTextButtonBoxColumn that = (TMTDataGridViewTextButtonBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.LOVViewName = this.LOVViewName;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;

            return that;
        }
    }
}