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
            this.KeyColumn = false;
            this.MandatoryColumn = false;
            this.LovViewName = string.Empty;
            this.LovText = string.Empty;

            this.FalseValue = "FALSE";
            this.IndeterminateValue = "FALSE";
            this.TrueValue = "TRUE";
            this.DbValue = string.Empty;
            this.IsFuntion = false;
        }

        public string DbColumnName { get; set; }
        public TypeCode DbColumnType { get; set; }
        public string DbLabelText { get; set; }

        [DefaultValue(false)]
        public bool KeyColumn { get; set; }

        public string LovText { get; set; }

        public string LovViewName { get; set; }

        [DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        [DefaultValue(false)]
        public bool EditAllowed { get; set; }

        public string FalseValue { get; set; }
        public string IndeterminateValue { get; set; }
        public string TrueValue { get; set; }
        public string DbValue { get; set; }

        [DefaultValue(false)]
        public bool IsFuntion { get; set; }

        public Type GetDbColumnType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}