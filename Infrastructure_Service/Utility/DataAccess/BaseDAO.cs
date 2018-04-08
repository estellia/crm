/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 17:52:39
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
using System.Text;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 数据访问的基类
    /// <remarks>
    /// <para>1.所有的数据访问类必须直接或间接的继承自该基类</para>
    /// <para>2.该基类会以日志的形式记录所执行的SQL命令</para>
    /// </remarks>
    /// </summary>
    public abstract class BaseDAO<T> where T : BasicUserInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseDAO(T pUserInfo, IConnectionStringManager pConnectionStringManager)
        {
            this.CurrentUserInfo = pUserInfo;
            //创建SQL Helper
            string connectionString = pConnectionStringManager.GetConnectionStringBy(pUserInfo);
            var sqlHelper = new DefaultSQLHelper(connectionString);
            sqlHelper.CurrentUserInfo = pUserInfo;
            this.SQLHelper = sqlHelper;
        }
        #endregion

        /// <summary>
        /// 数据访问助手
        /// </summary>
        protected ISQLHelper SQLHelper { get; set; }

        /// <summary>
        /// 当前的用户信息
        /// </summary>
        protected T CurrentUserInfo { get; private set; }

        public DateTime GetSqlServerTime()
        {
            string sql = "select getdate()";
            var timestr = Convert.ToDateTime(this.SQLHelper.ExecuteScalar(sql));
            return timestr;
        }
    }
}
