using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.TradeCenter.Framework;
using JIT.TradeCenter.BLL;
using System.Threading.Tasks;
using TradeCenter.BLL;
using System.Text;
using JIT.TradeCenter.Service.API;

namespace JIT.TradeCenter.Service
{
    /// <summary>
    /// Gateway 的摘要说明
    /// </summary>
    public class Gateway : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            TradeResponse rsp = new TradeResponse();
            rsp.ResultCode = 0;
            //获取请求
            try
            {
                var ip = context.Request.UserHostAddress;
                var action = context.Request["action"];
                var requestStr = context.Request["request"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = action + "请求：" + requestStr
                });

                var request = requestStr.DeserializeJSONTo<TradeRequest>();
                var userInfo = new BasicUserInfo() { ClientID = request.ClientID, UserID = request.UserID };

                //if (!IsValid(ip, request))
                //{
                //    rsp.ResultCode = 100;
                //    rsp.Message = "非法的请求!";
                //    context.Response.Write(rsp.ToJSON());
                //    return;
                //}
                try
                {
                    switch (action)
                    {
                        case "CreateOrder":
                            #region 创建订单
                            var result = API.TradeAPI.CreateOrder(request);
                            rsp.Datas = result;
                            if (result.ResultCode < 100)
                                rsp.ResultCode = 0;
                            else if (result.ResultCode < 200)
                                rsp.ResultCode = 101;
                            else if (result.ResultCode < 300)
                                rsp.ResultCode = 102;
                            else if (result.ResultCode < 400)
                                rsp.ResultCode = 103;
                            else if (result.ResultCode < 500)
                                rsp.ResultCode = 104;
                            #endregion
                            break;
                        case "QueryOrder":
                            rsp.Datas = API.TradeAPI.QueryOrder(request);
                            break;
                        case "IsOrderPaid":
                            rsp.Datas = API.TradeAPI.IsOrderPaid(request);
                            break;
                        case "CreateWXNativePayUrl":
                            rsp.Datas = API.TradeAPI.CreateWXNativePayUrl(request);
                            break;
                        case "WXGetSign":
                            rsp.Datas = API.TradeAPI.WXGetSign(request);
                            break;
                        case "WXGetUpdateFeedBackUrl":
                            rsp.Datas = API.TradeAPI.WXGetUpdateFeedBackUrl(request);
                            break;
                        case "SetPayChannel":
                            rsp.Datas = API.TradeAPI.SetPayChannel(request);
                            break;
                        case "Pay":
                            rsp.Datas = TradeAPI.PrePaidCardPay(request);
                            break;
                        default:
                            throw new Exception("未知的接口名称");
                    }

                }
                catch (Exception ex)
                {
                    rsp.ResultCode = 500;
                    rsp.Message = ex.Message;
                    Loggers.Exception(new ExceptionLogInfo(userInfo, ex));
                }
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("请求:{0}{1}响应:{2}", requestStr, Environment.NewLine, rsp.ToJSON()) });
                context.Response.Write(rsp.ToJSON());
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                rsp.ResultCode = 500;
                rsp.Message = ex.Message;
                context.Response.Write(rsp.ToJSON());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public bool IsValid(string pIpAddress, TradeRequest pRequest)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "请求IP：" + pIpAddress });
            if (pIpAddress == "127.0.0.1")
                return true;
            if (pIpAddress == "::1")
                return true;
            AppWhiteListBLL bll = new AppWhiteListBLL(pRequest.GetUserInfo());
            return bll.IsValidApp(pIpAddress, pRequest.AppID.ToString(), pRequest.ClientID);
        }
    }
}