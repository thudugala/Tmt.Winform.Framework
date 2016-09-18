using System;
using System.Collections.Generic;
using System.Data;

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
        public bool LoadAll { get; set; }

        public DataTable LovDataTable { get; set; }

        public string PrimaryColumnName { get; set; }

        public int RowIndex { get; set; }
    }
}