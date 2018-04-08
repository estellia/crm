/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/9 13:57:27
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
using JIT.Utility.MobileDeviceManagement.Base;
using JIT.Utility.MobileDeviceManagement.Entity;

namespace JIT.Utility.MobileDeviceManagement.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表MobileCommandRecord的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MobileCommandRecordDAO : BaseMobileDeviceManagementDAO, ICRUDable<MobileCommandRecordEntity>, IQueryable<MobileCommandRecordEntity>
    {
        public DataTable GetTable(string sql)
        {
            var data = this.SQLHelper.ExecuteDataset(sql);
            if (data.Tables.Count > 0)
                return data.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 获取命令
        /// </summary>
        /// <param name="pClientID"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public MobileCommandRecordEntity[] GetCommand(string pClientID, string pUserID)
        {
            string sql = @"select * from MobileCommandRecord where clientid='{0}'
                        and userid='{1}' and isdelete=0 and status in(0 ,2) and requestcount<3";
            sql = string.Format(sql, pClientID, pUserID);
            List<MobileCommandRecordEntity> list = new List<MobileCommandRecordEntity> { };
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileCommandRecordEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        private int UpdateIsdelete(SqlTransaction pTran, MobileCommandRecordEntity pEntity)
        {
            string sql = string.Format("update mobilecommandrecord set requestcount=requestcount+1 where recordid='{0}'", pEntity.RecordID);
            return this.SQLHelper.ExecuteNonQuery(pTran, CommandType.Text, sql);
        }

        /// <summary>
        /// 批量更新数据库IsDelete状态为1
        /// </summary>
        /// <param name="pTran"></param>
        /// <param name="pEntities"></param>
        /// <returns></returns>
        public int UpdateIsDelete(SqlTransaction pTran, MobileCommandRecordEntity[] pEntities)
        {
            int count = 0;
            using (pTran.Connection)
            {
                try
                {
                    foreach (var item in pEntities)
                    {
                        count += UpdateIsdelete(pTran, item);
                    }
                    pTran.Commit();
                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    return 0;
                }
            }
            return count;
        }

    }
}
