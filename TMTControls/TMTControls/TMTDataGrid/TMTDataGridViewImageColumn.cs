using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewImageColumn : DataGridViewImageColumn
    {
        public TMTDataGridViewImageColumn()
        {
            base.ValueType = typeof(byte[]);
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

        public override object Clone()
        {
            TMTDataGridViewImageColumn that = (TMTDataGridViewImageColumn)base.Clone();

            that.DataPropertyMandatory = this.DataPropertyMandatory;

            return that;
        }
    }
}