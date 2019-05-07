using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace TMT.Controls.WinForms
{
    public interface IDataManager
    {
        void ChangePassword(in string newPassword);

        Task<DataTable> LoadDataFromDatabase(DataLoadArg loadArg, int? limitOffset);

        Task<DataTable> LoadListOfValuesDataFromDatabase(in ListOfValueLoadingEventArgs e, in IList<string> listOfValueViewColumnDbNameList, in int? limitOffset);

        Task<int[]> SaveDataToDatabase(DataSaveEventArgs saveArg);

        void TestConnection();
    }
}