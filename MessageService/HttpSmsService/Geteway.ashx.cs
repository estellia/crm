using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HttpSmsService.DataContract;
using JIT.Utility.ExtensionMethod;
using HttpSmsService.API;
using HuYiMessageService;
using Top.Api.Domain;
using JIT.Utility.SMS.Base;


namespace HttpSmsService
{
    /// <summary>
    /// Geteway 的摘要说明
    /// </summary>
    public class Geteway : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string requeststr = context.Request["request"];
            string Channel = context.Request["Channel"]; //Alidayu Huyi
            var response = new Response() { ResultCode = 0, Message = "发送成功" };
            try
            {
                var request = requeststr.DeserializeJSONTo<Request>();
                
                switch (request.Action)
                {
                    case "SendMessage":
                        if (Channel == "Alidayu")
                        {

                            BizResult alidayuResult = null;
                            alidayuResult = SMSAPI.AlidayuMessage(request);
                            if (!alidayuResult.Success)
                            {
                                response.ResultCode = 300;
                                response.Message = string.Format("发送失败:{0}  {1}", alidayuResult.ErrCode, alidayuResult.Msg);
                            }

                        }
                        else if (Channel == "PickupCode")
                        {
                            BizResult alidayuResult = null;
                            alidayuResult = SMSAPI.AlidayuMessagePickupCode(request);
                            if (!alidayuResult.Success)
                            {
                                response.ResultCode = 300;
                                response.Message = string.Format("发送失败:{0}  {1}", alidayuResult.ErrCode, alidayuResult.Msg);
                            }
                        }
                        else if(Channel == "MassSMS")
                        {
                            Result result = null;
                            result = SMSAPI.MassSMSMessage(request);
                            if (!result.IsSuccess)
                            {
                                response.ResultCode = 300;
                                response.Message = string.Format("发送失败:{0}  {1}", result.Code, result.MSG);
                            }

                            response.ResData = result;
                        }
                        else
                        {
                            Result result = null;
                            result = SMSAPI.SendMessage(request);
                            if (!result.IsSuccess)
                            {
                                response.ResultCode = 300;
                                response.Message = string.Format("发送失败:{0}  {1}", result.Code, result.MSG);
                            }

                            response.ResData = result;
                        }
                      

                        break;   
                    default:
                        throw new Exception("错误的接口:" + request.Action);
                }
                

            }
            catch (Exception ex)
            {
                response.ResultCode = 500;
                response.Message = ex.Message;
            }
            context.Response.Write(response.ToJSON());
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