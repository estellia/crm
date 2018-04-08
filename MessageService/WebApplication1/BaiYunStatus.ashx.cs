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
    /// BaiYunStatus 的摘要说明
    /// </summary>
    public class BaiYunStatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string revtime = context.Request["revtime"];
            string mobile = context.Request["mobile"];
            string linkid = context.Request["linkid"];
            string status = context.Request["status"];
            if (string.IsNullOrEmpty(revtime) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(linkid) || string.IsNullOrEmpty(status))
                context.Response.Write("err");
            else
            {
                try
                {
                    SMSSendResultEntity entity = new SMSSendResultEntity();
                    entity.Mtmsgid = linkid;
                    entity.Mtstat = status;
                    entity.Rttime = revtime;
                    entity.CreateTime = DateTime.Now;
                    entity.IsDelete = 0;
                    SMSSendResultBLL bll = new SMSSendResultBLL(new JIT.Utility.BasicUserInfo());
                    bll.Create(entity);
                    context.Response.Write("ok");
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    context.Response.Write(string.Format("err{0}{1}", Environment.NewLine, ex.Message));
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