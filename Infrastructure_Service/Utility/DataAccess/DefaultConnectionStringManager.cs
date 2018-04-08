/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 17:26:56
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

using JIT.Const;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 默认的数据库连接字符串管理者 
    /// </summary>
    public class DefaultConnectionStringManager:IConnectionStringManager
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DefaultConnectionStringManager()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 内部的数据库连接字符串
        /// </summary>
        protected List<ConnectionString> InnerList = new List<ConnectionString>();

        /// <summary>
        /// 默认的数据库连接字符串
        /// </summary>
        protected string DefaultConnection = string.Empty;
        #endregion

        #region 添加数据库连接字符串
        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="pConnectionString">数据库连接字符串</param>
        /// <param name="pIsDefault">是否默认的数据库连接字符串</param>
        public void Add(ConnectionString pConnectionString, bool pIsDefault)
        {
            if (pConnectionString != null)
            {
                if (pIsDefault)
                {
                    this.DefaultConnection = pConnectionString.Value;
                }
                else
                {
                    int index = -1;
                    for (int i = 0; i < this.InnerList.Count; i++)
                    {
                        var item = this.InnerList[i];
                        if (item.ClientID == pConnectionString.ClientID)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index >= 0)
                    {
                        this.InnerList[index] = pConnectionString;
                    }
                    else
                    {
                        this.InnerList.Add(pConnectionString);
                    }
                }
            }
        }
        /// <summary>
        /// 添加数据库连接字符串(非默认)
        /// </summary>
        /// <param name="pConnectionString">数据库连接字符串</param>
        public void Add(ConnectionString pConnectionString)
        {
            this.Add(pConnectionString, false);
        }
        /// <summary>
        /// 添加一组数据库连接字符串(非默认)
        /// </summary>
        /// <param name="pConnectionStrings">一组数据库连接字符串</param>
        public void Add(IEnumerable<ConnectionString> pConnectionStrings)
        {
            if (pConnectionStrings != null)
            {
                foreach (var item in pConnectionStrings)
                    this.Add(item);
            }
        }
        #endregion

        #region 清除
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            this.InnerList.Clear();
        }
        #endregion

        #region IConnectionStringManager 成员
        /// <summary>
        /// 根据用户信息获取数据库连接字符串
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <returns></returns>
        public string GetConnectionStringBy(BasicUserInfo pUserInfo)
        {
            if (pUserInfo == null)
                throw new ArgumentNullException("pUserInfo");
            foreach (var item in this.InnerList)
            {
                if (item.ClientID == pUserInfo.ClientID)
                    return item.Value;
            }
            return this.DefaultConnection;
        }
        #endregion
    }
}
