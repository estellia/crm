/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/28 14:50:45
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
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.DataAccess.Base;

namespace JIT.TradeCenter.DataAccess
{
    
    /// <summary>
    /// 数据访问： 各平台的请求和响应记录 
    /// 表PayRequestRecord的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PayRequestRecordDAO : BaseTradeCenerDAO, ICRUDable<PayRequestRecordEntity>, IQueryable<PayRequestRecordEntity>
    {
        
    }
}
