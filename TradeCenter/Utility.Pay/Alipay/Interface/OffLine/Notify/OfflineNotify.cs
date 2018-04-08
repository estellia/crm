using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.Notify
{
    public class OfflineNotify : BaseNotify
    {
        #region 构造函数
        public OfflineNotify()
            : base()
        { }
        #endregion
        #region 属性
        /// <summary>
        /// 商户网站唯一订单号
        /// </summary>
        public string OutTradeNo
        {
            get { return this.GetDataPara("out_trade_no"); }
            set { this.SetDataPara("out_trade_no", value); }
        }
        /// <summary>
        /// 订单标题
        /// </summary>
        public string Subject
        {
            get { return this.GetDataPara("subject"); }
            set { this.SetDataPara("subject", value); }
        }
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string TradeNo
        {
            get { return this.GetDataPara("trade_no"); }
            set { this.SetDataPara("trade_no", value); }
        }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string TradeStatus
        {
            get { return this.GetDataPara("trade_status"); }
            set { this.SetDataPara("trade_status", value); }
        }
        /// <summary>
        /// 交易创建时间
        /// </summary>
        public string GmtCreate
        {
            get { return this.GetDataPara("gmt_create"); }
            set { this.SetDataPara("gmt_create", value); }
        }
        /// <summary>
        /// 交易付款时间
        /// </summary>
        public string GmtPayment
        {
            get { return this.GetDataPara("gmt_payment"); }
            set { this.SetDataPara("gmt_payment", value); }
        }
        /// <summary>
        /// 卖家支付宝账号，可以是email 和手机号码。
        /// </summary>
        public string SellerEmail
        {
            get { return this.GetDataPara("seller_email"); }
            set { this.SetDataPara("seller_email", value); }
        }
        /// <summary>
        /// 买家支付宝账号，可以是email 或手机号码。
        /// </summary>
        public string BuyerEmail
        {
            get { return this.GetDataPara("buyer_email"); }
            set { this.SetDataPara("buyer_email", value); }
        }
        /// <summary>
        /// 卖家支付宝账号对应的支付宝唯一用户号。以 2088 开头的纯 16 位数字。
        /// </summary>
        public string SellerID
        {
            get { return this.GetDataPara("seller_id"); }
            set { this.SetDataPara("seller_id", value); }
        }
        /// <summary>
        /// 买家支付宝账号对应的支付宝唯一用户号。以 2088 开头的纯 16 位数字。
        /// </summary>
        public string BuyerId
        {
            get { return this.GetDataPara("buyer_id"); }
            set { this.SetDataPara("buyer_id", value); }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price
        {
            get { return this.GetDataPara("price"); }
            set { this.SetDataPara("price", value); }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity
        {
            get { return this.GetDataPara("quantity"); }
            set { this.SetDataPara("quantity", value); }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string TotalFee
        {
            get { return this.GetDataPara("total_fee"); }
            set { this.SetDataPara("total_fee", value); }
        }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Body
        {
            get { return this.GetDataPara("body"); }
            set { this.SetDataPara("body", value); }
        }
        #endregion
    }
}
