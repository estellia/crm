using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay.Interface.Offline.Notify;
using JIT.Utility.Log;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.ExtensionMethod;
using JIT.Utility.Pay.Alipay.Interface.Wap.Notify;
using JIT.TradeCenter.Service.Utils;
using JIT.TradeCenter.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.TradeCenter.Framework.Channel;
using System.Threading.Tasks;
using PayCenterNotifyService;
using JIT.Utility.Pay.Alipay.Channel;
using JIT.TradeCenter.Service.Notify;
using JIT.TradeCenter.Entity;
using TradeCenter.BLL;

namespace JIT.TradeCenter.Service
{
    /// <summary>
    /// AliPayWapNotify 的摘要说明
    /// <para>
    /// </para>
    /// </summary>
    public class AliPayWapNotify : PaySuccessBaseNotify
    {
        public override bool Process(PayChannelEntity pChannel, HttpContext context, out Entity.AppOrderEntity entity)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                var channel = pChannel.ChannelParameters.DeserializeJSONTo<AliPayChannel>();
                Dictionary<string, string> sPara = new Dictionary<string, string>();
                foreach (var item in context.Request.Form.AllKeys)
                {
                    sPara[item] = context.Request.Form[item];
                }
                WapNotify notify = new WapNotify();
                notify.Load(sPara);
                var data = notify.GetNotifyData(channel);
                Loggers.Debug(new DebugLogInfo() { Message = "交易状态：" + data.TradeStatus });
                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                entity = bll.GetByID(data.OutTradeNo);
                Loggers.Debug(new DebugLogInfo() { Message = "AppOrder：" + entity.ToJSON() });
                if (data.TradeStatus == TradeStatus.TRADE_FINISHED.ToString() || data.TradeStatus == TradeStatus.TRADE_SUCCESS.ToString())
                {
                    #region 分润
                    //PayChannelBLL pbll = new PayChannelBLL(new Utility.BasicUserInfo());
                    //var channel = pbll.GetByID(entity.PayChannelID).ChannelParameters.DeserializeJSONTo<AliPayWapChannel>();
                    //RoyaltyRequest royaltyrequest = new RoyaltyRequest()
                    //{
                    //    TradeNo = data.TradeNo,
                    //    OutTradeNo = data.OutTradeNo,
                    //    OutBillNo = Helper.GetDataRandom(),
                    //    Partner = AliPayConfig.Partner,
                    //    RoyaltyType = "10",
                    //    RoyaltyParameters = "harvey0930@hotmail.com^0.01^Test",
                    //};
                    //if (!string.IsNullOrEmpty(channel.GetRoyaltyStr()))
                    //{
                    //    royaltyrequest.RoyaltyParameters = channel.GetRoyaltyStr();
                    //}
                    //try
                    //{
                    //    var royalReaponse = AliPayWapGeteway.GetRoyaltyResponse(royaltyrequest);
                    //    Loggers.Debug(new DebugLogInfo() { Message = royalReaponse.ToJSON() });
                    //    if (royalReaponse.IsSuccess == "T")
                    //    {
                    //        Loggers.Debug(new DebugLogInfo() { Message = "分润成功" });
                    //    }
                    //    else
                    //    {
                    //        Loggers.Debug(new DebugLogInfo() { Message = "分润失败" });
                    //    }
                    //    context.Response.Write("successss");
                    //    Loggers.Debug(new DebugLogInfo() { Message = "交易成功" });
                    //}
                    //catch (Exception ex)
                    //{
                    //    context.Response.Write("fail");
                    //    Loggers.Exception(new ExceptionLogInfo(ex));
                    //}
                    #endregion
                    #region 更新订单状态
                    entity.Status = 2;
                    entity.ErrorMessage = "";
                    bll.Update(entity);
                    Loggers.Debug(new DebugLogInfo() { Message = "更新订单状态成功！" });
                    #endregion
                    context.Response.Write("success");
                    return true;
                }
                else
                {
                    entity.ErrorMessage = data.TradeStatus;
                    bll.Update(entity);
                    context.Response.Write("fail");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                entity = null;
                context.Response.Write("fail");
                return false;
            }
        }
    }
}