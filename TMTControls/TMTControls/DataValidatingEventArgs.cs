using System;
using System.Data;

namespace TMT.Controls.WinForms
{
    public class DataValidatingEventArgs : EventArgs
    {
        public bool CancelSave { get; set; }

        public DataSet DataToBeSaved { get; set; }
    }
}