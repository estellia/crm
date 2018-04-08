/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/13 13:10:12
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
using System.Text;

using JIT.Utility.DataAccess;

namespace JIT.Utility.ETCL2.DataSource
{
    /// <summary>
    /// 以数据库为数据源 
    /// </summary>
    public class DBDataSource:IDataSource
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pSQLHelper">数据访问助手</param>
        public DBDataSource(ISQLHelper pSQLHelper)
        {
            this.CurrentSQLHelper = pSQLHelper;
        }

        /// <summary>
        /// 构造函数
        /// <remarks>
        /// <para>此时数据库为SQLSERVER</para>
        /// </remarks>
        /// </summary>
        /// <param name="pConnectionStringManager">连接字符串管理器</param>
        public DBDataSource(IConnectionStringManager pConnectionStringManager)
        {
            this.ConnectionStringManager = pConnectionStringManager;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 连接字符串管理器
        /// </summary>
        protected IConnectionStringManager ConnectionStringManager { get; set; }

        /// <summary>
        /// 数据访问助手
        /// </summary>
        protected ISQLHelper CurrentSQLHelper { get; set; }

        /// <summary>
        /// SQL语句或SQL语句模板
        /// </summary>
        public List<string> SQLs { get; set; }
        #endregion

        #region 虚方法
        /// <summary>
        /// 获取SQL语句
        /// <remarks>
        /// <para>子类可以重写该方法,实现SQL语句模板替换的逻辑</para>
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        protected string[] GetSQLs()
        {
            if (this.SQLs != null)
                return this.SQLs.ToArray();
            else
                return null;
        }
        #endregion

        #region IDataSource 成员
        /// <summary>
        /// 从数据源中抽取数据
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <returns></returns>
        public DataTable[] RetrieveData(BasicUserInfo pUserInfo)
        {
            if (this.CurrentSQLHelper == null)
            {
                var connectionString = this.ConnectionStringManager.GetConnectionStringBy(pUserInfo);
                this.CurrentSQLHelper = new DefaultSQLHelper(connectionString);
            }
            if (this.CurrentSQLHelper == null)
                throw new ArgumentException("无法获得数据访问助手");
            //
            var sqls = this.GetSQLs();
            if (sqls != null)
            {
                List<DataTable> results = new List<DataTable>();
                //
                foreach (var sql in sqls)
                {
                    var ds = this.CurrentSQLHelper.ExecuteDataset(sql);
                    if (ds != null)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            results.Add(dt);
                        }
                    }
                }
                //
                return results.ToArray();
            }
            else
            {
                return null;
            }

        }

        #endregion
    }
}
