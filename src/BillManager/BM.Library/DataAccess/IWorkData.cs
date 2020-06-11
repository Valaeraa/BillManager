using BM.Library.Models;
using System.Collections.Generic;

namespace BM.Library.DataAccess
{
    public interface IWorkData
    {
        List<WorkModel> GetWorkData();
        void InsertWorkData(WorkModel parameters);
        void UpdateWorkData(WorkModel parameters);
    }
}