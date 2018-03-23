using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TMT.Controls.WinForms
{
    public class DataLoadEventArgs : EventArgs
    {
        public DataLoadEventArgs()
        {
            this.ChangedDataSet = new DataSet
            {
                Locale = CultureInfo.InvariantCulture
            };
            this.DataLoadArgList = new List<DataLoadArg>();
        }

        public DataSet ChangedDataSet { get; }
        public IList<DataLoadArg> DataLoadArgList { get; }
    }

    public class DataLoadArg
    {
        public DataLoadArg()
        {
            this.SelectedRowIndexList = new HashSet<int>();
            this.ColumnDbNameList = new List<string>();
            this.SearchConditionList = new List<SearchEntity>();

            this.IsCaseSensitive = true;
            this.LoadSchema = true;
        }

        public string ViewName { get; set; }
        public string DefaultWhereStatement { get; set; }
        public string DefaultOrderByStatement { get; set; }
        public bool IsCaseSensitive { get; set; }
        public bool LimitLoad { get; set; }
        public bool LoadSchema { get; set; }

        public ISet<int> SelectedRowIndexList { get; }
        public List<string> ColumnDbNameList { get; }
        public List<SearchEntity> SearchConditionList { get; }
    }
}