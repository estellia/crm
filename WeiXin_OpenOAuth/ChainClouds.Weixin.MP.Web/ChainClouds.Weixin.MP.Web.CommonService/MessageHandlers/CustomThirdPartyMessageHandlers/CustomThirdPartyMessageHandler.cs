using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainClouds.Weixin.Open;
using ChainClouds.Weixin.Open.MessageHandlers;
using System.IO;
using ChainClouds.Weixin.MP.Web.CommonService.Utilities;
using ChainClouds.Weixin.Open.Entities.Request;
using JIT.CPOS.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Web.Configuration;
using JIT.Utility.DataAccess.Query;

namespace ChainClouds.Weixin.MP.Web.CommonService.ThirdPartyMessageHandlers
{
    public class CustomThirdPartyMessageHandler : ThirdPartyMessageHandler
    {
        public CustomThirdPartyMessageHandler(Stream inputStream, PostModel encryptPostModel)
            : base(inputStream, encryptPostModel)
        { }

        public override string OnComponentVerifyTicketRequest(RequestMessageComponentVerifyTicket requestMessage)
        {
            var openTicketPath = Server.GetMapPath("~/App_Data/OpenTicket");
            if (!Directory.Exists(openTicketPath))
            {
                Directory.CreateDirectory(openTicketPath);
            }

            //RequestDocument.Save(Path.Combine(openTicketPath, string.Format("{0}_Doc.txt", DateTime.Now.Ticks)));

            //记录ComponentVerifyTicket（也可以存入数据库或其他可以持久化的地方）
            using (TextWriter tw = new StreamWriter(Path.Combine(openTicketPath, string.Format("{0}.txt", RequestMessage.AppId))))
            {
                tw.Write(requestMessage.ComponentVerifyTicket);
                tw.Flush();
                tw.Close();
            }

            return base.OnComponentVerifyTicketRequest(requestMessage);
        }

        public override string OnUnauthorizedRequest(RequestMessageUnauthorized requestMessage)
        {
            //取消授权
            UnauthWechatApp(requestMessage.AuthorizerAppid);
            return base.OnUnauthorizedRequest(requestMessage);
        }
        /// <summary>
        /// 取消授权
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static bool UnauthWechatApp(string appid)
        {
            var clientId = "eb17cc2569c74ab7888b1f403972d37d";//测试用
            var loggingSessionInfo = Default.GetBSLoggingSession(clientId, "open");
            var waiBll = new WApplicationInterfaceBLL(loggingSessionInfo);
            var wmenuBll = new WMenuBLL(loggingSessionInfo);
            var userInfo = loggingSessionInfo;
            userInfo.CurrentLoggingManager.Connection_String = WebConfigurationManager.AppSettings["APConn"];
            var customerWxMappingBll = new TCustomerWeiXinMappingBLL(userInfo);
            var customerId = customerWxMappingBll.GetCustomerIdByAppId(appid);
            var waiEntitys = new WApplicationInterfaceEntity[] { };
            var waiEntity = new WApplicationInterfaceEntity();
            var component_Appid = WebConfigurationManager.AppSettings["Component_Appid"];
            waiEntitys = waiBll.QueryByEntity(new WApplicationInterfaceEntity { AppID = appid, OpenOAuthAppid = component_Appid, CustomerId = customerId, IsDelete = 0 }, null);
            if (waiEntitys != null && waiEntitys.Length > 0)
            {
                waiEntity = waiEntitys.FirstOrDefault();
                //waiEntity.OpenOAuthAppid = null;//置空
                //waiBll.Update(waiEntity);
                waiBll.Delete(waiEntity);


                
                //查询参数
                List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                complexCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = waiEntity.WeiXinID });
                var tempList = wmenuBll.PagedQuery(complexCondition.ToArray(), null, 10, 1);
                if (tempList.Entities.Length > 0)
                {
                    foreach (var item in tempList.Entities)
                    {
                        wmenuBll.Delete(item);
                    }
                }

                //var wmenuEntitys = wmenuBll.QueryByEntity(new WMenuEntity { WeiXinID = waiEntity.WeiXinID }, null);
                //if (wmenuEntitys != null && wmenuEntitys.Length > 0)
                //{
                //    foreach (var item in wmenuEntitys)
                //    {
                //        wmenuBll.Delete(item);
                //    }
                //}
            }
            return true;
        }
    }
}
