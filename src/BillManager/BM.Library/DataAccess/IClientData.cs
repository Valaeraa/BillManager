using BM.Library.Models;
using System.Collections.Generic;

namespace BM.Library.DataAccess
{
    public interface IClientData
    {
        List<ClientModel> GetClients();
        void InsertClientData(ClientModel model);
    }
}