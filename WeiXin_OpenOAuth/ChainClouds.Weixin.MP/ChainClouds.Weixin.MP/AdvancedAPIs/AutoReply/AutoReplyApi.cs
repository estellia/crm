﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：AutoReplyApi.cs
    文件功能描述：获取自动回复规则接口
    
    
    创建标识：ChainClouds - 20150907
----------------------------------------------------------------*/

/*
    Api地址：http://mp.weixin.qq.com/wiki/7/7b5789bb1262fb866d01b4b40b0efecb.html
 */

using ChainClouds.Weixin.MP.AdvancedAPIs.AutoReply;
using ChainClouds.Weixin.MP.CommonAPIs;

namespace ChainClouds.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取自动回复规则
    /// </summary>
    public static class AutoReplyApi
    {
        /// <summary>
        /// 获取自动回复规则
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <returns></returns>
        public static GetCurrentAutoreplyInfoResult GetCurrentAutoreplyInfo(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token={0}";

                return CommonJsonSend.Send<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
    }
}