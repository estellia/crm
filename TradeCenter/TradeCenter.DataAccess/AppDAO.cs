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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.TradeCenter.DataAccess.Base;
using JIT.TradeCenter.Entity;

namespace JIT.TradeCenter.DataAccess
{
    
    /// <summary>
    /// 数据访问： 应用详情应用是指将使用交易中心中间件的系统。对于交易中心而言，应用就是客户系统 
    /// 表App的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AppDAO : BaseTradeCenerDAO, ICRUDable<AppEntity>, IQueryable<AppEntity>
    {
        
    }
}
