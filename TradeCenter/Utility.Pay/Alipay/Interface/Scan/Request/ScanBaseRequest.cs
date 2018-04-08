using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Scan
{
    public abstract class ScanBaseRequest
    {
        public ScanBaseRequest()
        {
            Paras = new Dictionary<string, string>();
        }

        /// <summary>
        /// 请求的基参数值
        /// </summary>
        public Dictionary<string, string> Paras { get; set; }

        /// <summary>
        /// 获取基参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <returns></returns>
        protected string GetPara(string pParaName)
        {
            if (this.Paras.ContainsKey(pParaName))
                return this.Paras[pParaName];
            else
                return default(string);
        }

        #region 公共参数属性

        /// <summary>
        /// 支付宝分配给开发者的应用ID
        /// </summary>
        public string app_id
        {
            get { return this.GetPara("app_id"); }
            set { this.Paras["app_id"] = value; }
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string method
        {
            get { return this.GetPara("method"); }
            set { this.Paras["method"] = value; }
        }

        /// <summary>
        /// 请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        public string charset
        {
            get { return this.GetPara("charset"); }
            set { this.Paras["charset"] = value; }
        }

        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA
        /// </summary>
        public string sign_type
        {
            get { return this.GetPara("sign_type"); }
            set { this.Paras["sign_type"] = value; }
        }

        /// <summary>
        /// 商户请求参数的签名串
        /// </summary>
        public string sign
        {
            get { return this.GetPara("sign"); }
            set { this.Paras["sign"] = value; }
        }
        /// <summary>
        ///发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string timestamp
        {
            get { return this.GetPara("timestamp"); }
            set { this.Paras["timestamp"] = value; }
        }
        /// <summary>
        /// 调用的接口版本
        /// </summary>
        public string version
        {
            get { return this.GetPara("version"); }
            set { this.Paras["version"] = value; }
        }

        /// <summary>
        /// 支付宝服务器主动通知商户服务器里指定的页面http路径
        /// </summary>
        public string notify_url
        {
            get { return this.GetPara("notify_url"); }
            set { this.Paras["notify_url"] = value; }
        }
        #endregion

    }
}
