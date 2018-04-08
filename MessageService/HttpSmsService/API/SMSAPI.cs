using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Text;
using HttpSmsService.DataContract;
using JIT.Utility.SMS.BLL;
using JIT.Utility.SMS.Entity;
using HuYiMessageService;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.ManagementPlatform.Web.Module.BLL;
using JIT.ManagementPlatform.Web.Module.Entity;
using JIT.Utility;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api;
using Top.Api.Domain;
using JIT.Utility.SMS.Base;
using System.Configuration;

namespace HttpSmsService.API
{
    public static class SMSAPI
    {
        /// <summary>
        /// 互亿无线
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static Result SendMessage(DataContract.Request request)
        {
            string url = WebConfigurationManager.AppSettings["SMSSendURL"].ToString();
            var para = request.GetParameters<SendSmsReqPara>();
            SMSSendBLL bll = new SMSSendBLL(new JIT.Utility.BasicUserInfo());
            SMSCustomerBLL smsCustomerBll = new SMSCustomerBLL(new JIT.Utility.BasicUserInfo());
            var smsCustomer = smsCustomerBll.GetByID(null, para.Sign);
            var entity = new SMSSendEntity()
            {
                MobileNO = para.MobileNO,
                Sign = para.Sign,
                SMSContent = para.SMSContent,
                Account = smsCustomer.Account,
                Password = smsCustomer.Password
            };
            //调用短信接口直接发送
            var result = GetResult(entity.GetSMS().Send2(JIT.Utility.SMS.Base.SendType.Get,url));//GetSMS确定了是用的HuYiSMS

            if (result.IsSuccess)
            {   
                entity.Status = 1;
                entity.SendCount = (entity.SendCount ?? 0) + 1;
                entity.Mtmsgid = result.SMSID;
                entity.RegularlySendTime = DateTime.Now;
                Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, entity.MobileNO, entity.SMSContent, result.ToJSON()) });
            }
            else
            {
                entity.SendCount = (entity.SendCount ?? 0) + 1;
                Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, entity.MobileNO, entity.SMSContent, result.ToJSON()) });
            }
            //保存数据库
            bll.Create(entity);
            return result;
        }

        /// <summary>
        /// 互亿无线群发
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static Result MassSMSMessage(DataContract.Request request)
        {
            string url = WebConfigurationManager.AppSettings["MassSMSURL"].ToString();
            var para = request.GetParameters<SendSmsReqPara>();
            SMSSendBLL bll = new SMSSendBLL(new JIT.Utility.BasicUserInfo());
            var entity = new SMSSendEntity()
            {
                MobileNO = para.MobileNO,
                Sign = para.Sign,
                SMSContent = para.SMSContent,
                Account = "cf_znxx",
                Password = "03362ea3dbef364fddde820882f544ca"
            };
            //调用短信接口直接发送
            var result = GetResult(entity.GetSMS().Send2(SendType.Get, url));//GetSMS确定了是用的HuYiSMS

            if (result.IsSuccess)
            {
                entity.Status = 1;
                entity.SendCount = (entity.SendCount ?? 0) + 1;
                entity.Mtmsgid = result.SMSID;
                entity.RegularlySendTime = DateTime.Now;
              //  Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, entity.MobileNO, entity.SMSContent, result.ToJSON()) });
            }
            else
            {
                entity.SendCount = (entity.SendCount ?? 0) + 1;
                //Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, entity.MobileNO, entity.SMSContent, result.ToJSON()) });
            }
            return result;
        }

        /// <summary>
        /// 阿里大鱼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static BizResult AlidayuMessage(DataContract.Request request)
        {

            var para = request.GetParameters<SendSmsReqPara>();
            //配置模板ID
            var tplId = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TemplateID"]) ? "SMS_7730051" : ConfigurationManager.AppSettings["TemplateID"];

            AlibabaAliqinFcSmsNumSendResponse rsp = new AlibabaAliqinFcSmsNumSendResponse();

            string pSMSContent = "{\"code\":\"" + para.SMSContent + " \"}";
            rsp = AlidatySendMessage(para.Sign, pSMSContent, para.MobileNO, tplId);              
            return rsp.Result;
        }

        /// <summary>
        /// 阿里大鱼（发送取件码）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static BizResult AlidayuMessagePickupCode(DataContract.Request request)
        {
            return null;

            //以下代码少文件
            /*
            var para = request.GetParameters<PickupCodeReqPara>();

            AlibabaAliqinFcSmsNumSendResponse rsp = new AlibabaAliqinFcSmsNumSendResponse();

            string pSMSContent = "{\"orderNo\":\"" + para.OrderNo + " \",\"pickupCode\":\"" + para.PickupCode + " \"}";
            rsp = AlidatySendMessage(para.Sign, pSMSContent, para.MobileNO, "SMS_14265183");
            return rsp.Result;
            */
        }

        /// <summary>
        /// 阿里大鱼
        /// </summary>
        /// <param name="pSign"></param>
        /// <param name="pSMSContent"></param>
        /// <param name="pMobileNO"></param>
        /// <param name="pSmsTemplateCode"></param>
        /// <returns></returns>
        private static AlibabaAliqinFcSmsNumSendResponse AlidatySendMessage(string pSign, string pSMSContent, string pMobileNO, string pSmsTemplateCode)
        {
            string appKey = WebConfigurationManager.AppSettings["AlidayuAppKey"].ToString();
            string secret = WebConfigurationManager.AppSettings["AlidayuSecret"].ToString();
            string url = WebConfigurationManager.AppSettings["AlidayuURL"].ToString();
            //短信发送日志
            SMSSendBLL bll = new SMSSendBLL(new JIT.Utility.BasicUserInfo());

            ITopClient client = new DefaultTopClient(url, appKey, secret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.SmsType = "normal";
            req.SmsFreeSignName = pSign; //签名
            req.SmsParam = pSMSContent;
            req.RecNum = pMobileNO; //手机号
            req.SmsTemplateCode = pSmsTemplateCode;
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);

            string[] temp = pMobileNO.Split(',');
            foreach (var i in temp)
            {
                var entity = new SMSSendEntity()
                {
                    MobileNO = i,
                    Sign = pSign,
                };

                if (rsp.Result != null)
                {
                    entity.Status = 1;
                    entity.SendCount = (entity.SendCount ?? 0) + 1;
                    entity.RegularlySendTime = DateTime.Now;
                    var debug = new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, entity.MobileNO, rsp.Result.Msg, rsp.Result.ErrCode) };
                    Loggers.Debug(debug);
                }
                else
                {
                    entity.SendCount = (entity.SendCount ?? 0) + 1;
                    rsp.Result = new BizResult()
                    {
                        ErrCode = rsp.ErrCode,
                        Msg = rsp.SubErrMsg,
                        Success = false,
                    };
                    var debug = new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, entity.MobileNO, rsp.SubErrCode, rsp.ErrCode) };
                    Loggers.Debug(debug);
                }
                //保存数据库
                bll.Create(entity);
            }

            return rsp;
        }



        
        private static Result GetResult(string pResult)
        {
            var dic = Util.GetDic(pResult);
            Result result = new Result();
            result.Load(dic);
            return result;
        }

        private static string Fmt(this string value, params object[] para)
        {
            return string.Format(value, para);
        }
    }
}