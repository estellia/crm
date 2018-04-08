using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class CreateWXNativePayUrlReqPara
    {
        /// <summary>
        /// 支付通道ID
        /// </summary>
        public int? PayChannelID { get; set; }
        /// <summary>
        /// 商品ID或者订单ID
        /// </summary>
        public string ProductID { get; set; }

    }
}
