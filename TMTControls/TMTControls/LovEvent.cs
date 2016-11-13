using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TMTControls
{
    public delegate void LovLoadedEventHandler(object sender, LovLoadedEventArgs e);

    public delegate void LovLoadingEventHandler(object sender, LovLoadingEventArgs e);

    public class LovLoadedEventArgs : EventArgs
    {
        public string PrimaryColumnName { get; set; }

        public int RowIndex { get; set; }

        public Dictionary<string, object> SelectedRow { get; set; }
    }

    public class LovLoadingEventArgs : EventArgs
    {
        public bool IsValidate { get; set; }

        public DataTable LovDataTable { get; set; }

        public string PrimaryColumnName { get; set; }

        public int RowIndex { get; set; }

        public DataGridViewRow Row { get; set; }

        public DataTable SearchConditionTable { get; set; }

        public string LovViewName { get; set; }
    }
}