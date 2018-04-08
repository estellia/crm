using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Pay.PALifePay.ValueObject;
using PayCenterNotifyService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TradeCenter.BLL;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// PAPayRetAmtNotify 的摘要说明
    /// </summary>
    public class PAPayRetAmtNotify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string res = "{\"CODE\": \"00\", \"MSG\": \"OK\"}";// Y/N 接收成功或失败
            try
            {
                context.Response.ContentType = "application/json";
                #region 获取流数据
                System.IO.Stream s = context.Request.InputStream;
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
                #endregion

                string rspStr = builder.ToString();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Receive data from PAPayRetAmt : {0}", rspStr)
                });

                string result = HttpHelper.SendSoapRequest(rspStr, System.Configuration.ConfigurationManager.AppSettings["PAReturnAmountNotify"], "application/json");
                if(result.ToLower().Equals("true"))
                {
                    res = "{\"CODE\": \"00\", \"MSG\": \"OK\"}";// 
                }
                res = "{\"CODE\": \"01\", \"MSG\": \"OK\"}";// 
            }
            catch (Exception ex)
            {
                res = "{\"CODE\": \"01\", \"MSG\": \"" + ex + "\"}";// 
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("Response data from PAPayRetAmt : {0}", res)
            });
            context.Response.Write(res);
            context.Response.End();
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