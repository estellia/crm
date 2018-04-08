using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.WeiXinPay.Interface.Notify
{
    public class WXPayNotify : BaseNotify
    {
        #region 构造函数
        #endregion

        #region 属性
        /// <summary>
        /// //是否必填：否  String(8)   签名类型，取值：MD5、RSA，默认：MD5
        /// </summary>
        public string SignType
        {
            get { return this.GetDataPara("sign_type"); }
            set { this.SetDataPara("sign_type", value); }
        }
        /// <summary>
        /// //是否必填：否  String(8)版本号，默认为 1.0
        /// </summary>
        public string ServiceVersion
        {
            get { return this.GetDataPara("service_version"); }
            set { this.SetDataPara("service_version", value); }
        }
        /// <summary>
        /// //是否必填：否  String(8)字符编码,取值：GBK、UTF-8，默认：GBK。
        /// </summary>
        public string InputCharset
        {
            get { return this.GetDataPara("input_charset"); }
            set { this.SetDataPara("input_charset", value); }
        }
        /// <summary>
        /// //是否必填：是  String(32)签名
        /// </summary>
        public string Sign
        {
            get { return this.GetDataPara("sign"); }
            set { this.SetDataPara("sign", value); }
        }
        /// <summary>
        /// //是否必填：否  Int多密钥支持的密钥序号，默认1
        /// </summary>
        public string SignKeyIndex
        {
            get { return this.GetDataPara("sign_key_index"); }
            set { this.SetDataPara("sign_key_index", value); }
        }
        /// <summary>
        /// //是否必填：是  Int1-即时到账
        /// </summary>
        public string TradeMode
        {
            get { return this.GetDataPara("trade_mode"); }
            set { this.SetDataPara("trade_mode", value); }
        }
        /// <summary>
        /// //是否必填：是  Int支付结果：0—成功其他保留
        /// </summary>
        public string TradeState
        {
            get { return this.GetDataPara("trade_state"); }
            set { this.SetDataPara("trade_state", value); }
        }
        /// <summary>
        /// //是否必填：否  String(64)支付结果信息，支付成功时为空
        /// </summary>
        public string PayInfo
        {
            get { return this.GetDataPara("pay_info"); }
            set { this.SetDataPara("pay_info", value); }
        }
        /// <summary>
        /// //是否必填：是  String(10)商户号，也即之前步骤的partnerid,由微信统一分配的10 位正整数(120XXXXXXX)号
        /// </summary>
        public string Partner
        {
            get { return this.GetDataPara("partner"); }
            set { this.SetDataPara("partner", value); }
        }
        /// <summary>
        /// //是否必填：是  String(16)银行类型，在微信中使用 WX
        /// </summary>
        public string BankType
        {
            get { return this.GetDataPara("bank_type"); }
            set { this.SetDataPara("bank_type", value); }
        }
        /// <summary>
        /// //是否必填：否  String(32)银行订单号
        /// </summary>
        public string BankBillNo
        {
            get { return this.GetDataPara("bank_billno"); }
            set { this.SetDataPara("bank_billno", value); }
        }
        /// <summary>
        /// //是否必填：是  Int支付金额，单位为分，如果discount 有值，通知的 total_fee+ discount = 请求的 total_fee
        /// </summary>
        public string TotalFee
        {
            get { return this.GetDataPara("total_fee"); }
            set { this.SetDataPara("total_fee", value); }
        }
        /// <summary>
        /// //是否必填：是  Int现金支付币种,目前只支持人民币,默认值是 1-人民币
        /// </summary>
        public string FeeType
        {
            get { return this.GetDataPara("fee_type"); }
            set { this.SetDataPara("fee_type", value); }
        }
        /// <summary>
        /// //是否必填：是  String(128)支付结果通知 id，对于某些特定商户，只返回通知 id，要求商户据此查询交易结果
        /// </summary>
        public string NotifyID
        {
            get { return this.GetDataPara("notify_id"); }
            set { this.SetDataPara("notify_id", value); }
        }
        /// <summary>
        /// //是否必填：是  String(28)交易号，28 位长的数值，其中前 10 位为商户号，之后 8 位为 订 单 产 生 的 日 期 ， 如20090415，最后 10 位是流水号。
        /// </summary>
        public string TransactionID
        {
            get { return this.GetDataPara("transaction_id"); }
            set { this.SetDataPara("transaction_id", value); }
        }
        /// <summary>
        /// //是否必填：是  String(32商户系统的订单号，与请求一致。
        /// </summary>
        public string OutTradeNo
        {
            get { return this.GetDataPara("out_trade_no"); }
            set { this.SetDataPara("out_trade_no", value); }
        }
        /// <summary>
        /// //是否必填：否  String(127)商家数据包，原样返回
        /// </summary>
        public string Attach
        {
            get { return this.GetDataPara("attach"); }
            set { this.SetDataPara("attach", value); }
        }
        /// <summary>
        /// //是否必填：是  String(14)支 付 完 成 时 间 ， 格 式 为yyyyMMddhhmmss，如 2009年 12 月 27 日 9 点 10 分 10 秒表示为 20091227091010。时为 GMT+8 beijing。
        /// </summary>
        public string TimeEnd
        {
            get { return this.GetDataPara("time_end"); }
            set { this.SetDataPara("time_end", value); }
        }
        /// <summary>
        /// //是否必填：否  Int物流费用，单位分，默认 0。如 果 有 值 ， 必 须 保 证transport_fee +product_fee =total_fee
        /// </summary>
        public string TransportFee
        {
            get { return this.GetDataPara("transport_fee"); }
            set { this.SetDataPara("transport_fee", value); }
        }
        /// <summary>
        /// //是否必填：否  Int物品费用，单位分。如果有值，必 须 保 证 transport_fee +product_fee=total_fee
        /// </summary>
        public string ProductFee
        {
            get { return this.GetDataPara("product_fee"); }
            set { this.SetDataPara("product_fee", value); }
        }
        /// <summary>
        /// //是否必填：否  Int折扣价格，单位分，如果有值，通知的 total_fee + discount =请求的 total_fee
        /// </summary>
        public string Discount
        {
            get { return this.GetDataPara("discount"); }
            set { this.SetDataPara("discount", value); }
        }
        /// <summary>
        /// //是否必填：否  买家别名 String(64)对应买家账号的一个加密串
        /// </summary>
        public string BuyerAlias
        {
            get { return this.GetDataPara("buyer_alias"); }
            set { this.SetDataPara("buyer_alias", value); }
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return this.TradeState == "0"; }
        }
        #endregion

        #region 方法

        #endregion
    }
}
