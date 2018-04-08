using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract.Contract
{
    public class OrderPaymentDetailsContract: IZmindToXgx
    {
        /// <summary>
        /// 增删改标志
        /// </summary>
        public OptEnum Operation { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public EnumOrderPaymentType PaymentType { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string PayTime { get; set; }
        
    }
}
