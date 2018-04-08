using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    public enum EnumOrderChange
    {
        /// <summary>
        /// 审核通过
        /// </summary>
        Approval,

        /// <summary>
        /// 审核不通过
        /// </summary>
        NoApproval,

        /// <summary>
        /// 备货完成
        /// </summary>
        StockDone,

        /// <summary>
        /// 订单退回总部
        /// </summary>
        OrderUnit,

        /// <summary>
        /// 订单修改服务时间，提货时间
        /// </summary>
        OrderChangeTime
    }
}
