using BM.Library.Internal.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Library.DataAccess
{
    public class WorkData : IWorkData
    {
        private const string _connectionString = "DefaultConnection";

        public List<WorkModel> GetWorkData(int clientId)
        {
            var sqlString = "select * from Work where ClientId = @ClientId";

            var sql = new SqlDataAccess();
            var output = sql.LoadData<WorkModel, dynamic>(sqlString, new { ClientId = clientId }, _connectionString);

            return output;
        }

        public void InsertWorkData(WorkModel parameters)
        {
            string sqlString = "insert into Work (ClientId, Hours, Title, Description) " +
                "values (@ClientId, @Hours, @Title, @Description)";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }

        public void UpdateWorkData(WorkModel parameters)
        {
            var sqlString = "update Work set Hours = @Hours, Title = @Title, Description = @Description, " +
                "Paid = @Paid, PaymentId = @PaymentId where Id = @Id";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }
    }
}
