using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace TMTControls
{
    public interface IDataManager
    {
        Task<DataTable> LoadDataFromDatabase(DataLoadArg loadArg, int? limitOffset);

        Task<DataTable> LoadListOfValuesDataFromDatabase(ListOfValueLoadingEventArgs e, IList<string> listOfValueViewColumnDbNameList, int? limitOffset);

        Task SaveDataToDatabase(DataSaveArg saveArg);
    }
}