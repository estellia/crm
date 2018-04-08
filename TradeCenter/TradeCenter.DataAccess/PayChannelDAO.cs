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
    /// 数据访问： 支付通道支付通道+订单信息 组成了支付所必须的所有信息。如果应用下各个客户的收款账户不一样,则应用下的各个客户的支付通道是不同的。 
    /// 表PayChannel的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PayChannelDAO : BaseTradeCenerDAO, ICRUDable<PayChannelEntity>, IQueryable<PayChannelEntity>
    {
        public int GetMaxChannelId()
        {
            var sql = "select max(channelId) from paychannel";

            var result = this.SQLHelper.ExecuteScalar(sql);

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(result);
            }
        }

    }
}
