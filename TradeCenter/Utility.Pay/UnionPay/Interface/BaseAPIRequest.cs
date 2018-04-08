/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 13:09:32
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

using JIT.Utility.Pay.UnionPay.ExtensionMethod;

namespace JIT.Utility.Pay.UnionPay.Interface
{
    /// <summary>
    /// 银联的支付接口的请求的基类 
    /// </summary>
    public abstract class BaseAPIRequest:IAPIRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseAPIRequest()
        {
            this.Parameters = new Dictionary<string, object>();
            this.Version = "1.0.0";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// API的参数字典
        /// </summary>
        protected Dictionary<string, object> Parameters { get; set; }
        #endregion

        #region 工具方法
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="pKey">参数名</param>
        /// <param name="pValue">参数值</param>
        protected void SetParameter(string pKey, object pValue)
        {
            if (string.IsNullOrWhiteSpace(pKey))
                throw new ArgumentNullException("pKey");
            if (pValue == null)
            {
                if (this.Parameters.ContainsKey(pKey))
                    this.Parameters.Remove(pKey);
            }
            else
            {
                if (this.Parameters.ContainsKey(pKey))
                {
                    this.Parameters[pKey] = pValue;
                }
                else
                {
                    this.Parameters.Add(pKey, pValue);
                }
            }
        }

        /// <summary>
        /// 获取指定参数名的参数值
        /// </summary>
        /// <typeparam name="T">参数值类型</typeparam>
        /// <param name="pKey">参数名</param>
        protected T GetParameter<T>(string pKey)
        {
            if (string.IsNullOrWhiteSpace(pKey))
                throw new ArgumentNullException("pKey");
            if (!this.Parameters.ContainsKey(pKey))
                return default(T);
            else
            {
                var val = this.Parameters[pKey];
                return (T)val;
            }
        }
        #endregion

        #region 公共参数
        /// <summary>
        /// 应用名称
        /// </summary>
        protected string Application
        {
            get { return this.GetParameter<string>("application"); }
            set { this.SetParameter("application", value); }
        }

        /// <summary>
        /// 通讯协议版本号
        /// <remarks>
        /// <para>1.0.0(现默认填该值)</para>
        /// </remarks>
        /// </summary>
        protected string Version
        {
            get { return this.GetParameter<string>("version"); }
            set { this.SetParameter("version", value); }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime
        {
            get { return this.GetParameter<DateTime?>("sendTime"); }
            set { this.SetParameter("sendTime", value); }
        }

        /// <summary>
        /// 发送流水号
        /// </summary>
        public string SendSeqID
        {
            get { return this.GetParameter<string>("sendSeqId"); }
            set { this.SetParameter("sendSeqId", value); }
        }
        #endregion

        #region IAPIRequest 成员
        /// <summary>
        /// 获取请求参数的内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            //
            if (this.SendSeqID == null)
            {
                this.SendSeqID = Guid.NewGuid().ToString("N");
            }
            //
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendFormat("<upbp application=\"{0}\" version =\"{1}\" sendTime =\"{2}\" sendSeqId =\"{3}\">"
                , this.Application
                , this.Version
                , this.SendTime == null ? DateTime.Now.ToAPIFormatString() : this.SendTime.Value.ToAPIFormatString()
                , this.SendSeqID);
            //
            return sb.ToString();
        }
        #endregion
    }
}
