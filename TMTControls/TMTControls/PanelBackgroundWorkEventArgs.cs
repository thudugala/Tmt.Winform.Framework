using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TMTControls
{
    public class PanelBackgroundWorkEventArgs : EventArgs
    {
        public PanelBackgroundWorkEventArgs()
        {
            this.ChangedDataSet = new DataSet
            {
                Locale = CultureInfo.InvariantCulture
            };
            this.ChildViewList = new List<PanelBackgroundWorkEventArgs>();
            this.HeaderViewColumnDbNameList = new List<string>();
            this.SaveResults = new List<int>();
            this.IsCaseSensitive = true;
            this.LoadSchema = true;
            this.SelectedRowIndexList = new HashSet<int>();
        }

        public PanelBackgroundWorkType WorkType { get; set; }

        public DataSet ChangedDataSet { get; }
        public IList<string> HeaderViewColumnDbNameList { get; }

        public void HeaderViewColumnDbNameListAddRange(IList<string> headerViewColumnDbNameList)
        {
            (HeaderViewColumnDbNameList as List<string>).AddRange(headerViewColumnDbNameList);
        }

        public DataTable HeaderSearchConditionTable { get; set; }
        public string HeaderViewName { get; set; }
        public string DefaultWhereStatement { get; set; }
        public string DefaultOrderByStatement { get; set; }

        public IList<PanelBackgroundWorkEventArgs> ChildViewList { get; private set; }

        public IList<int> SaveResults { get; private set; }

        public void SaveResultsAddRange(IEnumerable<int> saveResultList)
        {
            (SaveResults as List<int>).AddRange(saveResultList);
        }

        public bool IsCaseSensitive { get; set; }
        public bool LimitLoad { get; set; }
        public bool LoadSchema { get; set; }

        public ISet<int> SelectedRowIndexList { get; }
    }
}