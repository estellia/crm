/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
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
using JIT.TradeCenter.DataAccess.Base;
using JIT.TradeCenter.Entity;

namespace JIT.TradeCenter.DataAccess
{

    /// <summary>
    /// 数据访问： 应用订单表 
    /// 表AppOrder的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AppOrderDAO : BaseTradeCenerDAO, ICRUDable<AppOrderEntity>, IQueryable<AppOrderEntity>
    {
        public SqlTransaction CreateTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        public void DeleteByAppInfo(string pAppClientID, string pAppOrderID, int pAppID, SqlTransaction pTran)
        {
            string sql = string.Format("update AppOrder set isdelete=1,LastUpdateTime=getdate() where AppClientID='{0}' and AppOrderID='{1}' and AppID={2} and isdelete=0",
                pAppClientID, pAppOrderID, pAppID);
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(pTran, CommandType.Text, sql);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
            }
        }

        public AppOrderEntity[] GetNotNodify()
        {
            List<AppOrderEntity> list = new List<AppOrderEntity> { };
            string sql = "select * from apporder where status=2 and isnull(IsNotified,0)=0 and isdelete=0 and isnull(NotifyCount,0)<7 and (DATEDIFF(MI,NextNotifyTime,GETDATE())<1 OR (NextNotifyTime IS NULL AND DATEDIFF(MI,CREATETIME,GETDATE())>10))";
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    AppOrderEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取当天未支付成功的订单
        /// </summary>
        /// <returns></returns>
        public AppOrderEntity[] GetUnpaidOrder()
        {
            List<AppOrderEntity> list = new List<AppOrderEntity> { };
            string sql = "select * from apporder where STATUS IN (0,1) AND CONVERT(VARCHAR(10),AppOrderTime,120)=CONVERT(VARCHAR(10),GETDATE(),120) AND isdelete=0";
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    AppOrderEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        #region 通过AppOrderId查询交易中心订单
        /// <summary>
        /// 通过AppOrderId查询交易中心订单
        /// </summary>
        /// <param name="appOrderId"></param>
        /// <returns></returns>
        public AppOrderEntity GetAppOrderByAppOrderId(string appOrderId)
        {
            AppOrderEntity m = null;
            string sql = @" SELECT top 1 *
                            FROM    AppOrder
                            WHERE   IsDelete = 0
                                    AND AppOrderID = @AppOrderID; ";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@AppOrderID", appOrderId));
            using (var rd = this.SQLHelper.ExecuteReader(CommandType.Text, sql, ls.ToArray()))
            {
                while (rd.Read())
                {
                    this.Load(rd, out m);
                }
            }
            return m;
        }
        #endregion
    }
}
