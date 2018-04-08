using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Notify
{
    public class NotifyData
    {
        #region 构造函数
        public NotifyData()
        {
            DataParas = new Dictionary<string, string>();
        }
        #endregion

        #region 属性
        public Dictionary<string, string> DataParas { get; private set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentType
        {
            get { return this.DataParas["payment_type"]; }
            set { this.DataParas["payment_type"] = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Subject
        {
            get { return this.DataParas["subject"]; }
            set { this.DataParas["subject"] = value; }
        }
        /// <summary>
        /// 支付宝交易号
        /// <para>该交易在支付宝系统中的交易流水号。</para>
        /// <para>最短 16 位，最长 64 位</para>
        /// </summary>
        public string TradeNo
        {
            get { return this.DataParas["trade_no"]; }
            set { this.DataParas["trade_no"] = value; }
        }
        /// <summary>
        /// 买家支付宝账号，可以是email 或手机号码。
        /// </summary>
        public string BuyerEmail
        {
            get { return this.DataParas["buyer_email"]; }
            set { this.DataParas["buyer_email"] = value; }
        }
        /// <summary>
        /// 该笔交易创建的时间。格式为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string GmtCreate
        {
            get { return this.DataParas["gmt_create"]; }
            set { this.DataParas["gmt_create"] = value; }
        }
        /// <summary>
        /// 通知的类型。固定值。
        /// </summary>
        public string NotifyType
        {
            get { return this.DataParas["notify_type"]; }
            set { this.DataParas["notify_type"] = value; }
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        public string Quantity
        {
            get { return this.DataParas["quantity"]; }
            set { this.DataParas["quantity"] = value; }
        }
        /// <summary>
        /// 对应商户网站的订单系统中的唯一订单号，非支付宝交易号。
        ///<para>需保证在商户网站中的唯一性。是请求时对应的参数，原样返回。</para>
        /// </summary>
        public string OutTradeNo
        {
            get { return this.DataParas["out_trade_no"]; }
            set { this.DataParas["out_trade_no"] = value; }
        }
        /// <summary>
        /// 通知的发送时间。格式为 yyyy-MM-dd HH:mm:ss。
        /// </summary>
        public string NotifyTime
        {
            get { return this.DataParas["notify_time"]; }
            set { this.DataParas["notify_time"] = value; }
        }
        /// <summary>
        /// 卖家支付宝账号对应的支付宝唯一用户号。以 2088 开头的纯 16 位数字。
        /// </summary>
        public string SellerID
        {
            get { return this.DataParas["seller_id"]; }
            set { this.DataParas["seller_id"] = value; }
        }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string TradeStatus
        {
            get { return this.DataParas["trade_status"]; }
            set { this.DataParas["trade_status"] = value; }
        }
        /// <summary>
        /// 该交易是否调整过价格。本接口创建的交易不会被修改总价，固定值为 N。
        /// </summary>
        /// <param name="pPara"></param>
        public string IsTotalFeeAdjust
        {
            get { return this.DataParas["is_total_fee_adjust"]; }
            set { this.DataParas["is_total_fee_adjust"] = value; }
        }
        /// <summary>
        /// 该笔订单的总金额。请求时对应的参数，原样通知回来。
        /// </summary>
        public string TotalFee
        {
            get { return this.DataParas["total_fee"]; }
            set { this.DataParas["total_fee"] = value; }
        }
        /// <summary>
        /// 该笔交易的买家付款时间。格式为 yyyy-MM-dd HH:mm:ss。如果交易未付款，则不返回该参数。
        /// </summary>
        public string GmtPayment
        {
            get { return this.DataParas["gmt_payment"]; }
            set { this.DataParas["gmt_payment"] = value; }
        }
        /// <summary>
        /// 卖家支付宝账号，可以是email 和手机号码。
        /// </summary>
        public string SellerEmail
        {
            get { return this.DataParas["seller_email"]; }
            set { this.DataParas["seller_email"] = value; }
        }
        /// <summary>
        /// 交易关闭时间
        /// </summary>
        public string GmtClose
        {
            get { return this.DataParas["gmt_close"]; }
            set { this.DataParas["gmt_close"] = value; }
        }
        /// <summary>
        /// 商品单价
        /// <para>目前和 total_fee 值相同。单位：元。不应低于 0.01 元。</para>
        /// </summary>
        public string price
        {
            get { return this.DataParas["price"]; }
            set { this.DataParas["price"] = value; }
        }
        /// <summary>
        /// 买家支付宝账号对应的支付宝唯一用户号。以 2088 开头的纯 16 位数字。
        /// </summary>
        public string BuyerId
        {
            get { return this.DataParas["buyer_id"]; }
            set { this.DataParas["buyer_id"] = value; }
        }
        /// <summary>
        /// 通知校验 ID。唯一识别通知内容。重发相同内容的通知时，该值不变。
        /// </summary>
        public string NotifyId
        {
            get { return this.DataParas["notify_id"]; }
            set { this.DataParas["notify_id"] = value; }
        }
        /// <summary>
        /// 是否在交易过程中使用了红包。
        /// </summary>
        public string UseCoupon
        {
            get { return this.DataParas["use_coupon"]; }
            set { this.DataParas["use_coupon"] = value; }
        }
        #endregion

        #region 方法
        public void Load(Dictionary<string, string> pPara)
        {
            this.DataParas = pPara;
        }
        #endregion
    }
}
