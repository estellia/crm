using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Negotiation.Interface
{
    public abstract class NeBaseAPIRequest : IAPIRequest
    {
        #region 属性集
        /// <summary>
        /// API的参数字典
        /// </summary>
        private Dictionary<string, object> Parameters { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public NeBaseAPIRequest()
        {
            this.Parameters = new Dictionary<string, object>();
            this.Version = "1.0.0";
        }
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
        ///商户编号
        /// </summary>
        protected string MerchantId
        {
            get { return this.GetParameter<string>("merchantId"); }
            set { this.SetParameter("merchantId", value); }
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
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendFormat("<bpsa application=\"{0}\" version=\"{1}\">", this.Application + ".Req", this.Version);
            sb.AppendFormat("<merchantId>{0}</merchantId>", this.MerchantId);
            return sb.ToString();
        }
        #endregion
    }
}
