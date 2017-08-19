using System;
using System.Data;

namespace TMTControls
{
    public class DataValidatingEventArgs : EventArgs
    {
        public bool CancelSave { get; set; }

        public DataSet DataToBeSaved { get; set; }
    }
}