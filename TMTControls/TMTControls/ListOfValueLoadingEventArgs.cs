using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    public class ListOfValueLoadingEventArgs : EventArgs
    {
        public IDictionary<string, string> FilterColumns;

        public ListOfValueLoadingEventArgs()
        {
            this.SearchConditionList = new List<SearchEntity>();
            this.FilterColumns = new Dictionary<string, string>();
        }

        public bool Handled { get; set; }

        public bool IsValidate { get; set; }

        public bool LimitLoad { get; set; }
        public DataTable ListOfValueDataTable { get; set; }

        public string ListOfValueHeaderText { get; set; }
        public string ListOfValueViewName { get; set; }
        public string PrimaryColumnName { get; set; }

        public Type PrimaryColumnType { get; set; }

        public string PrimaryColumnValue { get; set; }

        public DataGridViewRow Row { get; set; }
        public int RowIndex { get; set; }
        public IList<SearchEntity> SearchConditionList { get; }
    }
}