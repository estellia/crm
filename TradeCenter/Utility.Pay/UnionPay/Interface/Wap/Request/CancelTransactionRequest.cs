/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/25 16:31:59
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;
using JIT.Utility.Pay.UnionPay.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.ValueObject;

namespace JIT.Utility.Pay.UnionPay.Interface.Wap.Request
{
    /// <summary>
    /// 交易撤销请求
    /// <remarks>
    /// <para>商户平台 -> 支付前置</para>
    /// </remarks>
    /// </summary>
    public class CancelTransactionRequest : BaseAPIRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CancelTransactionRequest()
        {
            this.Application = "MTransCancel.Req";
        }
        #endregion

        #region 参数集
        /// <summary>
        /// 商户名称
        /// <remarks>
        /// <para>0-25个任意字符</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string MerchantName
        {
            get { return this.GetParameter<string>("merchantName"); }
            set { this.SetParameter("merchantName", value); }
        }
        /// <summary>
        /// 商户代码
        /// <remarks>
        /// <para>15-24个数字字符</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string MerchantID
        {
            get { return this.GetParameter<string>("merchantId"); }
            set { this.SetParameter("merchantId", value); }
        }
        /// <summary>
        /// 商户订单号
        /// <remarks>
        /// <para>8-32个任意字符</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string MerchantOrderID
        {
            get { return this.GetParameter<string>("merchantOrderId"); }
            set { this.SetParameter("merchantOrderId", value); }
        }
        /// <summary>
        /// 商户订单时间
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public DateTime? MerchantOrderTime
        {
            get { return this.GetParameter<DateTime?>("merchantOrderTime"); }
            set { this.SetParameter("merchantOrderTime", value); }
        }
        /// <summary>
        /// 商户订单金额
        /// <remarks>
        /// <para>以分为单位</para>
        /// <para>最多不可以超过12位数</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public int? MerchantOrderAmt
        {
            get { return this.GetParameter<int?>("merchantOrderAmt"); }
            set { this.SetParameter("merchantOrderAmt", value); }
        }
        /// <summary>
        /// 订单金额币种
        /// <remarks>
        /// <para>必填参数</para>
        /// </summary>
        public Currencys? MerchantOrderCurrency
        {
            get { return this.GetParameter<Currencys?>("merchantOrderCurrency"); }
            set { this.SetParameter("merchantOrderCurrency", value); }
        }
        /// <summary>
        /// 交易类型,默认为ConsumptionRevoked
        /// <remarks>
        /// <para>取消订单时可选的交易类型有：</para>
        /// <para>1.ConsumptionRevoked</para>
        /// <para>2.PreAuthorizationRevoked</para>
        /// <para>3.PreAuthorizationCompletedRevoked</para>
        /// </remarks>
        /// </summary>
        public WapTransTypes? TransType
        {
            get { return this.GetParameter<WapTransTypes?>("transType"); }
            set { this.SetParameter("transType", value); }
        }
        /// <summary>
        /// 原交易的Cups交易流水号
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string CupsQid
        {
            get { return this.GetParameter<string>("cupsQid"); }
            set { this.SetParameter("cupsQid", value); }
        }
        /// <summary>
        /// 平台通知URL,商户平台用来接收交易结果通知的URL 
        /// <remarks>
        /// </remarks>
        /// </summary>
        public string BackUrl
        {
            get { return this.GetParameter<string>("backUrl"); }
            set { this.SetParameter("backUrl", value); }
        }
        /// <summary>
        /// 附加信息
        /// <remarks>
        /// <para>不超过200个任意字符</para>
        /// </remarks>
        /// </summary>
        public string ExtensionMessage
        {
            get { return this.GetParameter<string>("msgExt"); }
            set { this.SetParameter("msgExt", value); }
        }
        /// <summary>
        /// 自定义保留域
        /// <remarks>
        /// <para>不超过500个任意字符</para>
        /// </remarks>
        /// </summary>
        public string Misc
        {
            get { return this.GetParameter<string>("misc"); }
            set { this.SetParameter("misc", value); }
        }
        #endregion

        #region 获取请求参数的内容
        /// <summary>
        /// 获取请求参数的内容
        /// </summary>
        /// <returns></returns>
        public override string GetContent()
        {
            StringBuilder sb = new StringBuilder(base.GetContent());
            //
            if (this.MerchantName != null)
            {
                sb.AppendFormat("<merchantName>{0}</merchantName>", this.MerchantName);
            }
            else
            {
                throw new ArgumentException("参数MerchantName为必填参数.");
            }
            if (this.MerchantID != null)
            {
                sb.AppendFormat("<merchantId>{0}</merchantId>", this.MerchantID);
            }
            else
            {
                throw new ArgumentException("参数MerchantID为必填参数.");
            }
            if (this.MerchantOrderID != null)
            {
                sb.AppendFormat("<merchantOrderId>{0}</merchantOrderId>", this.MerchantOrderID);
            }
            else
            {
                throw new ArgumentException("参数MerchantOrderID为必填参数.");
            }
            if (this.MerchantOrderTime != null)
            {
                sb.AppendFormat("<merchantOrderTime>{0}</merchantOrderTime>", this.MerchantOrderTime.Value.ToAPIFormatString());
            }
            else
            {
                throw new ArgumentException("参数MerchantOrderTime为必填参数.");
            }
            if (this.MerchantOrderAmt != null)
            {
                sb.AppendFormat("<merchantOrderAmt>{0}</merchantOrderAmt>", this.MerchantOrderAmt.Value);
            }
            else
            {
                throw new ArgumentException("参数MerchantOrderAmt为必填参数.");
            }
            if (this.MerchantOrderCurrency != null)
            {
                sb.AppendFormat("<merchantOrderCurrency>{0}</merchantOrderCurrency>", this.MerchantOrderCurrency.Value.GetCode());
            }
            else
            {
                throw new ArgumentException("参数MerchantOrderCurrency为必填参数.");
            }
            if (this.TransType != null)
            {
                sb.AppendFormat("<transType>{0}</transType>", this.TransType.Value.GetCode());
            }
            if (this.CupsQid != null)
            {
                sb.AppendFormat("<cupsQid>{0}</cupsQid>", this.CupsQid);
            }
            else
            {
                throw new ArgumentException("参数CupsQid为必填参数.");
            }
            if (this.BackUrl != null)
            {
                sb.AppendFormat("<backUrl>{0}</backUrl>", this.BackUrl);
            }
            if (this.ExtensionMessage != null)
            {
                sb.AppendFormat("<msgExt>{0}</msgExt>", this.ExtensionMessage);
            }
            if (this.Misc != null)
            {
                sb.AppendFormat("<misc>{0}</misc>", this.Misc);
            }
            //结尾补闭合标签
            sb.Append("</upbp>");
            //
            return sb.ToString();
        }
        #endregion
    }
}
