using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ChainClouds.Weixin.Exceptions;
using ChainClouds.Weixin.MP.Web.CommonService.OpenTicket;
using ChainClouds.Weixin.Open.CommonAPIs;
using ChainClouds.Weixin.Open.ComponentAPIs;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web;
using JIT.Utility.DataAccess.Query;

namespace ChainClouds.Weixin.MP.Web.Controllers
{

    public class OpenOAuthController : Controller
    {
        private string component_AppId = WebConfigurationManager.AppSettings["Component_Appid"];
        private string component_Secret = WebConfigurationManager.AppSettings["Component_Secret"];
        //private static string ComponentAccessToken = null;//需要授权获取，腾讯服务器会主动推送
        

        #region 开放平台入口及回调

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="auth_code"></param>
        /// <param name="expires_in"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult OpenOAuthCallback(string auth_code, int expires_in, string appId)
        {
            try
            {

                #region 直接调用API

                //string openTicket = OpenTicketHelper.GetOpenTicket(component_AppId);
                //var component_access_token = Open.ComponentAPIs.ComponentApi.GetComponentAccessToken(component_AppId, component_Secret, openTicket).component_access_token;
                //ComponentAccessToken = component_access_token;
                //var oauthResult = Open.ComponentAPIs.ComponentApi.QueryAuth(component_access_token, component_AppId, auth_code);

                ////TODO:储存oauthResult.authorization_info
                //var authInfoResult = Open.ComponentAPIs.ComponentApi.GetAuthorizerInfo(component_access_token, component_AppId,
                //     oauthResult.authorization_info.authorizer_appid);

                #endregion

                #region 使用ComponentContainer

                //获取OAuth授权结果
                QueryAuthResult queryAuthResult;
                try
                {
                    queryAuthResult = ComponentContainer.GetQueryAuthResult(component_AppId, auth_code);
                }
                catch (Exception ex)
                {
                    throw new Exception("QueryAuthResult：" + ex.Message);
                }
                #endregion

                var authorizerInfoResult = AuthorizerContainer.GetAuthorizerInfoResult(component_AppId,
                    queryAuthResult.authorization_info.authorizer_appid);

                ViewData["QueryAuthorizationInfo"] = queryAuthResult.authorization_info;
                ViewData["GetAuthorizerInfoResult"] = authorizerInfoResult.authorizer_info;
                ViewData["ClientId"] = Request.QueryString["clientId"];
                var clientId = Request.QueryString["clientId"];

                //保存到微信信息表
                SetWechatApp(queryAuthResult, authorizerInfoResult, clientId);

                return View();
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 公众号授权页入口
        /// </summary>
        /// <returns></returns>
        public ActionResult JumpToMpOAuth(string clientId)
        {
            try
            {
                if (!string.IsNullOrEmpty(clientId))
                {
                    //WeixinInfo.clientId = clientId ?? WeixinInfo.clientId;
                    var loggingSessionInfo = Default.GetBSLoggingSession(clientId, "open");
                    if (string.IsNullOrEmpty(loggingSessionInfo.Conn))
                    {
                        return Content("商户不存在");
                    }

                    ViewBag.ClientId = clientId;
                    //var callbackUrl = Config.ServeAuthRedirectUri;//成功回调地址
                    //var url = WechatApp.GetComponentLoginPageUrl(component_AppId, WeixinInfo.pre_auth_codeInfo, callbackUrl);
                    //ViewBag.ComponentloginUrl = url;

                    //获取预授权码
                    //var preAuthCode = ComponentContainer.TryGetPreAuthCode(component_AppId, component_Secret);

                    //var callbackUrl = "http://open.chainclouds.com/OpenOAuth/OpenOAuthCallback";//成功回调地址
                    //var url = ComponentApi.GetComponentLoginPageUrl(component_AppId, preAuthCode, callbackUrl);
                    //return Redirect(url);

                    //return Redirect(url);
                    return View();
                }
                else
                {
                    //return Content("商户不存在");
                }
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ex.StackTrace.ToString());
            }
        }

        #endregion

        #region 微信用户授权相关

        public ActionResult Index(string appId)
        {
            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] = Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(appId, component_AppId, "http://open.chainclouds.com/OpenOAuth/UserInfoCallback", "ZmindBob", new[] { Open.OAuthScope.snsapi_userinfo, Open.OAuthScope.snsapi_base });
            ViewData["UrlBase"] = Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(appId, component_AppId, "http://open.chainclouds.com/OpenOAuth/BaseCallback", "ZmindBob", new[] { Open.OAuthScope.snsapi_userinfo, Open.OAuthScope.snsapi_base });
            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string state, string appId)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "ZmindBob")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            Open.OAuthAPIs.OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                var componentAccessToken = ComponentContainer.GetComponentAccessToken(component_AppId);
                result = Open.OAuthAPIs.OAuthApi.GetAccessToken(appId, component_AppId, componentAccessToken, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                Open.OAuthAPIs.OAuthUserInfo userInfo = Open.OAuthAPIs.OAuthApi.GetUserInfo(result.access_token, result.openid);
                return View(userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string state, string appId)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "ZmindBob")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var componentAccessToken = ComponentContainer.TryGetComponentAccessToken(component_AppId, component_Secret);

