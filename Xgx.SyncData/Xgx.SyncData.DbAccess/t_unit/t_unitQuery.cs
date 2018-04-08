using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Xgx.SyncData.DbAccess.t_unit
{
    internal class t_unitQuery
    {
        internal dynamic GetUnitById(string id)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select a.unit_code, a.unit_name, b.type_code, c.src_unit_id, a.unit_name_en, a.unit_name_short, d.city1_name, d.city2_name, d.city3_name, a.unit_address, a.unit_contact, a.unit_tel, a.unit_fax, a.unit_email, a.unit_postcode, a.unit_remark, a.create_time, a.modify_time, a.StoreType  from t_unit a
                        left join t_type b on a.type_id = b.type_id
                        left join t_unit_relation c on a.unit_id = c.dst_unit_id
                        left join t_city d on a.unit_city_id = d.city_id
                        where a.unit_id = @UnitId";
                return conn.QueryFirstOrDefault(sql, new {UnitId = id});
            }
        }
    }
}
