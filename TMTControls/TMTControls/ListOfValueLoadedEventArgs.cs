using System;
using System.Collections.Generic;

namespace TMTControls
{
    public class ListOfValueLoadedEventArgs : EventArgs
    {
        public ListOfValueLoadedEventArgs()
        {
            this.SelectedRow = new Dictionary<string, object>();
        }

        public ListOfValueLoadedEventArgs(Dictionary<string, object> selectedRow)
        {
            this.SelectedRow = selectedRow;
        }

        public bool IsValidate { get; set; }

        public string PrimaryColumnName { get; set; }

        public int RowIndex { get; set; }

        public string ListOfValueViewName { get; set; }

        public Dictionary<string, object> SelectedRow { get; }
    }
}