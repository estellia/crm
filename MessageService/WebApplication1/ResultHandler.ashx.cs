using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.SMSReportService.Entity;
using JIT.SMSReportService.BLL;
using JIT.Utility.Log;

namespace WebApplication1
{
    /// <summary>
    /// ResultHandler 的摘要说明
    /// </summary>
    public class ResultHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "接收到请求" });
            context.Response.ContentType = "text/plain";
            string command = context.Request.QueryString["command"];
            string spid = context.Request.QueryString["spid"];
            string sppassword = context.Request.QueryString["sppassword"];
            string spsc = context.Request.QueryString["spsc"];
            string mtmsgid = context.Request.QueryString["mtmsgid"];
            string mtstat = context.Request.QueryString["mtstat"];
            string mterrcode = context.Request.QueryString["mterrcode"];
            string rttime = context.Request.QueryString["rttime"];

            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(spid)
                || string.IsNullOrEmpty(mtmsgid) || string.IsNullOrEmpty(mtstat) || string.IsNullOrEmpty(mterrcode))
            {
                context.Response.Write("command=RT_RESPONSE&spid=" + spid + "&mtmsgid=" + mtmsgid + "&rtstat=ET:0101&rterrcode=100");
                return;
            }
            else
            {
                try
                {
                    SMSSendResultEntity entity = new SMSSendResultEntity();
                    entity.Command = command;
                    entity.Spid = spid;
                    entity.Sppassword = sppassword;
                    entity.Spsc = spsc;
                    entity.Mtmsgid = mtmsgid;
                    entity.Mtstat = mtstat;
                    entity.Mterrocde = mterrcode;
                    entity.Rttime = rttime;
                    entity.CreateTime = DateTime.Now;
                    entity.IsDelete = 0;
                    SMSSendResultBLL bll = new SMSSendResultBLL(new JIT.Utility.BasicUserInfo());
                    bll.Create(entity);
                    context.Response.Write("command=RT_RESPONSE&spid=" + spid + "&mtmsgid=" + mtmsgid + "&rtstat=ACCEPT&rterrcode=000");
                }
                catch (Exception ex)
                {
                    context.Response.Write("command=RT_RESPONSE&spid=" + spid + "&mtmsgid=" + mtmsgid + "&rtstat=ET:0226&rterrcode=100");
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
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