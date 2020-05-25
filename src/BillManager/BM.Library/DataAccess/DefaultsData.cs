using BM.Library.Internal.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Library.DataAccess
{
    public class DefaultsData
    {
        private const string _connectionString = "DefaultConnection";

        public DefaultsModel GetDefaultsData()
        {
            var sqlString = "select * from Defaults";

            var sql = new SqlDataAccess();

            var output = sql.LoadData<DefaultsModel, dynamic>(sqlString, new { }, _connectionString).FirstOrDefault();

            return output;
        }

        public void InsertDefaultsData(DefaultsModel model)
        {
            var sqlString = "insert into Defaults (HourlyRate, PreBill, HasCutOff, CutOff, MinimumHours, BillingIncrement, RoundUpAfterXMinutes) " +
                "values (@HourlyRate, @PreBill, @HasCutOff, @CutOff, @MinimumHours, @BillingIncrement, @RoundUpAfterXMinutes)";

            var sql = new SqlDataAccess();

            sql.SaveData(sqlString, model, _connectionString);

        }

        public void DeleteDefaultsData()
        {
            var sqlString = "delete from Defaults";

            var sql = new SqlDataAccess();

            sql.SaveData(sqlString, new DefaultsData { }, _connectionString);
        }
    }
}
