using System;

namespace TMTControls
{
    public class SearchEntity
    {
        public string Caption { get; set; }

        public string ColumnName { get; set; }

        public Type DataType { get; set; }

        public string Value { get; set; }

        public bool IsCheckBox { get; set; }
        public object FalseValue { get; set; }
        public object TrueValue { get; set; }
        public object IndeterminateValue { get; set; }

        public string ListOfValueView { get; set; }
    }
}