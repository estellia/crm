using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using JIT.Utility.Log;
using JIT.Utility.Pay.UnionPay.Interface.Wap;
using System.Configuration;
using JIT.Utility.Pay.UnionPay.Interface.Wap.Response;
using JIT.TradeCenter.BLL;
using System.Threading.Tasks;
using JIT.TradeCenter.Service.Utils;
using PayCenterNotifyService;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.TradeCenter.Service.Notify;
using JIT.TradeCenter.Entity;
using JIT.Utility.Pay.Negotiation;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// NegotiationNotify 的摘要说明
    /// </summary>
    public class NegotiationNotify : PaySuccessBaseNotify
    {


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public override bool Process(Entity.PayChannelEntity pChannel, HttpContext context, out Entity.AppOrderEntity entity)
        {
            entity = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                try
                {
                    #region Channel
                    var UnionWapChannel = pChannel.ChannelParameters.DeserializeJSONTo<UnionPayChannel>();
                    #endregion

                    //读取支付平台发送的交易通知密文

                    string strReq = sr.ReadToEnd();
                    Loggers.Debug(new DebugLogInfo() { Message = "Negotiation Transaction Notification Request=" + strReq });
                    //解析交易通知密文,获得交易通知请求的内容
                    string DecryptCertificateFilePath = HttpContext.Current.Server.MapPath("~/" + UnionWapChannel.DecryptCertificateFilePath);
                    var req = NegotiationGetWay.ParseTransactionNotificationRequest(DecryptCertificateFilePath, strReq);
                    Loggers.Debug(new DebugLogInfo() { Message = "Negotiation Transaction Notification Request=" + req.GetContent() });
                    AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                    entity = bll.GetByID(RemoverCovertMerchantSerial(req.MerchantOrderId));
                    if (req.IsPayFailuer)
                    {
                        try
                        {
                            //TODO:业务系统自身的订单处理逻辑（通常为：更新订单状态为支付成功）
                            #region 更新订单状态
                            entity.Status = 3;
                            entity.ErrorMessage = "";
                            bll.Update(entity);
                            #endregion
                            //业务处理完成后,告诉支付平台处理成功
                            var rsp = TransactionNotificationResponse.OK.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Transaction Notification Response={0}", rsp) });

                            context.Response.Write(rsp);
                            return true;
                        }
                        catch (Exception ex)
                        {//业务处理时如果出现异常
                            //记录日志
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            //告诉支付前置,业务处理失败,支付平台会在一定的时间范围内重发交易通知
                            var rsp = TransactionNotificationResponse.FAILED.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                            return false;
                        }
                    }
                    else if (req.IsPayOK)
                    {//用户支付成功
                        try
                        {
                            //TODO:业务系统自身的订单处理逻辑（通常为：更新订单状态为支付成功）
                            #region 更新订单状态
                            entity.Status = 2;
                            entity.ErrorMessage = "";
                            bll.Update(entity);
                            #endregion
                            //业务处理完成后,告诉支付平台处理成功
                            var rsp = TransactionNotificationResponse.OK.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Transaction Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                            return true;
                        }
                        catch (Exception ex)
                        {//业务处理时如果出现异常
                            //记录日志
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            //告诉支付前置,业务处理失败,支付平台会在一定的时间范围内重发交易通知
                            var rsp = TransactionNotificationResponse.FAILED.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                            return false;
                        }
                    }
                    else
                    {//用户支付失败
                        try
                        {
                            //TODO:业务系统自身的订单处理逻辑（通常为：更新订单状态为支付失败）
                            entity.ErrorMessage = req.PayFailedReason;
                            bll.Update(entity);
                            //业务处理完成后,告诉支付平台处理成功
                            var rsp = TransactionNotificationResponse.OK.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                            return false;
                        }
                        catch (Exception ex)
                        {//业务处理时如果出现异常
                            //记录日志
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            //告诉支付前置,业务处理失败,支付平台会在一定的时间范围内重发交易通知
                            var rsp = TransactionNotificationResponse.FAILED.GetContent();
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("Negotiation Notification Response={0}", rsp) });
                            context.Response.Write(rsp);
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {//出错
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    context.Response.Write(TransactionNotificationResponse.FAILED.GetContent());
                    return false;
                }
            }
        }
        internal static string RemoverCovertMerchantSerial(string str)
        {
            string strb = str;
            for (int i = 0; i < str.Length; i++)
            {
                string substr = strb.Substring(0, 1);
                if (substr == "0")
                {
                    strb = strb.Substring(1, strb.Length - 1);
                }
                else
                {
                    return strb;
                }
            }
            return str;
        }
    }
}