/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/10 9:56:47
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
using JIT.Utility.MSTRIntegration.Base;
using JIT.Utility.MSTRIntegration.Entity;

namespace JIT.Utility.MSTRIntegration.DataAccess
{
    
    /// <summary>
    /// 数据访问： 租户平台的用户登录网站后,插入一条这样的记录.MSTR的认证模块根据记录中的信息来创建/重新加载 MSTR的Session. 
    /// 表MSTRIntegrationUserSession的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MSTRIntegrationUserSessionDAO : BaseReportDAO, ICRUDable<MSTRIntegrationUserSessionEntity>, IQueryable<MSTRIntegrationUserSessionEntity>
    {
        
    }
}
