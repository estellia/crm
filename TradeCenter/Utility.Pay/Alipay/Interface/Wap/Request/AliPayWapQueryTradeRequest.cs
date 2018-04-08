using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.ExtensionMethod;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Request
{
    public class AliPayWapQueryTradeRequest : BaseRequest
    {
        #region 构造函数
        public AliPayWapQueryTradeRequest()
            : base()
        {
            this.SecID = "0001";
            this.Format = "xml";
            this.Version = "2.0";
        }

        public AliPayWapQueryTradeRequest(AliPayChannel pChannel)
            : this()
        {
            Channel = pChannel;
        }
        #endregion
        private AliPayChannel _channel;
        public AliPayChannel Channel
        {
            get { return this._channel; }
            set
            {
                _channel = value;
                ConfigByChannel(value);
            }
        }
        #region
        #endregion

        #region 属性
        /// <summary>
        /// 获取签名方式
        /// </summary>
        public string SecID
        {
            get { return this.GetPara("sec_id"); }
            private set { this.Paras["sec_id"] = value; }
        }

        /// <summary>
        /// 请求参数格式
        /// </summary>
        private string Format
        {
            get { return this.GetPara("format"); }
            set { this.Paras["format"] = value; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        private string Version
        {
            get { return this.GetPara("v"); }
            set { this.Paras["v"] = value; }
        }

        /// <summary>
        /// 请求的参数由子类实现
        /// </summary>
        private string ReqData
        {
            get { return this.GetPara("req_data"); }
            set { this.Paras["req_data"] = value; }
        }

        /// <summary>
        /// 授权令牌，调用“手机网页即时到账授权接口(alipay.wap.trade.create.direct)”成功后返回该值。此参数值不能更改。
        /// </summary>
        public string RequestToken
        {
            get { return this.GetDataPara("request_token"); }
            set { this.DataParas["request_token"] = value; }
        }
        #endregion

        #region 抽象方法

        protected override void SetReqData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<auth_and_execute_req>{0}</auth_and_execute_req>", this.DataParas.GetXMLString());
            this.ReqData = sb.ToString();
        }

        /// <summary>
        /// 获取Sign
        /// </summary>
        protected override void SetSign()
        {
            var sortTempdic = new SortedDictionary<string, string>(Paras);
            var tempdic = AliPayFunction.FilterPara(sortTempdic);
            var prestr = AliPayFunction.CreateLinkString(tempdic);
            switch (SecID)
            {
                case "MD5":
                    Sign = AlipayMD5.Sign(prestr, Channel.MD5Key, InputCharset);
                    break;
                case "RSA":
                    Sign = RSAFromPkcs8.Sign(prestr, Channel.RSA_PrivateKey, InputCharset);
                    break;
                case "0001":
                    Sign = RSAFromPkcs8.Sign(prestr, Channel.RSA_PrivateKey, InputCharset);
                    break;
                default:
                    throw new Exception("未知的加密方式：" + SecID);
            }
        }

        protected override bool IsValid()
        {
            return base.BaseValid() && !string.IsNullOrEmpty(RequestToken);
        }


        protected override void SetService()
        {
            this.Service = "alipay.wap.auth.authAndExecute";
        }

        #endregion

        #region 私有方法
        private void ConfigByChannel(AliPayChannel pChannel)
        {
            this.Partner = pChannel.Partner;
        }
        #endregion
    }
}
