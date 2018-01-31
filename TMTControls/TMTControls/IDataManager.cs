using System.Collections.Generic;
using System.Data;

namespace TMTControls
{
    public interface IDataManager
    {
        DataTable LoadDataFromDatabase(DataLoadArg loadArg, int? limitOffset);

        void SaveDataToDatabase(DataSaveArg saveArg);

        DataTable LoadListOfValuesDataFromDatabase(ListOfValueLoadingEventArgs e, IList<string> listOfValueViewColumnDbNameList, int? limitOffset);
    }
}