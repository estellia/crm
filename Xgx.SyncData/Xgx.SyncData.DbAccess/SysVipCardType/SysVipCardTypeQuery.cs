using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.SysVipCardType
{
    internal class SysVipCardTypeQuery
    {
        /// <summary>
        /// 获取商户最低级别的会员
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <returns></returns>
        internal SysVipCardTypeEntity GetMinVipCardType(string _CustomerID)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select top 1 * from SysVipCardType  where CustomerID=@CustomerID  order by VipCardLevel";
                List<SysVipCardTypeEntity> result = conn.Query<SysVipCardTypeEntity>(sql, new { CustomerID = _CustomerID }).ToList();
                return result != null && result.Count != 0 ? result[0] : null;
            }
        }
        //获取某个会员的会员卡级别
        internal SysVipCardTypeEntity GetVipCardTypeByVipID(string _VipID)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select top 1 a.* from SysVipCardType a 
                                                   inner join vipcard b on a.VipCardTypeID=b.VipCardTypeID
                                                  inner join VipCardVipMapping c on c.VipCardID=b.VipCardID
                                                 where c.vipid=@VIPID  ";
                List<SysVipCardTypeEntity> result = conn.Query<SysVipCardTypeEntity>(sql, new { VIPID = _VipID }).ToList();
                return result != null && result.Count != 0 ? result[0] : null;
            }
        }

    internal SysVipCardTypeEntity GetCardTypeIDByTypeId(int typeid)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select top 1 * from SysVipCardType  where VipCardTypeID=@VipCardTypeID";
                List<SysVipCardTypeEntity> result = conn.Query<SysVipCardTypeEntity>(sql, new { VipCardTypeID = typeid }).ToList();
                return result != null && result.Count != 0 ? result[0] : null;
            }
        }

    internal SysVipCardTypeEntity GetCardTypeIDByVipCardLevel(int _VipCardLevel,string _CustomerId)
        {
            using (var conn = new SqlConnection(ConnectionString.XgxSelect))
            {
                conn.Open();
                var sql = @"select top 1 * from SysVipCardType  where VipCardLevel=@VipCardLevel and CustomerId=@CustomerId";
                List<SysVipCardTypeEntity> result = conn.Query<SysVipCardTypeEntity>(sql, new { VipCardLevel = _VipCardLevel, CustomerId = _CustomerId}).ToList();
                return result != null && result.Count != 0 ? result[0] : null;
            }
        }

    }
}
