using System;
using System.Collections.Generic;
using System.Data;

namespace TMTControls
{
    public enum PanelBackgroundWorkeType
    {
        Save,
        Load,
        LoadChild
    }

    public class PanelBackgroundWorkeArg : EventArgs
    {
        public PanelBackgroundWorkeArg()
        {
            this.ChangedDataSet = new DataSet();
            this.ChildViewList = new List<KeyValuePair<string, DataTable>>();
            this.SaveResults = new List<int>();
            this.IsCaseSensitive = true;
        }

        public PanelBackgroundWorkeType Type { get; set; }

        public DataSet ChangedDataSet { get; set; }
        public List<string> HeaderViewColumnDbNameList { get; set; }
        public DataTable HeaderSearchConditionTable { get; set; }
        public string HeaderViewName { get; set; }

        public List<KeyValuePair<string, DataTable>> ChildViewList { get; private set; }

        public List<int> SaveResults { get; private set; }
        public bool IsCaseSensitive { get; set; }
        public int? ViewRowCount { get; set; }
    }
}