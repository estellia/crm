using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.TradeCenter.Service
{
    /// <summary>
    /// CallBack 的摘要说明
    /// </summary>
    public class CallBack : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("支付成功");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}