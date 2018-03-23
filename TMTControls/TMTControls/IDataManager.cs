using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace TMT.Controls.WinForms
{
    public interface IDataManager
    {
        Task<DataTable> LoadDataFromDatabase(in DataLoadArg loadArg, in int? limitOffset);

        Task<DataTable> LoadListOfValuesDataFromDatabase(in ListOfValueLoadingEventArgs e, in IList<string> listOfValueViewColumnDbNameList, in int? limitOffset);

        Task<int[]> SaveDataToDatabase(in DataSaveEventArgs saveArg);
    }
}