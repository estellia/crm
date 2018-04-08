/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/8 16:02:54
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
using System.Configuration;
using System.Text;

using JIT.Utility;
using JIT.Utility.DataAccess;

namespace JIT.TradeCenter.DataAccess.Base
{
    /// <summary>
    /// BaseTradeCenerDAO 
    /// </summary>
    public abstract class BaseTradeCenerDAO:BaseDAO<BasicUserInfo>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseTradeCenerDAO(BasicUserInfo pUserInfo)
            :base(pUserInfo,new DirectConnectionStringManager(ConfigurationManager.AppSettings["Conn"]))
        {
        }
        #endregion
    }
}
