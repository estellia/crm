/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 18:04:11
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
    /// 基础用户信息 
    /// </summary>
    [Serializable]
    public class BasicUserInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BasicUserInfo()
        {
        }
        #endregion

        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Conn { get; set; }

    }
}
