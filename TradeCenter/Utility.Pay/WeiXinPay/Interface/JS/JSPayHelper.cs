using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Pay.WeiXinPay.Interface.JS
{
    public class JSPayHelper : WeiXinPayBaseRequest
    {
        public JSPayHelper(WeiXinPayChannel pChannel)
            : base(pChannel)
        {
            SignType = "SHA1";
        }

        public string SignType
        {
            get
            {
                return GetPara("signType");
            }
            set
            {
                SetPara("signType", value);
            }
        }

        public override string GetContent()
        {
            var dic = new Dictionary<string, string>();
            dic["appId"] = this.AppId;
            dic["timeStamp"] = this.TimeStamp;
            dic["nonceStr"] = this.NonceStr;
            dic["package"] = this.Package;
            dic["signType"] = this.SignType;
            dic["paySign"] = this.AppSignature;
            return dic.ToJSON();
        }
    }
}
