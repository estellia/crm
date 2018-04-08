using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Negotiation.Interface.Request
{
    public class BatchPayRequest : NeBaseAPIRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BatchPayRequest(string merchantId)
        {
            this.Application = "BatchPay";
            this.MerchantId = merchantId;
        }

        #endregion

        #region 参数集

        /// <summary>
        /// 商户流水号
        /// <remarks>
        /// <para>最多不可以超过20位数</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string MerchantSerial
        {
            get { return this.GetParameter<string>("merchantSerial"); }
            set { this.SetParameter("merchantSerial", value); }
        }


        /// <summary>
        /// 交易时间
        /// <remarks>
        /// <para>最多不可以超过20位数</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string TransTime
        {
            get { return this.GetParameter<string>("transTime"); }
            set { this.SetParameter("transTime", value); }
        }
        /// <summary>
        /// 交易时间
        /// <remarks>
        /// <para>最多不可以超过20位数</para>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string TransList
        {
            get { return this.GetParameter<string>("transList"); }
            set { this.SetParameter("transList", value); }
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

        /// <summary>
        /// msgExt
        /// <remarks>
        /// <para>不超过200个任意字符</para>
        /// </remarks>
        /// </summary>
        public string MsgExt
        {
            get { return this.GetParameter<string>("msgExt"); }
            set { this.SetParameter("msgExt", value); }
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
            if (this.MerchantSerial != null)
            {
                sb.AppendFormat("<merchantSerial>{0}</merchantSerial>", this.MerchantSerial);
            }
            else
            {
                throw new ArgumentException("参数MerchantSerial为必填参数.");
            }

            if (this.TransTime != null)
            {
                sb.AppendFormat("<transTime>{0}</transTime>", this.TransTime);
            }
            else
            {
                throw new ArgumentException("参数TransTime为必填参数.");
            }

            if (this.TransList != null)
            {
                sb.AppendFormat("<transList>{0}</transList>", this.TransList);
            }
            else
            {
                throw new ArgumentException("参数TransList为必填参数.");
            }
            if (this.MsgExt != null)
            {
                sb.AppendFormat("<msgExt>{0}</msgExt>", this.MsgExt);
            }

            if (this.Misc != null)
            {
                sb.AppendFormat("<misc>{0}</misc>", this.Misc);
            }
            //结尾补闭合标签
            sb.Append("</bpsa>");
            return sb.ToString();
        }
        #endregion
    }
}
