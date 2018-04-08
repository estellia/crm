using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;
using HttpSmsService;
using NUnit.Framework;
using HttpSmsService.DataContract;
using JIT.Utility.Web;

namespace Test.HttpSmsService
{
    [TestFixture]
    public class TestAPI
    {
        private string Url = "http://www.jitmarketing.cn:10001/Geteway.ashx";
        [Test]
        public void SendMessage()
        {
            Request request = new Request();
            request.Action = "SendMessage";
            request.Parameters = new SendSmsReqPara() { MobileNO = "18019438327", SMSContent = "邓克勤上海,SJL MTD PS:2,724,508(107%)    JJTL MTD PS:#N/A(#N/A)    TTL MTD PS:2,724,508(107%)", Sign = "庄臣" };
            string str = string.Format("request={0}", request.ToJSON());
            var res = HttpClient.PostQueryString(Url, str);
            Console.WriteLine(res);
        }
    }
}
