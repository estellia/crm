using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using ChainClouds.Weixin.MP.MessageHandlers;
using ChainClouds.Weixin.MP.MvcExtension;
using ChainClouds.Weixin.MP.Web.CommonService.CustomMessageHandler;
using ChainClouds.Weixin.MP.Web.CommonService.MessageHandlers.OpenMessageHandler;
using ChainClouds.Weixin.MP.Web.CommonService.OpenTicket;
using ChainClouds.Weixin.Open;
using ChainClouds.Weixin.Open.MessageHandlers;
using ChainClouds.Weixin.MP.Web.CommonService.ThirdPartyMessageHandlers;
using ChainClouds.Weixin.Open.ComponentAPIs;
using ChainClouds.Weixin.Open.Entities.Request;
using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.Utility.Log;

namespace ChainClouds.Weixin.MP.Web.Controllers
{
    /// <summary>
    /// 第三方开放平台演示
    /// </summary>
    public class OpenController : Controller
    {
        private string component_AppId = WebConfigurationManager.AppSettings["Component_Appid"];
        private string component_Secret = WebConfigurationManager.AppSettings["Component_Secret"];
        private string component_Token = WebConfigurationManager.AppSettings["Component_Token"];
        private string component_EncodingAESKey = WebConfigurationManager.AppSettings["Component_EncodingAESKey"];

        /// <summary>
        /// 发起授权页的体验URL
        /// </summary>
        /// <returns></returns>
        public ActionResult OAuth(string Id)
        {
            //获取预授权码
            var preAuthCode = ComponentContainer.TryGetPreAuthCode(component_AppId, component_Secret);
            var openOAuthUrl = !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["openOAuthUrl"]) ? WebConfigurationManager.AppSettings["openOAuthUrl"] : "http://open.chainclouds.com";
            var callbackUrl = openOAuthUrl + "/OpenOAuth/OpenOAuthCallback?clientid=" + Id;//成功回调地址
            var url = ComponentApi.GetComponentLoginPageUrl(component_AppId, preAuthCode, callbackUrl);
            return Redirect(url);
        }

        /// <summary>
        /// 授权事件接收URL
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Notice(PostModel postModel)
        {
            //var logPath = Server.MapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            //if (!Directory.Exists(logPath))
            //{
            //    Directory.CreateDirectory(logPath);
            //}

            //using (TextWriter tw = new StreamWriter(Path.Combine(logPath, string.Format("{0}_RequestStream.txt", DateTime.Now.Ticks))))
            //{
            //    using (var sr = new StreamReader(Request.InputStream))
            //    {
            //        tw.WriteLine(sr.ReadToEnd());
            //        tw.Flush();
            //    }
            //}

            //Request.InputStream.Seek(0, SeekOrigin.Begin);

            try
            {
                postModel.Token = component_Token;
                postModel.EncodingAESKey = component_EncodingAESKey;//根据自己后台的设置保持一致
                postModel.AppId = component_AppId;//根据自己后台的设置保持一致

                var messageHandler = new CustomThirdPartyMessageHandler(Request.InputStream, postModel);//初始化
                //注意：再进行“全网发布”时使用上面的CustomThirdPartyMessageHandler，发布完成之后使用正常的自定义的MessageHandler，例如下面一行。
                //var messageHandler = new CommonService.CustomMessageHandler.CustomMessageHandler(Request.InputStream,
                //    postModel, 10);

                //记录RequestMessage日志（可选）
                //messageHandler.EcryptRequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request.txt", DateTime.Now.Ticks)));
                //messageHandler.RequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.AppId)));

                messageHandler.Execute();//执行

                //记录ResponseMessage日志（可选）
                //using (TextWriter tw = new StreamWriter(Path.Combine(logPath, string.Format("{0}_Response_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.AppId))))
                //{
                //    tw.WriteLine(messageHandler.ResponseMessageText);
                //    tw.Flush();
                //    tw.Close();
                //}

                return Content(messageHandler.ResponseMessageText);
            }
            catch (Exception ex)
            {
                throw;
                return Content("error：" + ex.Message);
            }
        }

