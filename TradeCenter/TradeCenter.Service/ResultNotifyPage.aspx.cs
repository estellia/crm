using JIT.TradeCenter.BLL.WxPayNotify;
using JIT.TradeCenter.Framework;
using JIT.TradeCenter.Service.API;
using JIT.Utility.Log;
using JIT.Utility.Pay.WeiXinPay.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.TradeCenter.Service
{
    public partial class ResultNotifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            System.IO.Stream s = Context.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "请求:" + this.GetType().ToString() + " TradeCenter Receive data from WeChat : " + builder.ToString() });

            string url = ConfigurationManager.AppSettings["ApiHost"] + "WeiXin/ResultNotifyPage.aspx";
            string response = HttpService.Post(builder.ToString(), url, false, 1000);
            JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "请求:【" + url +"】"+ this.GetType().ToString() + " TradeCenter from Page_Load WeChat : " + response });
            Context.Response.Write(response);
            Context.Response.End();
        }
    }
}