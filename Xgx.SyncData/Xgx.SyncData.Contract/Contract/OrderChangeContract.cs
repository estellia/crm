using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    public class OrderChangeContract : IXgxToZmind
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 变更操作
        /// </summary>
        public EnumOrderChange ChangeOperation { get; set; }

        /// <summary>
        /// 变更数据
        /// </summary>
        public string ChangeValue { get; set; }
    }
}
