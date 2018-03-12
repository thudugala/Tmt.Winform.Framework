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
            this.SaveResults = new List<int>();
        }

        public DataSet ChangedDataSet { get; }
        public IList<int> SaveResults { get; private set; }
        public object GeneratedKey { get; set; }

        public void SaveResultsAddRange(IEnumerable<int> saveResultList)
        {
            if (saveResultList != null)
            {
                (SaveResults as List<int>).AddRange(saveResultList);
            }
        }

        public void Dispose()
        {
            if (ChangedDataSet != null)
            {
                ChangedDataSet.Dispose();
            }
        }
    }
}