using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.Pay.Alipay.Interface.Wap.Notify;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.TestUtility.TestPay.Web.AliPay
{
    public partial class NotifyFrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> sPara = new Dictionary<string, string>();
                foreach (var item in Request.Form.AllKeys)
                {
                    sPara[item] = Request.Form[item];
                }
                WapNotify notify = new WapNotify();
                notify.Load(sPara);
                AliPayChannel channel = new AliPayChannel();
                var data = notify.GetNotifyData(channel);

                Loggers.Debug(new DebugLogInfo() { Message = "交易状态：" + data.TradeStatus });
                if (data.TradeStatus == TradeStatus.TRADE_FINISHED.ToString() || data.TradeStatus == TradeStatus.TRADE_SUCCESS.ToString())
                {
                    //分润
                    RoyaltyRequest royaltyrequest = new RoyaltyRequest()
                    {
                        TradeNo = data.TradeNo,
                        OutTradeNo = data.OutTradeNo,
                        OutBillNo = GetDataRandom(),
                        RoyaltyType = "10",
                        RoyaltyParameters = "harvey0930@hotmail.com^0.01^Test",
                    };
                    try
                    {
                        var royalReaponse = AliPayWapGeteway.GetRoyaltyResponse(royaltyrequest);
                        Loggers.Debug(new DebugLogInfo() { Message = royalReaponse.ToJSON() });
                        if (royalReaponse.IsSuccess == "T")
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = "分润成功" });
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = "分润失败" });
                        }
                        Response.Write("successss");
                        Loggers.Debug(new DebugLogInfo() { Message = "交易成功" });
                    }
                    catch (Exception ex)
                    {
                        Response.Write("fail");
                        Loggers.Exception(new ExceptionLogInfo(ex));
                    }
                }
                else
                {
                    Response.Write("fail");
                    Loggers.Debug(new DebugLogInfo() { Message = "交易失败" });
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                Response.Write("fail");
            }
        }

        /// <summary>
        /// 获取16位随机数
        /// </summary>
        /// <returns></returns>
        public string GetDataRandom()
        {
            string strData = string.Empty;
            strData += DateTime.Now.Year;
            strData += DateTime.Now.Month;
            strData += DateTime.Now.Day;
            strData += DateTime.Now.Hour;
            strData += DateTime.Now.Minute;
            strData += DateTime.Now.Second;
            Random r = new Random();
            strData = strData + r.Next(100);
            return strData;
        }
    }
}