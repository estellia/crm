/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：ComponentContainer.cs
    文件功能描述：通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    
    
    创建标识：ChainClouds - 20150430

    修改标识：ChainClouds - 20151004
    修改描述：v1.4.1 改名为ComponentContainer.cs，合并多个ComponentApp相关容器

    修改标识：ChainClouds - 20151005
    修改描述：v1.4.3 添加ComponentVerifyTicketExpireTime及自动更新机制

----------------------------------------------------------------*/

using System;
using ChainClouds.Weixin.Containers;
using ChainClouds.Weixin.Open.CommonAPIs;
using ChainClouds.Weixin.Open.Entities;
using ChainClouds.Weixin.Open.Exceptions;
using System.Web.Configuration;
using System.Linq;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web;
using JIT.Utility.DataAccess.Query;
using System.Collections.Generic;

namespace ChainClouds.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 第三方APP信息包
    /// </summary>
    public class ComponentBag : BaseContainerBag
    {
        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }
        /// <summary>
        /// 第三方平台AppSecret
        /// </summary>
        public string ComponentAppSecret { get; set; }

        /// <summary>
        /// 第三方平台ComponentVerifyTicket（每隔10分钟微信会主动推送到服务器，IP必须在白名单内）
        /// </summary>
        public string ComponentVerifyTicket { get; set; }
        /// <summary>
        /// 第三方平台ComponentVerifyTicket过期时间（实际上过期之后仍然可以使用一段时间）
        /// </summary>
        public DateTime ComponentVerifyTicketExpireTime { get; set; }

        /// <summary>
        /// ComponentAccessTokenResult
        /// </summary>
        public ComponentAccessTokenResult ComponentAccessTokenResult { get; set; }
        /// <summary>
        /// ComponentAccessToken过期时间
        /// </summary>
        public DateTime ComponentAccessTokenExpireTime { get; set; }


        /// <summary>
        /// PreAuthCodeResult 预授权码结果
        /// </summary>
        public PreAuthCodeResult PreAuthCodeResult { get; set; }
        /// <summary>
        /// 预授权码过期时间
        /// </summary>
        public DateTime PreAuthCodeExpireTime { get; set; }

        /// <summary>
        /// AuthorizerAccessToken
        /// </summary>
        public string AuthorizerAccessToken { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        /// <summary>
        /// ComponentBag
        /// </summary>
        public ComponentBag()
        {
            ComponentAccessTokenResult = new ComponentAccessTokenResult();
            ComponentAccessTokenExpireTime = DateTime.MinValue;

            PreAuthCodeResult = new PreAuthCodeResult();
            PreAuthCodeExpireTime = DateTime.MinValue;
        }

    }

    /// <summary>
    /// 通用接口ComponentAccessToken容器，用于自动管理ComponentAccessToken，如果过期会重新获取
    /// </summary>
    public class ComponentContainer : BaseContainer<ComponentBag>
    {
        private const string UN_REGISTER_ALERT = "此appId尚未注册，ComponentContainer.Register完成注册（全局执行一次即可）！";
        /// <summary>
        /// ComponentVerifyTicket服务器推送更新时间（分钟）
        /// </summary>
        private const int COMPONENT_VERIFY_TICKET_UPDATE_MINUTES = 10;

        /// <summary>
        /// 检查AppId是否已经注册，如果没有，则创建
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        private static void TryRegister(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId) || getNewToken)
            {
                Register(componentAppId, componentAppSecret, null, null, null);
            }
        }

        /// <summary>
        /// 获取ComponentVerifyTicket的方法
        /// </summary>
        public static Func<string, string> GetComponentVerifyTicketFunc = null;

        /// <summary>
        /// 从数据库中获取已存的AuthorizerAccessToken的方法
        /// </summary>
        public static Func<string, string> GetAuthorizerRefreshTokenFunc = null;

        /// <summary>
        /// AuthorizerAccessToken更新后的回调
        /// </summary>
        public static Action<string,RefreshAuthorizerTokenResult> AuthorizerTokenRefreshedFunc = null;


        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        public static void Register(string componentAppId, string componentAppSecret, Func<string, string> getComponentVerifyTicketFunc, Func<string, string> getAuthorizerRefreshTokenFunc, Action<string,RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc)
        {
            if (GetComponentVerifyTicketFunc == null)
            {
                GetComponentVerifyTicketFunc = getComponentVerifyTicketFunc;
                GetAuthorizerRefreshTokenFunc = getAuthorizerRefreshTokenFunc;
                AuthorizerTokenRefreshedFunc = authorizerTokenRefreshedFunc;
            }

            Update(componentAppId, new ComponentBag()
            {
                ComponentAppId = componentAppId,
                ComponentAppSecret = componentAppSecret,
            });
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <returns></returns>
        public new static bool CheckRegistered(string componentAppId)
        {
            return ItemCollection.ContainsKey(componentAppId);
        }


        #region component_verify_ticket

        /// <summary>
        /// 获取ComponentVerifyTicket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken"></param>
        /// <returns>如果不存在，则返回null</returns>
        public static string TryGetComponentVerifyTicket(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var bag = TryGetItem(componentAppId);
            var componentVerifyTicket = bag.ComponentVerifyTicket;
            if (getNewToken || componentVerifyTicket == default(string) || bag.ComponentVerifyTicketExpireTime < DateTime.Now)
            {
                if (GetComponentVerifyTicketFunc == null)
                {
                    throw new WeixinOpenException("GetComponentVerifyTicketFunc必须在注册时提供！", TryGetItem(componentAppId));
                }
                componentVerifyTicket = GetComponentVerifyTicketFunc(componentAppId); //获取最新的componentVerifyTicket
                bag.ComponentVerifyTicket = componentVerifyTicket;
                bag.ComponentVerifyTicketExpireTime = DateTime.Now.AddMinutes(COMPONENT_VERIFY_TICKET_UPDATE_MINUTES);
            }
            return componentVerifyTicket;
        }

        /// <summary>
        /// 更新ComponentVerifyTicket信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        public static void UpdateComponentVerifyTicket(string componentAppId, string componentVerifyTicket)
        {
            Update(componentAppId, bag =>
            {
                bag.ComponentVerifyTicket = componentVerifyTicket;
            });
        }

        #endregion

        #region component_access_token

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetComponentAccessToken(string componentAppId, string componentAppSecret, string componentVerifyTicket = null, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetComponentAccessToken(componentAppId, componentVerifyTicket);
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetComponentAccessToken(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            return GetComponentAccessTokenResult(componentAppId, componentVerifyTicket, getNewToken).component_access_token;
        }

        /// <summary>
        /// 获取可用AccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket">如果为null则自动获取</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetComponentAccessTokenResult(string componentAppId, string componentVerifyTicket = null, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = (ComponentBag)ItemCollection[componentAppId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ComponentAccessTokenExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    componentVerifyTicket = componentVerifyTicket ?? TryGetComponentVerifyTicket(componentAppId);

                    var componentAccessTokenResult = ComponentApi.GetComponentAccessToken(accessTokenBag.ComponentAppId, accessTokenBag.ComponentAppSecret, componentVerifyTicket);

                    accessTokenBag.ComponentAccessTokenResult = componentAccessTokenResult;
                    accessTokenBag.ComponentAccessTokenExpireTime = DateTime.Now.AddSeconds(componentAccessTokenResult.expires_in);
                }
            }
            return accessTokenBag.ComponentAccessTokenResult;
        }
        #endregion

        #region pre_auth_code

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetPreAuthCode(string componentAppId, string componentAppSecret, bool getNewToken = false)
        {
            TryRegister(componentAppId, componentAppSecret, getNewToken);
            return GetPreAuthCode(componentAppId);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetPreAuthCode(string componentAppId, bool getNewToken = false)
        {
            return GetPreAuthCodeResult(componentAppId, getNewToken).pre_auth_code;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCodeResult(string componentAppId, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = (ComponentBag)ItemCollection[componentAppId];
            lock (componentBag.Lock)
            {
                if (getNewToken || componentBag.PreAuthCodeExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var componentVerifyTicket = TryGetComponentVerifyTicket(componentAppId);

                    var accessToken = TryGetComponentAccessToken(componentAppId, componentBag.ComponentAppSecret, componentVerifyTicket);

                    var preAuthCodeResult = ComponentApi.GetPreAuthCode(componentBag.ComponentAppId, accessToken);
                    componentBag.PreAuthCodeExpireTime = DateTime.Now.AddSeconds(preAuthCodeResult.expires_in);


                    componentBag.PreAuthCodeResult = preAuthCodeResult;

                    ////TODO:这里有出现expires_in=0的情况，导致始终处于过期状态（也可能是因为参数过期等原因没有返回正确的数据，待观察）
                    //var expiresIn = componentBag.PreAuthCodeResult.expires_in > 0
                    //    ? componentBag.PreAuthCodeResult.expires_in
                    //    : 60 * 20;//默认为20分钟
                    //componentBag.PreAuthCodeExpireTime = DateTime.Now.AddSeconds(expiresIn);
                }
            }
            return componentBag.PreAuthCodeResult;
        }
        #endregion

        #region api_query_auth

        /// <summary>
        /// 获取QueryAuthResult（此方法每次都会发出请求，不缓存）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="updateToAuthorizerContanier">是否将Authorization更新到AuthorizerContanier</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        /// <exception cref="WeixinOpenException"></exception>
        public static QueryAuthResult GetQueryAuthResult(string componentAppId, string authorizationCode, bool updateToAuthorizerContanier = true, bool getNewToken = false)
        {
            if (!CheckRegistered(componentAppId))
            {
                throw new WeixinOpenException(UN_REGISTER_ALERT);
            }

            var componentBag = (ComponentBag)ItemCollection[componentAppId];
            lock (componentBag.Lock)
            {
                var accessToken = TryGetComponentAccessToken(componentAppId, componentBag.ComponentAppSecret);
                var queryAuthResult = ComponentApi.QueryAuth(accessToken, componentAppId, authorizationCode);

                if (updateToAuthorizerContanier)
                {
                    //更新到AuthorizerContainer
                    AuthorizerContainer.TryUpdateAuthorizationInfo(componentAppId, queryAuthResult.authorization_info.authorizer_appid, queryAuthResult.authorization_info);
                }

                return queryAuthResult;
            }
        }
        
        /// <summary>
        /// 取消授权
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static bool UnauthWechatApp(string appid,string clientid)
        {
            var clientId = clientid == null ? "eb17cc2569c74ab7888b1f403972d37d" : clientid;//测试用
            var loggingSessionInfo = Default.GetBSLoggingSession(clientId, "open");
            var waiBll = new WApplicationInterfaceBLL(loggingSessionInfo);            
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


                //var wmenuBll = new WMenuBLL(loggingSessionInfo);
                ////查询参数
                //List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                //complexCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = waiEntity.WeiXinID });
                //var tempList = wmenuBll.PagedQuery(complexCondition.ToArray(), null, 10, 1);
                //if (tempList.Entities.Length > 0)
                //{
                //    foreach (var item in tempList.Entities)
                //    {
                //        wmenuBll.Delete(item);
                //    }
                //}

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

        #endregion
    }
}
