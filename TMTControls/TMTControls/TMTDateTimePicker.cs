using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTDateTimePicker : DateTimePicker
    {
        public TMTDateTimePicker()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.DateTime;
            this.Format = DateTimePickerFormat.Short;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.DateTime), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum { get; set; }
    }
}