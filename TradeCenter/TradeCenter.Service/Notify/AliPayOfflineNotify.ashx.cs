using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Pay.Alipay.Interface.Offline.Notify;
using JIT.Utility.Log;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.TradeCenter.BLL;
using JIT.Utility.ExtensionMethod;
using System.Threading.Tasks;
using JIT.TradeCenter.Service.Utils;
using PayCenterNotifyService;
using JIT.TradeCenter.Service.Notify;
using JIT.TradeCenter.Entity;

namespace JIT.TradeCenter.Service
{
    /// <summary>
    /// 支付宝线下支付通知
    /// <remarks>
    /// <para>线下支付有：声波支付,条码支付 etc..</para>
    /// <para>用户在支付平台中支付成功后,支付平台回调本接口通知订单支付成功,在本接口内处理自身的业务逻辑</para>
    /// </remarks>
    /// </summary>
    public class AliPayOfflineNotify : PaySuccessBaseNotify
    {
        public override bool Process(PayChannelEntity pChannel, HttpContext pContext, out Entity.AppOrderEntity pEntity)
        {
            pContext.Response.ContentType = "text/plain";
            try
            {
                //组织支付平台回调时回传的参数集
                Dictionary<string, string> sPara = new Dictionary<string, string>();
                foreach (var item in pContext.Request.Form.AllKeys)
                {
                    sPara[item] = pContext.Request.Form[item];
                }
                //构建通知对象
                OfflineNotify notify = new OfflineNotify();
                notify.Load(sPara);
                Loggers.Debug(new DebugLogInfo() { Message = "交易状态：" + notify.TradeStatus + Environment.NewLine + notify.ToJSON() });
                //根据通知结果更新订单
                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                //根据订单号从数据库中找到记录
                pEntity = bll.GetByID(notify.OutTradeNo);

                Loggers.Debug(new DebugLogInfo() { Message = "OutTradeNo：" + notify.OutTradeNo + ", TradeStatus:" + notify.TradeStatus + ", pEntity:" + pEntity.ToJSON() });

                if (notify.TradeStatus == TradeStatus.TRADE_FINISHED.ToString() || notify.TradeStatus == TradeStatus.TRADE_SUCCESS.ToString())
                {
                    #region 更新订单状态
                    pEntity.Status = 2;
                    pEntity.ErrorMessage = "";
                    bll.Update(pEntity);
                    #endregion
                    pContext.Response.Write("success");

                    Loggers.Debug(new DebugLogInfo() { Message = "更新订单状态成功！" });

                    return true;
                }
                else
                {
                    pEntity.ErrorMessage = notify.TradeStatus;
                    bll.Update(pEntity);
                    pContext.Response.Write("fail");

                    Loggers.Debug(new DebugLogInfo() { Message = "更新订单状态失败：" + notify.TradeStatus });

                    return false;
                }
            }
            catch (Exception ex)
            {
                pContext.Response.Write("fail");
                pEntity = null;
                Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = ex.Message });
                return false;
            }
        }
    }
}