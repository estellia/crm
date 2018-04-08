/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/11 14:14:55
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
using System.Data;

namespace JIT.Utility
{
    /// <summary>
    /// 租户用户的基础信息 
    /// </summary>
    public class BasicTenantUserInfo : BasicUserInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BasicTenantUserInfo()
        {
        }
        #endregion

        /// <summary>
        /// 租户数据库的数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}