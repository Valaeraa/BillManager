using BM.Library.Models;

namespace BM.Library.DataAccess
{
    public interface IDefaultsData
    {
        void DeleteDefaultsData();
        DefaultsModel GetDefaultsData();
        void InsertDefaultsData(DefaultsModel parameters);
    }
}