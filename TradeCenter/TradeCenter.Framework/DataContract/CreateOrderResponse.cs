using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class CreateOrderResponse
    {
        /// <summary>
        /// 返回状态码:0-99 成功,100-199 UnionPayWap请求订单失败,200-299 UnionPayIVR请求订单失败,300-399 AliPayWap请求订单失败
        /// </summary>
        [JsonIgnore]
        public int ResultCode { get; set; }

        /// <summary>
        /// 订单ID(交易中心订单号 )
        /// </summary>
        public long? OrderID { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// 二维码地址(Offline支付时)
        /// </summary>
        public string QrCodeUrl { get; set; }

        /// <summary>
        /// 微信Native支付返回给支付平台的包数据
        /// </summary>
        public string WXPackage { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
}
