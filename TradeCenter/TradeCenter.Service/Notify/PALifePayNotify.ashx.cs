using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using JIT.TradeCenter.BLL;
using JIT.TradeCenter.BLL.TonysFarmRecharge;
using JIT.TradeCenter.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Pay.PALifePay.ValueObject;
using JIT.Utility.Pay.WeiXinPay.Interface;
using PayCenterNotifyService;
using TradeCenter.BLL;
using System.Collections;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// PALifePayNotify 的摘要说明
    /// </summary>
    public class PALifePayNotify : IHttpHandler
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
                //Task.Factory.StartNew(() =>
                //{
                //    HttpHelper.SendSoapRequest(DateTime.Now + "  收到旺财支付回调：" + rspStr, "http://182.254.242.12:56789/log/push/");
                //});
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Receive data from PALifePay : {0}", rspStr)
                });


                LifePayRP req = rspStr.DeserializeJSONTo<LifePayRP>();

                string orderId = req.merOrderNo;
                string resultcode = req.notifyType;
                string openId = req.openId;

                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                AppOrderEntity appOrder = bll.QueryByEntity(new AppOrderEntity()
                {
                    AppOrderID = orderId,
                }, null).FirstOrDefault();

                // 00 成功 01 失败 02 其他
                if (appOrder != null && resultcode == "00" && appOrder.Status != 2)
                {
                    appOrder.Status = 2;
                }

                if (appOrder != null && !(appOrder.IsNotified ?? false))
                {
                    try
                    {
                        string msg;
                        Hashtable ht = new Hashtable();
                        ht.Add("serialNo", req.serialNo);
                        // 异步通知cpos
                        if (NotifyHandler.Notify(appOrder, out msg, ht))
                        {
                            appOrder.IsNotified = true;
                        }
                        else
                        {
                            appOrder.NextNotifyTime = DateTime.Now.AddMinutes(1);
                        }
                        //通知完成,通知次数+1
                        appOrder.NotifyCount = (appOrder.NotifyCount ?? 0) + 1;
                        bll.Update(appOrder);
                    }
                    catch (Exception ex)
                    {
                        Loggers.Exception(new ExceptionLogInfo(ex));
                    }
                }
                if ((appOrder.IsNotified ?? false) && appOrder.Status == 2)
                {
                    res = "{\"CODE\": \"00\", \"MSG\": \"OK\"}";// 
                }
            }
            catch (Exception ex)
            {
                res = "{\"CODE\": \"01\", \"MSG\": \"" + ex + "\"}";// 
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("Response data from PALifePay : {0}", res)
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