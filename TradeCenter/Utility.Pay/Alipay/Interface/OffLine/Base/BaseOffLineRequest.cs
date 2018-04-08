using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.Base
{
    public abstract class BaseOfflineRequest : BaseRequest
    {
        #region 构造函数
        public BaseOfflineRequest()
            : base()
        {
            this.SignType = "MD5";
            this.Currency = "156";
        }

        public BaseOfflineRequest(AliPayChannel pChannel)
            : this()
        {
            Channel = pChannel;
        }
        #endregion

        #region 通道Channel
        private AliPayChannel _channel;
        public AliPayChannel Channel
        {
            get { return this._channel; }
            set
            {
                _channel = value;
                ConfigByChannel(value);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// DSA、RSA、MD5 三个值可选，必须大写。不可空
        /// </summary>
        public string SignType
        {
            get { return this.GetPara("sign_type"); }
            private set { this.SetPara("sign_type", value); }
        }
        /// <summary>
        /// 支付宝服务器主动通知商户网站里指定的页面http路径。可空
        /// </summary>
        public string NotifyUrl
        {
            get { return this.GetPara("notify_url"); }
            set { this.SetPara("notify_url", value); }
        }
        /// <summary>
        /// 签名类型。可空
        /// 1：证书签名
        /// 2：其他密钥签名
        /// 如果为空，默认为 2
        /// </summary>
        public string AlipayCaRequest
        {
            get { return this.GetPara("alipay_ca_request"); }
            set { this.SetPara("alipay_ca_request", value); }
        }
        /// <summary>
        /// 支付宝合作商户网站唯一订单号。不可空
        /// </summary>
        public string OutTradeNo
        {
            get { return this.GetPara("out_trade_no"); }
            set { this.SetPara("out_trade_no", value); }
        }
        /// <summary>
        /// 商品的标题/交易标题/订单标题/订单关键字等。该参数最长为 128 个汉字。不可空
        /// </summary>
        public string Subject
        {
            get { return this.GetPara("subject"); }
            set { this.SetPara("subject", value); }
        }

        /// <summary>
        /// 该笔订单的资金总额，取值范围[0.01,100000000]，精确到小数点后 2 位。不可空
        /// </summary>
        public string TotalFee
        {
            get { return this.GetPara("total_fee"); }
            set { this.SetPara("total_fee", value); }
        }
        /// <summary>
        /// 卖家支付宝账号对应的支付宝唯一用户号。可空
        ///<para>以2088开头的纯16位数字。</para>
        ///<para>如果和 seller_email 同时为空，则本参数默认填充partner 的值。</para>
        /// </summary>
        private string SellerID
        {
            get { return this.GetPara("seller_id"); }
            set { this.SetPara("seller_id", value); }
        }
        /// <summary>
        /// 卖家支付宝账号，可以为email 或者手机号。可空
        ///<para>如果 seller_id 不为空，则以seller_id 的值作为卖家账号，忽略本参数。</para>
        /// </summary>
        private string SellerEmail
        {
            get { return this.GetPara("seller_email"); }
            set { this.SetPara("seller_email", value); }
        }
        /// <summary>
        /// 买家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。可空
        /// </summary>
        public string BuyerID
        {
            get { return this.GetPara("buyer_id"); }
            set { this.SetPara("buyer_id", value); }
        }
        /// <summary>
        /// 买家支付宝账号，可以为email 或者手机号。可空
        /// </summary>
        public string BuyerEmail
        {
            get { return this.GetPara("buyer_email"); }
            set { this.SetPara("buyer_email", value); }
        }
        /// <summary>
        /// 操作员的类型：可空
        ///<para> 0：支付宝操作员</para>
        ///<para> 1：商户的操作员</para>
        ///如果传入其它值或者为空，则默认设置为 1。
        /// </summary>
        public string OperatorType
        {
            get { return this.GetPara("operator_type"); }
            set { this.SetPara("operator_type", value); }
        }
        /// <summary>
        /// 卖家的操作员 ID。可空
        /// </summary>
        public string OperatorID
        {
            get { return this.GetPara("operator_id"); }
            set { this.SetPara("operator_id", value); }
        }
        /// <summary>
        /// 对一笔交易的具体描述信息。可空
        /// <para>如果是多种商品，请将商品描述字符串累加传给body。</para>
        /// </summary>
        public string Body
        {
            get { return this.GetPara("body"); }
            set { this.SetPara("body", value); }
        }
        /// <summary>
        /// 收银台页面上，商品展示的超链接。可空
        /// </summary>
        public string ShowUrl
        {
            get { return this.GetPara("show_url"); }
            set { this.SetPara("show_url", value); }
        }
        /// <summary>
        /// 订单金额币种。目前只支持传入 156（人民币）。可空
        /// 如果为空，则默认设置为156。
        /// </summary>
        public string Currency
        {
            get { return this.GetPara("currency"); }
            set { this.SetPara("currency", value); }
        }
        /// <summary>
        /// 订单中商品的单价。可空
        /// <para>如果请求时传入本参数，则必须满足 total_fee=price×quantity 的条件</para>
        /// </summary>
        public string Price
        {
            get { return this.GetPara("price"); }
            set { this.SetPara("price", value); }
        }
        /// <summary>
        /// 订单中商品的数量。可空
        /// <para>如果请求时传入本参数，则必须满足 total_fee=price×quantity 的条件。</para>
        /// </summary>
        public string Quantity
        {
            get { return this.GetPara("quantity"); }
            set { this.SetPara("quantity", value); }
        }
        /// <summary>
        /// 描述商品明细信息，json格式.可空
        /// </summary>
        public string GoodsDetail
        {
            get { return this.GetPara("goods_detail"); }
            set { this.SetPara("goods_detail", value); }
        }
        /// <summary>
        /// 用于商户的特定业务信息的传递.可空
        /// <para>只有商户与支付宝约定了传递此参数且约定了参数含义，此参数才有效。</para>
        /// </summary>
        public string ExtendParams
        {
            get { return this.GetPara("extend_params"); }
            set { this.SetPara("extend_params", value); }
        }
        /// <summary>
        /// 设置未付款交易的超时时间，一旦超时，该笔交易就会自动被关闭。可空
        /// <para>取值范围：1m～15d。</para>
        /// <para>m-分钟，h-小时，d-天，1c-当天（无论交易何时创建，都在 0 点关闭）。</para>
        /// <para>该参数数值不接受小数点，如 1.5h，可转换为 90m。</para>
        /// 该功能需要联系支付宝配置关闭时间。
        /// </summary>
        public string ItBPay
        {
            get { return this.GetPara("it_b_pay"); }
            set { this.SetPara("it_b_pay", value); }
        }
        /// <summary>
        /// 卖家的分账类型，目前只支持传入 ROYALTY（普通分账类型）。可空
        /// </summary>
        public string RoyaltyType
        {
            get { return this.GetPara("royalty_type"); }
            set { this.SetPara("royalty_type", value); }
        }
        /// <summary>
        /// 描述分账明细信息，json格式.可空
        /// </summary>
        public string RoyaltyParameters
        {
            get { return this.GetPara("royalty_parameters"); }
            set { this.SetPara("royalty_parameters", value); }
        }
        /// <summary>
        /// 描述多渠道收单的渠道明细信息，json格式.可空
        /// </summary>
        public string ChannelParameters
        {
            get { return this.GetPara("channel_parameters"); }
            set { this.SetPara("channel_parameters", value); }
        }
        #endregion

        #region 接口方法
        protected override void SetReqData()
        {

        }

        protected override bool IsValid()
        {
            return base.BaseValid() && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(OutTradeNo)
                && !string.IsNullOrEmpty(TotalFee) && !string.IsNullOrEmpty(SignType) && IsSelfValid();
        }

        protected override void SetSign()
        {
            var sortTempdic = new SortedDictionary<string, string>(Paras);
            var tempdic = AliPayFunction.FilterPara(sortTempdic);
            var prestr = AliPayFunction.CreateLinkString(tempdic);
            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = "Sign字符串:" + prestr });
            switch (SignType)
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
                    throw new Exception("未知的加密方式：" + SignType);
            }
            this.SignType = "MD5";
        }

        protected override void SetService()
        {
            SetSelfService();
        }
        #endregion

        #region 抽象方法
        public abstract void SetSelfService();
        public abstract bool IsSelfValid();
        #endregion

        #region 私有方法
        private void ConfigByChannel(AliPayChannel pChannel)
        {
            this.Partner = pChannel.Partner;
            this.SellerID = pChannel.Partner;
            this.SellerEmail = pChannel.SellerEmail;
        }
        #endregion
    }
}
