using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TMT.Controls.WinForms
{
    public class DataSaveArg : IDisposable
    {
        public DataSaveArg()
        {
            this.ChangedDataSet = new DataSet
            {
                Locale = CultureInfo.InvariantCulture
            };
        }

        public DataSet ChangedDataSet { get; }
        public object GeneratedKey { get; set; }

        public void Dispose()
        {
            if (ChangedDataSet != null)
            {
                ChangedDataSet.Dispose();
            }
        }
    }
}