using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JIT.Utility.SMS
{
    public class BaiYunSMS : Base.BaseSMS
    {
        public BaiYunSMS()
        {
            PID = ConfigurationManager.AppSettings["PID"];
            Number = ConfigurationManager.AppSettings["Number"];
            Password = ConfigurationManager.AppSettings["PWD"];
        }
        public string PID { get; private set; }
        public string Number { get; private set; }
        public string Password { get; private set; }
        public int? Extend { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }

        public override string GetURL()
        {
            return ConfigurationManager.AppSettings["BaiYunSendUrl"];
        }

        public override string GetParamStr()
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(PID))
                str += "pid={0}&".Fmt(PID);
            if (!string.IsNullOrEmpty(Number))
                str += "number={0}&".Fmt(Number);
            if (!string.IsNullOrEmpty(Mobile))
                str += "mobile={0}&".Fmt(Mobile);
            if (!string.IsNullOrEmpty(Message))
                str += "message={0}&".Fmt(Message.UrlEncode());
            if (Extend.HasValue)
                str += "extend={0}&".Fmt(Extend);
            if (!string.IsNullOrEmpty(Password))
                str += "password={0}&".Fmt(Password);
            return str.Trim('&');
        }

        public override byte[] GetData()
        {
            string str = GetParamStr();
            return str.GetData();
        }

        public string GetBalance()
        {
            string url = "{0}?user={1}&pass={2}".Fmt(ConfigurationManager.AppSettings["BaiYunBalanceUrl"], "jit", Password);
            return MessageMethod.doGetRequest(url);
        }
    }
}
