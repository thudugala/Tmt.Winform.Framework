using System;
using System.Collections.Generic;

namespace TMTControls
{
    public class SearchEntity
    {
        public string Caption { get; set; }

        public string ColumnName { get; set; }

        public Type DataType { get; set; }

        public object FalseValue { get; set; }
        public object IndeterminateValue { get; set; }
        public bool IsCheckBox { get; set; }
        public bool IsFuntion { get; set; }
        public string ListOfValueView { get; set; }
        public object TrueValue { get; set; }
        public string Value { get; set; }

        public IList<string> GetSearchValueList()
        {
            return Value?.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string ToString()
        {
            return $"[{Value}], {DataType}, {ColumnName}, {IsFuntion}";
        }
    }
}