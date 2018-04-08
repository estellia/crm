using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Xgx.SyncData.DbAccess.T_City
{
    internal class T_CityQuery
    {
        internal string GetIdByName(string cityName1, string cityName2, string cityName3)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select city_id from t_city where city1_name = @CityName1 and city2_name = @CityName2 and city3_name = @CityName3";
                var result = conn.ExecuteScalar<string>(sql, new { CityName1 = cityName1, CityName2 = cityName2, CityName3 = cityName3 });
                return result;
            }
        }
    }
}
