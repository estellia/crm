using JIT.TradeCenter.BLL;
using JIT.TradeCenter.BLL.TonysFarmRecharge;
using JIT.TradeCenter.BLL.WxPayNotify;
using JIT.TradeCenter.Entity;
using JIT.Utility.Log;
using PayCenterNotifyService;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// WxScanQrCodePayNotify 的摘要说明
    /// </summary>
    public class WxScanQrCodePayNotify : IHttpHandler
    {
        #region testdata
        /*
         <xml><appid><![CDATA[wxb4f8f3d799d22f03]]></appid>
        <attach><![CDATA[测试数据]]></attach>
        <bank_type><![CDATA[CFT]]></bank_type>
        <cash_fee><![CDATA[1]]></cash_fee>
        <fee_type><![CDATA[CNY]]></fee_type>
        <is_subscribe><![CDATA[Y]]></is_subscribe>
        <mch_id><![CDATA[1264926201]]></mch_id>
        <nonce_str><![CDATA[rdwbEb2FXXmV7LBF]]></nonce_str>
        <openid><![CDATA[oY-Cqs7wZ6p_Cq_0AAP2QHLhANRc]]></openid>
        <out_trade_no><![CDATA[126492620120160426005955291]]></out_trade_no>
        <result_code><![CDATA[SUCCESS]]></result_code>
        <return_code><![CDATA[SUCCESS]]></return_code>
        <sign><![CDATA[C251718418AFA3FDEB764E258F220340]]></sign>
        <time_end><![CDATA[20160426010056]]></time_end>
        <total_fee>1</total_fee>
        <trade_type><![CDATA[NATIVE]]></trade_type>
        <transaction_id><![CDATA[4008492001201604265225452570]]></transaction_id>
        </xml>
         */
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            WxPayData res = new WxPayData();
            try
            {
                context.Response.ContentType = "text/plain";

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

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Receive data from WeChat : {0}", builder.ToString())
                });

                string rspStr = builder.ToString().
                    Replace("<![CDATA[", "").Replace("]]>", "");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rspStr);

                string orderId = doc.GetElementsByTagName("out_trade_no")[0].InnerText;
                string resultcode = doc.GetElementsByTagName("result_code")[0].InnerText;
                string openId = doc.GetElementsByTagName("openid")[0].InnerText;

                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                AppOrderEntity appOrder = bll.QueryByEntity(new AppOrderEntity()
                {
                    AppOrderID = orderId,
                }, null).FirstOrDefault();


                if (appOrder != null && resultcode == "SUCCESS" && appOrder.Status != 2)
                {
                    appOrder.Status = 2;
                    new RechargeBLL().RechargeTonysCardAct2(appOrder);
                }

                if (appOrder != null && !(appOrder.IsNotified ?? false))
                {
                    try
                    {
                        string msg;
                        if (NotifyHandler.Notify(appOrder, out msg, true))
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
                        res.SetValue("return_code", "FAIL");
                        res.SetValue("return_msg", "FAIL");
                    }
                }

                NotifyHandler.NotifyTFSaveWxVipInfo(appOrder.AppOrderID, appOrder.AppClientID, openId);
                if ((appOrder.IsNotified ?? false) && appOrder.Status == 2)
                {
                    res.SetValue("return_code", "SUCCESS");
                    res.SetValue("return_msg", "OK");
                }
            }
            catch (Exception ex)
            {
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
            }
            context.Response.Write(res.ToXml());
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