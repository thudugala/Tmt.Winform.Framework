using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewButtonColumn : DataGridViewButtonColumn
    {
        public TMTDataGridViewButtonColumn()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.Text = "∙∙∙";
            this.UseColumnTextForButtonValue = true;
        }

        public override DataGridViewCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                value.Alignment = DataGridViewContentAlignment.TopCenter;
                value.NullValue = "∙∙∙";
                value.Padding = new Padding(2);
                value.BackColor = Color.FromArgb(224, 224, 224);

                base.DefaultCellStyle = value;
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

        public override object Clone()
        {
            TMTDataGridViewButtonColumn that = (TMTDataGridViewButtonColumn)base.Clone();

            that.LOVViewName = this.LOVViewName;

            return that;
        }
    }
}