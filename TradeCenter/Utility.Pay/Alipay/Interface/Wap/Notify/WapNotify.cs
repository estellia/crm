using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using JIT.Utility.Pay.Alipay.Util;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Notify
{
    public class WapNotify : BaseNotify
    {
        #region 构造函数
        public WapNotify()
            : base()
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// 接口名称。
        /// </summary>
        public string Service
        {
            get { return this.DataParas["service"]; }
            set { this.DataParas["service"] = value; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get { return this.DataParas["v"]; }
            set { this.DataParas["v"] = value; }
        }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string SecID
        {
            get { return this.DataParas["sec_id"]; }
            set { this.DataParas["sec_id"] = value; }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            get { return this.DataParas["sign"]; }
            set { this.DataParas["sign"] = value; }
        }
        /// <summary>
        /// 通知业务参数字符串
        /// </summary>
        public string NotifyDataStr
        {
            get { return this.DataParas["notify_data"]; }
            set { this.DataParas["notify_data"] = value; }
        }


        #endregion

        #region 方法
        public NotifyData GetNotifyData(AliPayChannel pChannel)
        {
            var str = string.Empty;

            if (this.SecID == "0001")
            {
                str = RSAFromPkcs8.DecryptData(NotifyDataStr, pChannel.RSA_PrivateKey, AliPayConfig.InputCharset);
            }
            else
            {
                str = HttpUtility.UrlDecode(NotifyDataStr);
            }
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    Log.Loggers.Debug(new Log.DebugLogInfo() { Message ="阿里PAY支付通知的业务数据:"+ str });
                    Dictionary<string, string> tempDic = new Dictionary<string, string>();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(str);
                    foreach (XmlElement item in doc.DocumentElement.ChildNodes)
                    {
                        tempDic[item.Name] = item.InnerText;
                    }
                    NotifyData data = new NotifyData();
                    data.Load(tempDic);
                    return data;
                }
                catch (Exception ex)
                {
                    Log.Loggers.Exception(new Log.ExceptionLogInfo(ex) { ErrorMessage = str + ex.Message });
                }
            }
            return null;
        }
        #endregion
    }
}
