using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract.Dependency
{
    public enum EnumOrderStatus
    {
        /// <summary>
        /// 300待付款
        /// </summary>
        PendingPay = 300,

        /// <summary>
        /// 100待审核
        /// </summary>
        Pending = 100,

        /// <summary>
        /// 520待服务
        /// </summary>
        PendingService = 520,

        /// <summary>
        /// 410备货中
        /// </summary>
        Stocking = 410,

        /// <summary>
        /// 510待提货
        /// </summary>
        PendingDelivery = 510,

        /// <summary>
        /// 500待发货	
        /// </summary>
        PendingShip = 500,

        /// <summary>
        /// 620已服务	
        /// </summary>
        Serviced = 620,

        /// <summary>
        /// 610已提货
        /// </summary>
        Picked = 610,

        /// <summary>
        /// 600已发货
        /// </summary>
        Shipped = 600,

        /// <summary>
        /// 700交易完成
        /// </summary>
        Done = 700,

        /// <summary>
        /// 310退款中
        /// </summary>
        Refunding = 310,

        /// <summary>
        /// 320退款完成
        /// </summary>
        RefundDone = 320,

        /// <summary>
        /// 800已关闭
        /// </summary>
        Close = 800
    }
}
