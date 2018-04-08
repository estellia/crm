using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.Entity
{
    /// <summary>
    /// 渠道明细信息
    /// </summary>
    public class ChannelDetail
    {
        /// <summary>
        /// 付款方设备编号。
        /// </summary>
        public string equipment_no { get; set; }
        /// <summary>
        /// 付款方终端 ID。
        /// </summary>
        public string termId { get; set; }
        /// <summary>
        /// 付款方终端类型。如果要传递该参数，商户必须事先和支付宝约定传什么值。
        /// </summary>
        public string termType { get; set; }
        /// <summary>
        /// 付款方终端操作系统类型。如果要传递该参数，商户必须事先和支付宝约定传什么值。
        /// </summary>
        public string termOsType { get; set; }
        /// <summary>
        /// 付款方终端客户端版本。
        /// </summary>
        public string clientVersion { get; set; }
        /// <summary>
        /// 收款方设备编号。
        /// </summary>
        public string payeeEquipmentNo { get; set; }
        /// <summary>
        /// 收款方终端 ID。
        /// </summary>
        public string payeeTermId { get; set; }
        /// <summary>
        /// 收款方终端类型。如果要传递该参数，商户必须事先和支付宝约定传什么值
        /// </summary>
        public string payeeTermType { get; set; }
        /// <summary>
        /// 收款方终端操作系统类型。如果要传递该参数，商户必须事先和支付宝约定传什么值。
        /// </summary>
        public string payeeTermOsType { get; set; }
        /// <summary>
        /// 收款方客户端版本。
        /// </summary>
        public string payeeClientVersion { get; set; }
    }
}
