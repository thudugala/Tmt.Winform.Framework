using System;
using System.Data;
using System.Windows.Forms;

namespace TMTControls
{
    public class ListOfValueLoadingEventArgs : EventArgs
    {
        public bool IsValidate { get; set; }

        public DataTable ListOfValueDataTable { get; set; }

        public string PrimaryColumnName { get; set; }

        public string PrimaryColumnType { get; set; }

        public string PrimaryColumnValue { get; set; }

        public int RowIndex { get; set; }

        public DataGridViewRow Row { get; set; }

        public DataTable SearchConditionTable { get; set; }

        public string ListOfValueViewName { get; set; }

        public string ListOfValueHeaderText { get; set; }

        public bool LimitLoad { get; set; }
    }
}