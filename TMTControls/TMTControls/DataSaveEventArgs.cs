using System;
using System.Data;
using System.Globalization;

namespace TMT.Controls.WinForms
{
    public class DataSaveEventArgs : EventArgs
    {
        public DataSaveEventArgs()
        {
            this.ChangedDataSet = new DataSet
            {
                Locale = CultureInfo.InvariantCulture
            };
        }

        public DataSet ChangedDataSet { get; }
        public object GeneratedKey { get; set; }
    }
}