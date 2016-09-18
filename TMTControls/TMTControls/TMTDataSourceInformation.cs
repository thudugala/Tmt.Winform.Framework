using System;
using System.ComponentModel;

namespace TMTControls
{
    public class TMTDataSourceInformation
    {
        public TMTDataSourceInformation()
        {
            this.DbColumnType = TypeCode.Empty;
            this.DbColumnName = string.Empty;
            this.DbLabelText = string.Empty;
            this.KeyColum = false;
            this.MandatoryColum = false;
            this.LovViewName = string.Empty;
            this.LovText = string.Empty;

            this.FalseValue = "FALSE";
            this.IndetetermibateValue = "FALSE";
            this.TrueValue = "TRUE";
            this.DbValue = string.Empty;
        }

        public string DbColumnName { get; set; }
        public TypeCode DbColumnType { get; set; }
        public string DbLabelText { get; set; }

        [DefaultValue(false)]
        public bool KeyColum { get; set; }

        public string LovText { get; set; }

        public string LovViewName { get; set; }

        [DefaultValue(false)]
        public bool MandatoryColum { get; set; }

        public string FalseValue { get; set; }
        public string IndetetermibateValue { get; set; }
        public string TrueValue { get; set; }
        public string DbValue { get; set; }

        public Type GetDbColumnType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}