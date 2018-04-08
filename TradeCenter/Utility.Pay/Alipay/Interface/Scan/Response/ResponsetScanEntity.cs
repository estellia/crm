using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Scan
{
    public class ResponsetScanEntity
    {
        public tradeResponse alipay_trade_precreate_response { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }

    public class tradeResponse
    {
        /// <summary>
        /// CODE 10000 处理成功
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 接口返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 商户的订单号 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 当前预下单请求生成的二维码码串，可以用二维码生成工具根据该码串值生成对应的二维码 
        /// </summary>
        public string qr_code { get; set; }
    }
}
