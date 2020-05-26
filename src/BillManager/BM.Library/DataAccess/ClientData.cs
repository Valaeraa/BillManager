using BM.Library.Internal.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Library.DataAccess
{
    public class ClientData
    {
        private const string _connectionString = "DefaultConnection";

        public List<ClientModel> GetClients()
        {
            var sql = new SqlDataAccess();
            var sqlString = "select * from Client";

            var output = sql.LoadData<ClientModel, dynamic>(sqlString, new { }, _connectionString);

            return output;
        }
    }
}
