using BM.Library.Internal.DataAccess;
using BM.Library.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Library.DataAccess
{
    public class PaymentData : IPaymentData
    {
        private const string _connectionString = "DefaultConnection";

        public void InsertPaymentData(PaymentModel parameters)
        {
            var sqlString = "insert into Payment (ClientId, Hours, Amount) " +
                "values (@ClientId, @Hours, @Amount)";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }

        public void UpdatePaymentData(PaymentModel parameters)
        {
            var sqlString = "update Payment set Hours = @Hours, " +
                "Amount = @Amount where Id = @Id";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }

        public List<PaymentModel> GetPayments()
        {
            var sqlString = "select * from Payment where ClientId = @ClientId";

            var sql = new SqlDataAccess();

            var output = sql.LoadData<PaymentModel, dynamic>(sqlString, new { }, _connectionString);

            return output;
        }
    }
}
