/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:30:35
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Message.WCF.Base;
using JIT.Utility.Message.WCF.Entity;


namespace JIT.Utility.Message.WCF.DataAccess
{

    /// <summary>
    /// 数据访问： 客户人员表 
    /// 表ClientUser的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ClientUserDAO : CommonDAO_QDY, ICRUDable<ClientUserEntity>, IQueryable<ClientUserEntity>
    {
        public ClientUserEntity[] GetValidUser(OrdersCheckingEntity entity)
        {
            List<ClientUserEntity> list = new List<ClientUserEntity> { };
//            string sql = string.Format(@"select d.*
//from OrdersChecking a 
//    inner join Orders b on a.OrdersID=b.OrdersID
//    cross apply fnGetSuperiorIDByUserID(b.CreateBy) c
//    inner join ClientUser d on c.ClientUserID=d.ClientUserID and d.IsDelete=0 and a.ClientPositionID=d.ClientPositionID
//where a.CheckingID={0}", entity.CheckingID);

            string sql = string.Format("select * from clientuser where clientpositionid={0}", entity.ClientPositionID);
            
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    ClientUserEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