            var result = Open.OAuthAPIs.OAuthApi.GetAccessToken(appId, component_AppId, componentAccessToken, code);//TODO:使用Container

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            Open.OAuthAPIs.OAuthUserInfo userInfo = null;
            try
            {
                //已关注，可以得到详细信息
                userInfo = Open.OAuthAPIs.OAuthApi.GetUserInfo(result.access_token, result.openid);
                ViewData["ByBase"] = true;
                return View("UserInfoCallback", userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                //未关注，只能授权，无法得到详细信息
                //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                return Content("用户已授权，授权Token：" + result);
            }
        }

        #endregion

        #region 授权信息

        public ActionResult GetAuthorizerInfoResult(string authorizerId)
        {
            var getAuthorizerInfoResult = AuthorizerContainer.GetAuthorizerInfoResult(component_AppId, authorizerId);
            return Json(getAuthorizerInfoResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefreshAuthorizerAccessToken(string authorizerId)
        {
            var componentAccessToken = ComponentContainer.GetComponentAccessToken(component_AppId);
            var authorizationInfo = AuthorizerContainer.GetAuthorizationInfo(component_AppId, authorizerId);
            if (authorizationInfo == null)
            {
                return Content("授权信息读取失败！");
            }

            var refreshToken = authorizationInfo.authorizer_refresh_token;
            var result = AuthorizerContainer.RefreshAuthorizerToken(componentAccessToken, component_AppId, authorizerId,
                refreshToken);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        ///获取COMPONENT_ACCESS_TOKEN
        public ActionResult GetComponentAccessToken()
        {
            var componentAccessToken = ComponentContainer.GetComponentAccessToken(component_AppId);
            //var authorizationInfo = AuthorizerContainer.GetAuthorizationInfo(component_AppId, authorizerId);
            //if (authorizationInfo == null)
            //{
            //    return Content("授权信息读取失败！");
            //}

            //var refreshToken = authorizationInfo.authorizer_refresh_token;
            //var result = AuthorizerContainer.RefreshAuthorizerToken(componentAccessToken, component_AppId, authorizerId,
            //    refreshToken);
            return Json(componentAccessToken, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存微信信息
        /// </summary>
        /// <param name="pwai"></param>
        /// <param name="paui"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static bool SetWechatApp(QueryAuthResult pwai, GetAuthorizerInfoResult paui, string clientId)
        {
            #region 保存到微信信息表里
            //var clientId = "eb17cc2569c74ab7888b1f403972d37d";//测试用
            var loggingSessionInfo = Default.GetBSLoggingSession(clientId, "open");
            var waiBll = new WApplicationInterfaceBLL(loggingSessionInfo);
            var waiEntitys = new WApplicationInterfaceEntity[] { };
            var waiEntity = new WApplicationInterfaceEntity();

            //去除之前授权的记录
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "AppID", Value = pwai.authorization_info.authorizer_appid });
            complexCondition.Add(new DirectCondition(" CustomerId != '" + clientId + "'"));
            complexCondition.Add(new EqualsCondition() { FieldName = "OpenOAuthAppid", Value = WebConfigurationManager.AppSettings["Component_Appid"] });
            var tempList = waiBll.PagedQuery(complexCondition.ToArray(), null, 10, 1);
            if (tempList.Entities.Length > 0)
            {
                foreach (var item in tempList.Entities)
                {
                    waiBll.Delete(item);
                }
            }

            var prevWaiEntitys = waiBll.QueryByEntity(new WApplicationInterfaceEntity { AppID = pwai.authorization_info.authorizer_appid, CustomerId = clientId, IsDelete = 0, OpenOAuthAppid = WebConfigurationManager.AppSettings["Component_Appid"] }, null);


            waiEntitys = waiBll.QueryByEntity(new WApplicationInterfaceEntity { AppID = pwai.authorization_info.authorizer_appid, CustomerId = clientId, IsDelete = 0 }, null);
            if (waiEntitys != null && waiEntitys.Length > 0)
            {
                waiEntity = waiEntitys.FirstOrDefault();
                waiEntity.URL = Config.URL;
                waiEntity.Token = "jitmarketing";//老代码写死token//Config.ServerToken;
                //waiEntity.Token = "zmindclouds";//老代码写死token//Config.ServerToken,
                //waiEntity.AppID = "wx691c2f2bbac04b4b";
                //waiEntity.AppSecret = "0c79e1fa963cd80cc0be99b20a18faeb";
                //waiEntity.PrevEncodingAESKey = "F3Rd5xvdCqUsL5FFwhS1vSIqTRFoNpRcWhyGrOjQhAK";
                //waiEntity.CurrentEncodingAESKey = "F3Rd5xvdCqUsL5FFwhS1vSIqTRFoNpRcWhyGrOjQhAK";
                waiEntity.ServerIP = Config.ServerIP;
                waiEntity.AuthUrl = Config.AuthUrl;
                waiEntity.RequestToken = pwai != null ? pwai.authorization_info.authorizer_access_token : string.Empty;
                //waiEntity.RefreshToken = pwai != null ? pwai.authorization_info.authorizer_refresh_token : string.Empty,//刷新token
                waiEntity.ExpirationTime = DateTime.Now.AddSeconds(7000);//默认7200，提前200秒过期
                waiEntity.IsMoreCS = 1;
                waiEntity.OpenOAuthAppid = WebConfigurationManager.AppSettings["Component_Appid"]; 
                waiBll.Update(waiEntity);
                //提交管理平台
                waiBll.setCposApMapping(waiEntity);
            }
            else
            {
                if (pwai != null && paui != null)
                {

                }
                var weixinTypeId = "3";//服务号
                switch (paui.authorizer_info.service_type_info.id.ToString())
                {
                    case "0":
                        weixinTypeId = "1";//订阅号
                        break;
                    case "1":
                        weixinTypeId = "2";//订阅号
                        break;
                }
                waiEntity = new WApplicationInterfaceEntity()
                {
                    ApplicationId = Guid.NewGuid().ToString("N"),
                    WeiXinName = paui != null ? paui.authorizer_info.nick_name : string.Empty,
                    WeiXinID = paui != null ? paui.authorizer_info.user_name : string.Empty,
                    URL = Config.URL,
                    Token = "jitmarketing",//老代码写死token//Config.ServerToken,
                    AppID = pwai != null ? pwai.authorization_info.authorizer_appid : string.Empty,
                    AppSecret = string.Empty,//空

                    //Token = "zmindclouds",//老代码写死token//Config.ServerToken,
                    //AppID = "wx691c2f2bbac04b4b",
                    //AppSecret = "0c79e1fa963cd80cc0be99b20a18faeb",//空
                    //PrevEncodingAESKey = "F3Rd5xvdCqUsL5FFwhS1vSIqTRFoNpRcWhyGrOjQhAK",
                    //CurrentEncodingAESKey = "F3Rd5xvdCqUsL5FFwhS1vSIqTRFoNpRcWhyGrOjQhAK",

                    ServerIP = Config.ServerIP,
                    WeiXinTypeId = weixinTypeId,// paui != null ? paui.authorizer_info.service_type_info.id.ToString() : "3",
                    AuthUrl = Config.AuthUrl,
                    RequestToken = pwai != null ? pwai.authorization_info.authorizer_access_token : string.Empty,
                    //RefreshToken = pwai != null ? pwai.authorization_info.authorizer_refresh_token : string.Empty,//刷新token
                    ExpirationTime = DateTime.Now.AddSeconds(7000),//默认7200，提前200秒过期
                    IsHeight = 1,//1=高级帐号
                    IsMoreCS = 1,
                    OpenOAuthAppid = WebConfigurationManager.AppSettings["Component_Appid"],// component_AppId;
                    CustomerId = clientId
                };
                waiBll.Create(waiEntity);

                //提交微信初级菜单
                waiBll.setCreateWXMenu(waiEntity);
                //提交管理平台
                waiBll.setCposApMapping(waiEntity);

            }

            #endregion

            return true;
        }
        #endregion
    }
}

