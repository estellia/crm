using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class CreateOrderParameters
    {
        /// <summary>
        /// 对应数据库中的Channel,1-AliPayWap支付,2-Union语音支付,3-UnionWap支付,4-AliPayOffline支付
        /// </summary>
        public int? PayChannelID { get; set; }
        /// <summary>
        /// 客户端提交的订单ID
        /// </summary>
        public string AppOrderID { get; set; }
        /// <summary>
        /// 提交订单时间
        /// </summary>
        public string AppOrderTime { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public int? AppOrderAmount { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string AppOrderDesc { get; set; }
        /// <summary>
        /// 货币种类,当前只支持1-人民币
        /// </summary>
        public int? Currency { get; set; }
        /// <summary>
        /// 用于语音支付的电话号码
        /// </summary>
        public string MobileNO { get; set; }

        /// <summary>
        /// 支付成功跳转URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 动态ID,用于声波,条码,二维码面支付(非预订单)
        /// </summary>
        public string DynamicID { get; set; }

        /// <summary>
        /// 动态ID类型
        /// <remarks>
        /// <para>声波:soundwave</para>
        /// <para>二维码:qrcode</para>
        /// <para>条码:barcode</para>
        /// </remarks>
        /// </summary>
        public string DynamicIDType { get; set; }

        /// <summary>
        /// 微信用户id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 客户端ip
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 支付模式 0:支付宝扫码支付; 1:微信扫码支付; 
        /// </summary>
        public int? PaymentMode { get; set; }

        /// <summary>
        /// 附加参数
        /// </summary>
        public Dictionary<string, object> Paras { get; set; }

        public DateTime GetDateTime()
        {
            if (string.IsNullOrEmpty(this.AppOrderTime))
                throw new Exception("AppOrderTime不能为空");
            return DateTime.ParseExact(this.AppOrderTime, "yyyy-MM-dd HH:mm:ss fff", null);
        }
    }
}
