/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/13 13:25:16
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

namespace JIT.Utility
{
    /// <summary>
    /// 系统运行信息
    /// <remarks>
    /// <para>1.线程安全</para>
    /// </remarks>
    /// </summary>
    public class SystemRuntimeInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        private SystemRuntimeInfo()
        {
        }
        #endregion

        #region 单例模式
        /// <summary>
        /// 实例
        /// </summary>
        private static SystemRuntimeInfo INSTANCE = null;

        /// <summary>
        /// 锁
        /// </summary>
        private static object LOCKER = new object();

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static SystemRuntimeInfo GetInstace()
        {
            if (INSTANCE == null)
            {
                lock (LOCKER)
                {
                    if (INSTANCE == null)
                    {
                        INSTANCE = new SystemRuntimeInfo();
                    }
                }
            }
            return INSTANCE;
        }
        #endregion

        #region 并发锁
        /// <summary>
        /// 并发锁
        /// </summary>
        private object _locker = new object();
        #endregion

        #region 执行的T-SQL
        /// <summary>
        /// 执行的T-SQL语句
        /// </summary>
        private Dictionary<string, FixedSizeQueue<TSQL>> _executedTSQL = new Dictionary<string, FixedSizeQueue<TSQL>>();

        /// <summary>
        /// 根据客户ID+用户ID获得唯一的键值
        /// </summary>
        /// <param name="pClientID"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        private string getUniqueKey(string pClientID, string pUserID)
        {
            return string.Format("{0}_%%_{1}",pClientID,pUserID);
        }

        /// <summary>
        /// 将T-SQL语句压入相应的队列
        /// </summary>
        /// <param name="pClientID">客户ID</param>
        /// <param name="pUserID">用户ID</param>
        /// <param name="pTSQL">T-SQL</param>
        public void EnqueueTSQL(string pClientID, string pUserID, TSQL pTSQL)
        {
            string key = this.getUniqueKey(pClientID, pUserID);
            //判断是否有该客户+用户的T-SQL运行记录,没有则创建
            FixedSizeQueue<TSQL> queue = null;
            if (!this._executedTSQL.ContainsKey(key))
            {
                lock (_locker)
                {
                    if (!this._executedTSQL.ContainsKey(key))
                    {
                        queue = new FixedSizeQueue<TSQL>(30);
                        this._executedTSQL.Add(key, queue);
                    }
                    else
                    {
                        queue = this._executedTSQL[key];
                    }
                }
            }
            else
            {
                queue = this._executedTSQL[key];
            }
            //将当前的T-SQL压入队列
            queue.Enqueue(pTSQL);
        }

        /// <summary>
        /// 根据客户+用户获得所执行最后的一批的T-SQL
        /// </summary>
        /// <param name="pClientID"></param>
        /// <param name="pUserID"></param>
        public TSQL[] GetTSQLBy(string pClientID, string pUserID)
        {
            var key = this.getUniqueKey(pClientID, pUserID);
            if (this._executedTSQL.ContainsKey(key))
            {
                return this._executedTSQL[key].ToArray();
            }
            else
                return null;
        }
        #endregion
    }
}
