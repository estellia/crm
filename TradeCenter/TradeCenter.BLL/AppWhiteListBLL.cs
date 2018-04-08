/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/13 14:47:38
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
using System.Text.RegularExpressions;

namespace JIT.TradeCenter.BLL
{
    /// <summary>
    /// 业务处理： 访问白名单 
    /// </summary>
    public partial class AppWhiteListBLL
    {
        /// <summary>
        /// 判断是否是合法的应用
        /// </summary>
        /// <param name="pIpaddress">IP地址</param>
        /// <param name="pAppID">应用ID</param>
        /// <param name="pClientID">客户ID</param>
        /// <returns></returns>
        public bool IsValidApp(string pIpaddress, string pAppID, string pClientID)
        {
            var entitys = this._currentDAO.GetByIP(pIpaddress, pAppID);
            if (entitys.Length > 0)
            {
                foreach (var item in entitys)
                {
                    var regex = new Regex(item.Regex);
                    if (regex.IsMatch(pClientID))
                        return true;
                }
                return false;
            }
            else
                return false;
        }
    }
}