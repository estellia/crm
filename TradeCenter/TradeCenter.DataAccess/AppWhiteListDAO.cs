/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/13 14:47:38
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
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.DataAccess.Base;

namespace JIT.TradeCenter.DataAccess
{

    /// <summary>
    /// 数据访问： 访问白名单 
    /// 表AppWhiteList的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AppWhiteListDAO : BaseTradeCenerDAO, ICRUDable<AppWhiteListEntity>, IQueryable<AppWhiteListEntity>
    {
        /// <summary>
        /// 根据IP地址查找白名单
        /// </summary>
        /// <param name="pIpAddress"></param>
        /// <returns></returns>
        public AppWhiteListEntity[] GetByIP(string pIpAddress, string pAppID)
        {
            List<AppWhiteListEntity> list = new List<AppWhiteListEntity>();
            string sql = string.Format("select * from appwhitelist where isdelete=0 and ipaddress='{0}' and AppID='{1}'", pIpAddress, pAppID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    AppWhiteListEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
