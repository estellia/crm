/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 17:50:35
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

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 数据库连接字符串管理者接口 
    /// </summary>
    public interface IConnectionStringManager
    {
        /// <summary>
        /// 根据用户信息获取数据库连接字符串
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <returns></returns>
        string GetConnectionStringBy(BasicUserInfo pUserInfo);
    }
}
