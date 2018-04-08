using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;

namespace WebApplication1
{
    /// <summary>
    /// BaiYunRecieve 的摘要说明
    /// </summary>
    public class BaiYunRecieve : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Revtime = context.Request["Revtime"];
            string Srctermid = context.Request["Srctermid"];
            string Msgcontent = context.Request["Msgcontent"];
            string Desttermid = context.Request["Desttermid"];
            if (string.IsNullOrEmpty(Revtime) || string.IsNullOrEmpty(Srctermid)
                || string.IsNullOrEmpty(Msgcontent) || string.IsNullOrEmpty(Desttermid))
                context.Response.Write("err");
            else
            {
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("{0},{1},{2},{3}", Revtime, Srctermid, Msgcontent, Desttermid) });
                context.Response.Write("ok");
            }
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