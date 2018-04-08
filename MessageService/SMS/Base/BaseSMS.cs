using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JIT.Utility.SMS.Base
{
    public enum SendType
    {
        Get,
        Post
    }

    public abstract class BaseSMS : ISMS
    {
        public BaseSMS()
        {
            DataPara = new Dictionary<string, string>();
        }

        public Dictionary<string, string> DataPara { get; set; }

        public string Send(SendType type)
        {
            switch (type)
            {
                case SendType.Get:
                    return MessageMethod.doGetRequest(GetFullURL());
                case SendType.Post:
                    return MessageMethod.doPostRequest(GetURL(), GetData());
                default:
                    return string.Empty;
            }
        }

        public string Send2(SendType type,string url = "")
        {
            switch (type)
            {
                case SendType.Get:
                    return MessageMethod.doGetRequest(GetFullURL2(url));//GetFullURL2拼成了get方式的发送短信的请求
                case SendType.Post:
                    return MessageMethod.doPostRequest(GetURL(), GetData());
                default:
                    return string.Empty;
            }
        }

        public string GetFullURL()
        {
            return GetURL().Trim('/') + "?" + GetParamStr();
        }

        public string GetFullURL2(string url)
        {
            return url.Trim('/') + "?method=Submit" + GetParamStr2();
        }


        public abstract string GetURL();

        public abstract string GetParamStr();

        public abstract byte[] GetData();

        protected string GetPara(string pKey)
        {
            if (this.DataPara.ContainsKey(pKey))
                return DataPara[pKey];
            else
                return default(string);
        }

        protected void SetPara(string pKey, string pValue)
        {
            this.DataPara[pKey] = pValue;
        }

        protected string GetParamStr2()
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in DataPara)
            {
                if (!string.IsNullOrEmpty(item.Value))
                    str.Append(string.Format("&{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value)));
            }
            return str.ToString();
        }
    }
}
