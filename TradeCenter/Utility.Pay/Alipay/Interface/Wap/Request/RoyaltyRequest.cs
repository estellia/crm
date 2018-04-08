using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Util;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Request
{
    public class RoyaltyRequest : BaseRequest
    {
        #region 构造函数
        public RoyaltyRequest()
        {
            SignType = "MD5";
        }
        #endregion

        #region 属性
        #region 基本参数

        /// <summary>
        /// DSA、RSA、MD5 三个值可选，必须大写。
        /// </summary>
        public string SignType
        {
            get
            {
                return this.GetPara("sign_type");
            }
            protected set { this.Paras["sign_type"] = value; }
        }

        /// <summary>
        /// 支付宝服务器主动通知商户网站里指定的页面http路径
        /// </summary>
        public string NotifyUrl
        {
            get
            {
                return this.GetPara("notify_url");
            }
            set { this.Paras["notify_url"] = value; }
        }
        #endregion

        #region 业务参数
        /// <summary>
        /// 商户请求分润的唯一标示。
        /// 长度为小于等于16 位的纯数字或者字符。
        /// 每一个partner下同一笔交易若发起多笔分润请求，其分润号不能重复。重复则视为同一次请求
        /// </summary>
        public string OutBillNo
        {
            get
            {
                return this.GetPara("out_bill_no");
            }
            set { this.Paras["out_bill_no"] = value; }
        }
        /// <summary>
        /// 支付宝分润类型。目前只支持“10”类型。
        /// </summary>
        public string RoyaltyType
        {
            get
            {
                return this.GetPara("royalty_type");
            }
            set { this.Paras["royalty_type"] = value; }
        }
        /// <summary>
        /// 分润参数明细
        /// </summary>
        public string RoyaltyParameters
        {
            get
            {
                return this.GetPara("royalty_parameters");
            }
            set { this.Paras["royalty_parameters"] = value; }
        }
        /// <summary>
        /// 需要进行分润的支付宝交易号。支付交易成功后由支付宝系统返回。
        /// <para>trade_no 和 out_trade_no 至少填写一项。</para>
        /// </summary>
        public string TradeNo
        {
            get
            {
                return this.GetPara("trade_no");
            }
            set { this.Paras["trade_no"] = value; }
        }
        /// <summary>
        /// 需要进行分润的支付宝交易对应的商户订单号。用来标示对一笔唯一订单进行分润
        /// <para>trade_no 和out_trade_no 至少填写一项；</para>
        /// <para>如果一起传，以out_trade_no 为准，同时检查 trade_no 是否匹配。</para>
        /// </summary>
        public string OutTradeNo
        {
            get
            {
                return this.GetPara("out_trade_no");
            }
            set { this.Paras["out_trade_no"] = value; }
        }
        #endregion
        #endregion

        protected override void SetSign()
        {
            var sortTempdic = new SortedDictionary<string, string>(Paras);
            var tempdic = AliPayFunction.FilterPara(sortTempdic);
            var prestr = AliPayFunction.CreateLinkString(tempdic);
            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = "Sign字符串:" + prestr });
            switch (SignType)
            {
                case "MD5":
                    Sign = AlipayMD5.Sign(prestr, AliPayConfig.MD5_Key_Royalty, InputCharset);
                    break;
                case "RSA":
                    Sign = RSAFromPkcs8.Sign(prestr, AliPayConfig.RSA_PrivateKey_Royalty, InputCharset);
                    break;
                case "0001":
                    Sign = RSAFromPkcs8.Sign(prestr, AliPayConfig.RSA_PrivateKey_Royalty, InputCharset);
                    break;
                default:
                    throw new Exception("未知的加密方式：" + SignType);
            }
            this.SignType = "MD5";
        }

        protected override void SetReqData()
        {

        }

        protected override bool IsValid()
        {
            if (!base.BaseValid())
                throw new Exception("基参数错误");
            if (string.IsNullOrEmpty(OutBillNo))
                throw new Exception("分润号为空");
            if (string.IsNullOrEmpty(RoyaltyType))
                throw new Exception("分润类型为空");
            if (string.IsNullOrEmpty(OutTradeNo))
                throw new Exception("交易号为空");
            if (string.IsNullOrEmpty(RoyaltyParameters))
                throw new Exception("金额为空");
            return true;
        }

        protected override void SetService()
        {
            this.Service = "distribute_royalty";
        }
    }
}
