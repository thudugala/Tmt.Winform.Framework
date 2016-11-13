using System.ComponentModel;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewDataSourceInformation
    {
        public TMTDataGridViewDataSourceInformation()
        {
            this.LovViewName = string.Empty;
            this.KeyColum = false;
            this.MandatoryColum = false;
            this.EditAllowed = false;
            this.IsFuntion = false;
        }

        [DefaultValue("")]
        public string LovViewName { get; set; }

        [DefaultValue(false)]
        public bool KeyColum { get; set; }

        [DefaultValue(false)]
        public bool MandatoryColum { get; set; }

        [DefaultValue(false)]
        public bool EditAllowed { get; set; }

        [DefaultValue(false)]
        public bool IsFuntion { get; set; }
    }
}