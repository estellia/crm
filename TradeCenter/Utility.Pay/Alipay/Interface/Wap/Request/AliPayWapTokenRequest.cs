using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.ExtensionMethod;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Request
{
    public class AliPayWapTokenRequest : BaseRequest
    {
        public AliPayWapTokenRequest(AliPayChannel pChannel)
            : base()
        {
            this.SecID = "0001";
            this.Format = "xml";
            this.Version = "2.0";
            Channel = pChannel;
        }

        public AliPayChannel Channel { get; set; }

        /// <summary>
        /// 用于关联请求与响应，防止请求重播。支付宝限制来自同一个partner 的请求号必须唯一
        /// </summary>
        public string ReqID
        {
            get { return this.GetPara("req_id"); }
            set { this.Paras["req_id"] = value; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public string SecID
        {
            get { return this.GetPara("sec_id"); }
            private set { this.Paras["sec_id"] = value; }
        }

        /// <summary>
        /// 请求参数格式
        /// </summary>
        private string Format
        {
            get { return this.GetPara("format"); }
            set { this.Paras["format"] = value; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        private string Version
        {
            get { return this.GetPara("v"); }
            set { this.Paras["v"] = value; }
        }

        /// <summary>
        /// 请求的参数由子类实现
        /// </summary>
        private string ReqData
        {
            get { return this.GetPara("req_data"); }
            set { this.Paras["req_data"] = value; }
        }

        /// <summary>
        /// 用户购买的商品名称。不可空
        /// </summary>
        public string Subject
        {
            get { return this.GetDataPara("subject"); }
            set { this.DataParas["subject"] = value; }
        }

        /// <summary>
        /// 支付宝合作商户网站唯一订单号。不可空
        /// </summary>
        public string OutTradeNo
        {
            get { return this.GetDataPara("out_trade_no"); }
            set { this.DataParas["out_trade_no"] = value; }
        }

        /// <summary>
        /// 该笔订单的资金总额，单位为 RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。不可空
        /// </summary>
        public string TotalFee
        {
            get { return this.GetDataPara("total_fee"); }
            set { this.DataParas["total_fee"] = value; }
        }

        /// <summary>
        /// 卖家的支付宝账号。交易成功后，买家资金会转移到该账户中。不可空
        /// </summary>
        public string SellerAccountName
        {
            get { return this.GetDataPara("seller_account_name"); }
            set { this.DataParas["seller_account_name"] = value; }
        }

        /// <summary>
        /// 支付成功后的跳转页面链接。支付成功才会跳转。不可空
        /// </summary>
        public string CallBackUrl
        {
            get { return this.GetDataPara("call_back_url"); }
            set { this.DataParas["call_back_url"] = value; }
        }

        /// <summary>
        /// 支付宝服务器主动通知商户网站里指定的页面 http路径。可空
        /// </summary>
        public string NotifyUrl
        {
            get
            {
                return this.GetDataPara("notify_url");
            }
            set { this.DataParas["notify_url"] = value; }
        }

        /// <summary>
        /// 买家在商户系统的唯一标识。
        ///当该买家支付成功一次后，再次支付金额在 30 元内时，不需要再次输入密码。可空
        /// </summary>
        public string OutUser
        {
            get
            {
                return this.GetDataPara("out_user");
            }
            set { this.DataParas["out_user"] = value; }
        }

        /// <summary>
        /// 收银台页面上，商品展示的超链接。可空
        /// </summary>
        public string MerchantUrl
        {
            get
            {
                return this.GetDataPara("merchant_url");
            }
            set { this.DataParas["merchant_url"] = value; }
        }

        /// <summary>
        /// 交易自动关闭时间，单位为分钟。默认值 21600（即 15 天）。可空
        /// </summary>
        public string PayExpire
        {
            get
            {
                return this.GetDataPara("pay_expire");
            }
            set { this.DataParas["pay_expire"] = value; }
        }

        /// <summary>
        /// 基类抽象方法的实现，获取请求的业务参数
        /// </summary>
        /// <returns></returns>
        protected override void SetReqData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<direct_trade_create_req>{0}</direct_trade_create_req>", this.DataParas.GetXMLString());
            this.ReqData = sb.ToString();
        }

        /// <summary>
        /// 基类抽象方法的实现，是否是一个有效的请求
        /// </summary>
        /// <returns></returns>
        protected override bool IsValid()
        {
            return base.BaseValid() && !string.IsNullOrEmpty(ReqID) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(OutTradeNo)
                && !string.IsNullOrEmpty(TotalFee) && !string.IsNullOrEmpty(SellerAccountName) && !string.IsNullOrEmpty(CallBackUrl);
        }

        /// <summary>
        /// 获取Sign
        /// </summary>
        protected override void SetSign()
        {
            var sortTempdic = new SortedDictionary<string, string>(Paras);
            var tempdic = AliPayFunction.FilterPara(sortTempdic);
            var prestr = AliPayFunction.CreateLinkString(tempdic);
            switch (SecID)
            {
                case "MD5":
                    Sign = AlipayMD5.Sign(prestr, Channel.MD5Key, InputCharset);
                    break;
                case "RSA":
                    Sign = RSAFromPkcs8.Sign(prestr, Channel.RSA_PrivateKey, InputCharset);
                    break;
                case "0001":
                    Sign = RSAFromPkcs8.Sign(prestr, Channel.RSA_PrivateKey, InputCharset);
                    break;
                default:
                    throw new Exception("未知的加密方式：" + SecID);
            }
        }

        /// <summary>
        /// 设置接口名称
        /// </summary>
        protected override void SetService()
        {
            this.Service = "alipay.wap.trade.create.direct";
        }
    }
}
