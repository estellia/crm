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
using System.Text;
using System.Xml.Serialization;

namespace JIT.TradeCenter.Service.Notify
{
    /// <summary>
    /// WXNativePayNogify 的摘要说明
    /// </summary>
    public class WeiXinPayNotify : PaySuccessBaseNotify
    {
        public override bool Process(PayChannelEntity pChannel, HttpContext pContext, out Entity.AppOrderEntity pEntity)
        {
            pContext.Response.ContentType = "text/plain";
            try
            {
                //var WXChannel = pChannel.ChannelParameters.DeserializeJSONTo<WeiXinPayChannel>();

                byte[] buffer = new byte[pContext.Request.InputStream.Length];
                pContext.Request.InputStream.Read(buffer, 0, (int)pContext.Request.InputStream.Length);
                string str = Encoding.UTF8.GetString(buffer);
                Loggers.Debug(new DebugLogInfo() { Message = "WeiXinNotify:" + str });//微信回调返回的信息*****可用于本地测试
                //构建通知对象（反序列化）
                WeiXinPayHelper.NotifyResult result = new XmlSerializer(typeof(WeiXinPayHelper.NotifyResult)).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(str.Replace("xml>", "NotifyResult>")))) as WeiXinPayHelper.NotifyResult;

                Loggers.Debug(new DebugLogInfo() { Message = "交易状态：" + result.result_code + Environment.NewLine + result.ToJSON() });//把反序列化的数据转换成json数据
                //根据通知结果更新订单
                AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());

                Loggers.Debug(new DebugLogInfo() { Message = "WeiXinNotify_result.out_trade_no:" + result.out_trade_no });

                //根据订单号从数据库中找到记录
                pEntity = bll.GetByID(result.out_trade_no);

                if (result.result_code == "SUCCESS")
                {
                    #region 更新订单状态
                    pEntity.Status = 2;
                    pEntity.ErrorMessage = "";
                    bll.Update(pEntity);
                    #endregion
                    pContext.Response.Write("<xml><return_code><![CDATA[SUCCESS]]></return_code></xml>");
                    return true;
                }
                else
                {
                    pEntity.ErrorMessage = result.err_code_des;
                    bll.Update(pEntity);
                    pContext.Response.Write("<xml><return_code><![CDATA[FAIL]]></return_code></xml>");
                    return false;
                }
            }
            catch (Exception ex)
            {
                pEntity = null;
                Loggers.Exception(new ExceptionLogInfo(ex));
                pContext.Response.Write("<xml><return_code><![CDATA[FAIL]]></return_code></xml>");
                return false;
            }
        }
    }
}