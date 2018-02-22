using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextButtonBoxColumn : DataGridViewColumn, ITMTDataGridViewColumn
    {
        public TMTDataGridViewTextButtonBoxColumn()
            : base(new TMTDataGridViewTextButtonBoxCell())
        {
            base.ValueType = typeof(string);
            base.SortMode = DataGridViewColumnSortMode.Automatic;
            base.DefaultCellStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.True
            };
            this.TabStop = true;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                    throw new InvalidCastException($"Must be a {nameof(TMTDataGridViewTextButtonBoxCell)}");
                }
                base.CellTemplate = value;
            }
        }

        [Category("Behavior"), DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing { get; set; }

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
            }
        }

        [Category("LOV Data"), DefaultValue("")]
        public string ListOfValueViewName { get; set; }

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        public override object Clone()
        {
            var that = (TMTDataGridViewTextButtonBoxColumn)base.Clone();

            that.ListOfValueViewName = this.ListOfValueViewName;
            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.TabStop = this.TabStop;
            that.CharacterCasing = this.CharacterCasing;

            return that;
        }
    }
}