using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Pay.UnionPay.Util;
using JIT.Utility.Pay.UnionPay.Interface.IVR;
using JIT.Utility.Pay.UnionPay.Interface.IVR.Response;

namespace JIT.TestUtility.TestPay.Web.UnionPay.IVR
{
    /// <summary>
    /// 交易通知
    /// <remarks>
    /// <para>当消费者在支付页面完成支付过程后,支付平台将向商户平台发送交易通知,商户平台在交易通知中完成对订单状态的更新等处理</para>
    /// </remarks>
    /// </summary>
    public class TranNotification : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                try
                {
                    //读取支付平台发送的交易通知密文
                    string strReq = sr.ReadToEnd();
                    Loggers.Debug(new DebugLogInfo() { Message = "[IVR]Encrypted Transaction Notification Request=" + strReq });
                    //解析交易通知密文,获得交易通知请求的内容
                    var req = IVRGateway.ParseTransactionNotificationRequest(ConfigurationManager.AppSettings["IVRDecryptCertificateFilePath"], strReq);
                    Loggers.Debug(new DebugLogInfo() { Message = "[IVR]Decrypted Transaction Notification Request=" + req.GetContent() });
                    if (req.IsPayOK)
                    {//用户支付成功
                        try
                        {
                            //TODO:业务系统自身的订单处理逻辑（通常为：更新订单状态为支付成功）


                            //业务处理完成后,告诉支付平台处理成功
                            var rsp = TransactionNotificationResponse.OK.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]Transaction Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                        }
                        catch (Exception ex)
                        {//业务处理时如果出现异常
                            //记录日志
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            //告诉支付前置,业务处理失败,支付平台会在一定的时间范围内重发交易通知
                            var rsp = TransactionNotificationResponse.FAILED.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]Transaction Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                        }
                    }
                    else
                    {//用户支付失败
                        try
                        {
                            //TODO:业务系统自身的订单处理逻辑（通常为：更新订单状态为支付失败）

                            //业务处理完成后,告诉支付平台处理成功
                            var rsp = TransactionNotificationResponse.OK.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]Transaction Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                        }
                        catch (Exception ex)
                        {//业务处理时如果出现异常
                            //记录日志
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            //告诉支付前置,业务处理失败,支付平台会在一定的时间范围内重发交易通知
                            var rsp = TransactionNotificationResponse.FAILED.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]Transaction Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                        }
                    }
                }
                catch (Exception ex)
                {//出错
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    context.Response.Write(TransactionNotificationResponse.FAILED.GetContent());
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