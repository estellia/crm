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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.TradeCenter.DataAccess;
using JIT.TradeCenter.Entity;

namespace JIT.TradeCenter.BLL
{   
    /// <summary>
    /// 业务处理： 应用详情应用是指将使用交易中心中间件的系统。对于交易中心而言，应用就是客户系统 
    /// </summary>
    public partial class OrderQueryBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private AppDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderQueryBLL(BasicUserInfo pUserInfo)
        {
            this._currentDAO = new AppDAO(pUserInfo);
        }
        #endregion
    }
}