using BM.Library.Internal.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Library.DataAccess
{
    public class ClientData : IClientData
    {
        private const string _connectionString = "DefaultConnection";

        public List<ClientModel> GetClients()
        {
            var sqlString = "select * from Client order by Name";

            var sql = new SqlDataAccess();
            var output = sql.LoadData<ClientModel, dynamic>(sqlString, new { }, _connectionString);

            return output;
        }

        public void InsertClientData(ClientModel parameters)
        {
            var sqlString = "insert into Client (Name, HourlyRate, Email, PreBill, HasCutOff, CutOff, MinimumHours, BillingIncrement, RoundUpAfterXMinutes)" +
                "values (@Name, @HourlyRate, @Email, @PreBill, @HasCutOff, @CutOff, @MinimumHours, @BillingIncrement, @RoundUpAfterXMinutes)";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }

        public void UpdateClientData(ClientModel parameters)
        {
            var sqlString = "update Client set Name = @Name, HourlyRate = @HourlyRate, Email = @Email, PreBill = @PreBill, HasCutOff = @HasCutOff, " +
                "CutOff = @CutOff, MinimumHours = @MinimumHours, BillingIncrement = @BillingIncrement, RoundUpAfterXMinutes = @RoundUpAfterXMinutes " +
                "where Id = @Id";

            var sql = new SqlDataAccess();
            sql.SaveData(sqlString, parameters, _connectionString);
        }
    }
}
