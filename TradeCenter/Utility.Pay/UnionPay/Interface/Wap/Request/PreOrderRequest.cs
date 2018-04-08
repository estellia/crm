/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 13:39:46
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
    /// 预订单请求
    /// <remarks>
    /// <para>商户平台 -> 支付前置</para>
    /// </remarks>
    /// </summary>
    public class PreOrderRequest:BaseAPIRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PreOrderRequest()
        {
            this.Application = "MGw.Req";
            this.GatewayType = GatewayTypes.WAP;
        }
        #endregion

        #region 参数集
        /// <summary>
        /// 商户名称
        /// <remarks>
        /// <para>0-25个任意字符</para>
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
        /// </summary>
        public Currencys? MerchantOrderCurrency
        {
            get { return this.GetParameter<Currencys?>("merchantOrderCurrency"); }
            set { this.SetParameter("merchantOrderCurrency", value); }
        }

        /// <summary>
        /// 商户订单描述
        /// <remarks>
        /// <para>0-128个的任意字符</para>
        /// <para>可选参数</para>
        /// </remarks>
        /// </summary>
        public string MerchantOrderDesc
        {
            get { return this.GetParameter<string>("merchantOrderDesc"); }
            set { this.SetParameter("merchantOrderDesc", value); }
        }

        /// <summary>
        /// 交易超时时间
        /// <remarks>
        /// <para>渠道平台需要严格控制，对交易超时的订单，拒绝支付</para>
        /// </remarks>
        /// </summary>
        public DateTime? TransTimeout
        {
            get { return this.GetParameter<DateTime?>("transTimeout"); }
            set { this.SetParameter("transTimeout", value); }
        }

        /// <summary>
        /// 交易类型.如不填写，默认为消费
        /// <remarks>
        /// <para>预订单时可选择的交易类型有：Consumption和PreAuthorization</para>
        /// </remarks>
        /// </summary>
        public WapTransTypes? TransType
        {
            get { return this.GetParameter<WapTransTypes?>("transType"); }
            set { this.SetParameter("transType", value); }
        }

        /// <summary>
        /// 支付网关类型
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        protected GatewayTypes? GatewayType
        {
            get { return this.GetParameter<GatewayTypes?>("gwType"); }
            set { this.SetParameter("gwType", value); }
        }

        ///// <summary>
        ///// 支付网关调用地址或调用指令
        ///// <remarks>
        ///// <para>最多不超过256个任意字符</para>
        ///// </remarks>
        ///// </summary>
        //public string GatewayInvokeCommand
        //{
        //    get { return this.GetParameter<string>("gwInvokeCmd"); }
        //    set { this.SetParameter("gwInvokeCmd", value); }
        //}

        /// <summary>
        /// 前台通知URL
        /// <remarks>
        /// <para>用来接收交易结果通知并跳转返回的URL</para>
        /// <para>最多不超过256个任意字符</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string FrontUrl
        {
            get { return this.GetParameter<string>("frontUrl"); }
            set { this.SetParameter("frontUrl", value); }
        }

        /// <summary>
        /// 后台通知URL
        /// <remarks>
        /// <para>用来接收交易结果通知的后台URL</para>
        /// <para>最多不超过256个任意字符</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string BackUrl
        {
            get { return this.GetParameter<string>("backUrl"); }
            set { this.SetParameter("backUrl", value); }
        }

        /// <summary>
        /// 商户处的用户名
        /// <remarks>
        /// <para>最多不超过100个任意字符</para>
        /// </remarks>
        /// </summary>
        public string MerchantUserID
        {
            get { return this.GetParameter<string>("merchantUserId"); }
            set { this.SetParameter("merchantUserId", value); }
        }

        /// <summary>
        /// 手机号码
        /// <remarks>
        /// <para>定长的11个数字字符</para>
        /// </remarks>
        /// </summary>
        public string MobileNum
        {
            get { return this.GetParameter<string>("mobileNum"); }
            set { this.SetParameter("mobileNum", value); }
        }

        /// <summary>
        /// 姓名
        /// <remarks>
        /// <para>不超过20个的任意字符</para>
        /// </remarks>
        /// </summary>
        public string UserName
        {
            get { return this.GetParameter<string>("userName"); }
            set { this.SetParameter("userName", value); }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public IDTypes? IDType
        {
            get { return this.GetParameter<IDTypes?>("idType"); }
            set { this.SetParameter("idType", value); }
        }

        /// <summary>
        /// 证件号码
        /// <remarks>
        /// <para>默认为身份证号</para>
        /// <para>不超过30个任意字符</para>
        /// </remarks>
        /// </summary>
        public string IDNum
        {
            get { return this.GetParameter<string>("idNum"); }
            set { this.SetParameter("idNum", value); }
        }

        /// <summary>
        /// 卡号
        /// <remarks>
        /// <para>不超过20个任意字符</para>
        /// </remarks>
        /// </summary>
        public string CarNum
        {
            get { return this.GetParameter<string>("cardNum"); }
            set { this.SetParameter("cardNum", value); }
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
                sb.AppendFormat("<merchantName>{0}</merchantName>",this.MerchantName);
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
            if (this.MerchantOrderDesc != null)
            {
                sb.AppendFormat("<merchantOrderDesc>{0}</merchantOrderDesc>", this.MerchantOrderDesc);
            }
            if (this.TransTimeout != null)
            {
                sb.AppendFormat("<transTimeout>{0}</transTimeout>", this.TransTimeout.Value.ToAPIFormatString());
            }
            if (this.TransType != null)
            {
                sb.AppendFormat("<transType>{0}</transType>", this.TransType.Value.GetCode());
            }
            if (this.GatewayType != null)
            {
                sb.AppendFormat("<gwType>{0}</gwType>", this.GatewayType.Value.GetCode());
            }
            else
            {
                throw new ArgumentException("参数GatewayType为必填参数.");
            }
            if (this.FrontUrl != null)
            {
                sb.AppendFormat("<frontUrl>{0}</frontUrl>", this.FrontUrl);
            }
            else
            {
                throw new ArgumentException("参数FrontUrl为必填参数.");
            }
            if (this.BackUrl != null)
            {
                sb.AppendFormat("<backUrl>{0}</backUrl>", this.BackUrl);
            }
            else
            {
                throw new ArgumentException("参数BackUrl为必填参数.");
            }
            if (this.MerchantUserID != null)
            {
                sb.AppendFormat("<merchantUserId>{0}</merchantUserId>", this.MerchantUserID);
            }
            if (this.MobileNum != null)
            {
                sb.AppendFormat("<mobileNum>{0}</mobileNum>", this.MobileNum);
            }
            if (this.UserName != null)
            {
                sb.AppendFormat("<userName>{0}</userName>", this.UserName);
            }
            if (this.IDType != null)
            {
                sb.AppendFormat("<idType>{0}</idType>", this.IDType.Value.GetCode());
            }
            if (this.IDNum != null)
            {
                sb.AppendFormat("<idNum>{0}</idNum>", this.IDNum);
            }
            if (this.CarNum != null)
            {
                sb.AppendFormat("<cardNum>{0}</cardNum>", this.CarNum);
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
