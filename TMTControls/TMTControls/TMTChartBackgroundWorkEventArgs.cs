using System;
using System.Data;

namespace TMTControls
{
    public class TMTChartBackgroundWorkEventArgs : EventArgs
    {
        public TMTChartBackgroundWorkEventArgs()
        {
            this.SearchConditionTable = TMTExtend.GetSearchConditionTable();
        }

        public DataTable SearchConditionTable { get; }
        public DataTable ViewData { get; set; }
        public string ViewName { get; set; }
    }
}