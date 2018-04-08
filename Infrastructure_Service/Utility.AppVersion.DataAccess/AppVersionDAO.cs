/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
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
using JIT.Utility.AppVersion.DataAccess.Base;
using JIT.Utility.AppVersion.Entity;

namespace JIT.Utility.AppVersion.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表AppVersion的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AppVersionDAO : CommonDAO, ICRUDable<AppVersionEntity>, IQueryable<AppVersionEntity>
    {
        public AppVersionEntity[] GetLatestVersion(int pBusinessZoneID, int pAppID)
        {
            List<AppVersionEntity> list = new List<AppVersionEntity> { };
            string sql = string.Format("select top 1 * from AppVersion where isdelete=0 and BusinessZoneID={0} and appid={1} order by version desc", pBusinessZoneID, pAppID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    AppVersionEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
                return list.ToArray();
            }
        }

        public AppVersionEntity[] GetVersion(string pClientID, string pAppCode)
        {
            List<AppVersionEntity> list = new List<AppVersionEntity> { };
            StringBuilder sub = new StringBuilder();
            if (!string.IsNullOrEmpty(pClientID))
                sub.AppendFormat(" and clientid='{0}'", pClientID);
            if (!string.IsNullOrEmpty(pAppCode))
                sub.AppendFormat(" and AppCode='{0}'", pAppCode);
            string sql = string.Format("select * from AppVersion where isdelete=0 {0} order by version desc", sub);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    AppVersionEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
                return list.ToArray();
            }
        }
    }
}
