using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.PALifePay.ValueObject
{
    public class LifePayRP
    {
        /// <summary>
        /// 用户唯一ID
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 商户号ID
        /// </summary>
        public string merchantCode { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public string merOrderNo { get; set; }
        /// <summary>
        /// 通知类型   00 成功 01 失败 02 其他
        /// </summary>
        public string notifyType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [IgnoreSignature]
        [SignatureField]
        public string securitySign { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 交易完成时间
        /// </summary>
        public string transactionTime { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string orderTraceNo { get; set; }
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string transactionBizNo { get; set; }
        /// <summary>
        /// 平安金管家流水号
        /// </summary>
        public string serialNo { get; set; }
    }
}
