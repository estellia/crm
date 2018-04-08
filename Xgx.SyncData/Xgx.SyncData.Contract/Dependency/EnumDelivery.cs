using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    public enum EnumDelivery
    {
        /// <summary>
        /// 未设置配送方式
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 送货上门
        /// </summary>
        HomeDelivery = 1,
        /// <summary>
        /// 到店自提
        /// </summary>
        ShopPickUp = 2,
        /// <summary>
        /// 到店服务
        /// </summary>
        ShopService = 4
    }
}