        /// <summary>
        /// 微信服务器会不间断推送最新的Ticket（10分钟一次），需要在此方法中更新缓存
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Callback(Entities.Request.PostModel postModel,string clientId)
        {
            //此处的URL格式类型为：http://open.chainclouds.com/Open/Callback/$APPID$， 在RouteConfig中进行了配置，你也可以用自己的格式，只要和开放平台设置的一致。

            //处理微信普通消息，可以直接使用公众号的MessageHandler。此处的URL也可以直接填写公众号普通的URL，如本Demo中的/Weixin访问地址。

            //var logPath = Server.MapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            //if (!Directory.Exists(logPath))
            //{
            //    Directory.CreateDirectory(logPath);
            //}

            postModel.Token = component_Token;
            postModel.EncodingAESKey = component_EncodingAESKey; //根据自己后台的设置保持一致
            postModel.AppId = component_AppId; //根据自己后台的设置保持一致

            var maxRecordCount = 10;
            MessageHandler<CustomMessageContext> messageHandler = null;

            try
            {
                var checkPublish = true; //是否在“全网发布”阶段
                if (checkPublish)
                {
                    messageHandler = new OpenCheckMessageHandler(Request.InputStream, postModel, 10);
                }
                else
                {
                    messageHandler = new CustomMessageHandler(Request.InputStream, postModel, maxRecordCount);
                }

                //messageHandler.RequestDocument.Save(Path.Combine(logPath,
                //    string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.FromUserName)));

                //调用老接口处理微信消息
                var logsession = BaseService.GetWeixinLoggingSession(messageHandler.RequestMessage.ToUserName);
                messageHandler.CustomerId = logsession.ClientID;
                messageHandler.LoggingSession = logsession;
                var msgUrl = WebConfigurationManager.AppSettings["URL"] + "?" + this.Request.QueryString.ToString();
                var msgResult = Utils.GetRemoteData(msgUrl, "POST", messageHandler.EcryptRequestDocument.ToString());
                //var msgUrl = WebConfigurationManager.AppSettings["URL"];
                //var msgResult = Utils.GetRemoteData(msgUrl, "POST", messageHandler.RequestDocument.ToString());
                //if (msgResult.LastIndexOf("<xml>") > 0)
                //{
                //    msgResult = msgResult.Substring(0, msgResult.LastIndexOf("<xml>"));
                //    //var tempMsg = msgResult.Substring(0, msgResult.LastIndexOf("<xml>"));
                //}

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "msgResult 返回:" + msgResult
                });

                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(msgResult);
                //XDocument xdoc = XDocument.Parse(doc.ToString());
                //ChainClouds.Weixin.MP.Entities.ResponseMessageBase.CreateResponseMessage(msgResult);
                //messageHandler.TextResponseMessage = msgResult;
                //if (!string.IsNullOrEmpty(msgResult))
                return Content(msgResult);
                //else
                //    messageHandler.Execute(); //执行


                //if (messageHandler.ResponseDocument != null)
                //{
                //    messageHandler.ResponseDocument.Save(Path.Combine(logPath,
                //        string.Format("{0}_Response_{1}.txt", DateTime.Now.Ticks,
                //            messageHandler.RequestMessage.FromUserName)));
                //}
                //return new FixWeixinBugWeixinResult(messageHandler);
            }
            catch (Exception ex)
            {
                using (
                    TextWriter tw =
                        new StreamWriter(Server.MapPath("~/App_Data/Open/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }

                    if (ex.InnerException != null)
                    {
                        tw.WriteLine("========= InnerException =========");
                        tw.WriteLine(ex.InnerException.Message);
                        tw.WriteLine(ex.InnerException.Source);
                        tw.WriteLine(ex.InnerException.StackTrace);
                    }

                    tw.Flush();
                    tw.Close();
                    return Content("");
                }
            }
        }
    }
}
