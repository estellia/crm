using JIT.Utility.Pay.Alipay.Channel;
using JIT.Utility.Pay.Alipay.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Scan
{
    public class RequestScanEntity : ScanBaseRequest
    {
        public RequestScanEntity(AliPayChannel channel)
        {
            this.app_id = channel.SCAN_AppID;
            this.method = "alipay.trade.precreate";
            this.charset = "utf-8";
            this.sign_type = "RSA";
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.version = "1.0";
            this.channel = channel;

        }

        private AliPayChannel channel;

        /// <summary>
        /// 请求参数
        /// </summary>
        public string biz_content
        {
            get { return this.GetPara("biz_content"); }
            set { this.Paras["biz_content"] = value; }
        }
    }

    public class RequstScanDetail
    {
        #region 扫码支付请求参数
        /// <summary>
        /// 商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 卖家支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        public string seller_id { get; set; }
        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string subject { get; set; }
        #endregion
    }
}
