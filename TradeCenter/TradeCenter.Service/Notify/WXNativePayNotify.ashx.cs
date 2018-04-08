using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.TradeCenter.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.TradeCenter.Entity;
using JIT.Utility.Pay.WeiXinPay.Interface.Notify;
using System.IO;
using System.Xml;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// WXNativePayNogify 的摘要说明
    /// </summary>
    public class WXNativePayNotify : PaySuccessBaseNotify
    {
        public override bool Process(PayChannelEntity pChannel, HttpContext pContext, out Entity.AppOrderEntity pEntity)
        {
             pContext.Response.ContentType = "text/plain";
            try
            {
                var orderId = pContext.Request["outTradeNo"];
                var outTradeNo = pContext.Request.QueryString["outTradeNo"];
                Loggers.Debug(new DebugLogInfo() { Message = "outTradeNo:" + outTradeNo });
                Loggers.Debug(new DebugLogInfo() { Message = "ChannelID:104&OrderId:" + orderId });
                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                //根据订单号从数据库中找到记录
                pEntity = bll.GetByID(orderId);

              
                #region 更新订单状态
                pEntity.Status = 2;
                pEntity.ErrorMessage = "";
                bll.Update(pEntity);
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                pEntity = null;
                Loggers.Exception(new ExceptionLogInfo(ex));
                return false;
            }
            
        }

        //public override bool Process(PayChannelEntity pChannel, HttpContext pContext, out Entity.AppOrderEntity pEntity)
        //{
        //    pContext.Response.ContentType = "text/plain";
        //    try
        //    {
        //        var WXChannel = pChannel.ChannelParameters.DeserializeJSONTo<WeiXinPayChannel>();
        //        Dictionary<string, string> sPara = new Dictionary<string, string>();
        //        foreach (var item in pContext.Request.QueryString.AllKeys)
        //        {
        //            sPara[item] = pContext.Request.QueryString[item];
        //        }
        //        Loggers.Debug(new DebugLogInfo() { Message = sPara.ToJSON() });
        //        //构建通知对象
        //        WXPayNotify notify = new WXPayNotify();
        //        notify.Load(sPara);

        //        Loggers.Debug(new DebugLogInfo() { Message = "交易状态：" + notify.TradeState + Environment.NewLine + notify.ToJSON() });
        //        //根据通知结果更新订单
        //        AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
        //        //根据订单号从数据库中找到记录
        //        pEntity = bll.GetByID(notify.OutTradeNo);

        //        try
        //        {
        //            string xmlStr = string.Empty;
        //            #region POST的数据内容 可获取OpenId做一些其它的业务
        //            using (var stream = pContext.Request.InputStream)
        //            {
        //                using (var rd = new StreamReader(stream, System.Text.Encoding.UTF8))
        //                {
        //                    xmlStr = rd.ReadToEnd();
        //                }
        //            }
        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(xmlStr);


        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            Loggers.Exception(new ExceptionLogInfo(ex));
        //        }
        //        if (notify.IsSuccess)
        //        {
        //            #region 更新订单状态
        //            pEntity.Status = 2;
        //            pEntity.ErrorMessage = "";
        //            bll.Update(pEntity);
        //            #endregion
        //            pContext.Response.Write("success");
        //            return true;
        //        }
        //        else
        //        {
        //            pEntity.ErrorMessage = notify.TradeState;
        //            bll.Update(pEntity);
        //            pContext.Response.Write("fail");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pEntity = null;
        //        Loggers.Exception(new ExceptionLogInfo(ex));
        //        pContext.Response.Write("fail");
        //        return false;
        //    }
        //}
    }
}