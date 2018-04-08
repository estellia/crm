﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：JsApiTicketResult.cs
    文件功能描述：jsapi_ticket请求后的JSON返回格式
    
    
    创建标识：ChainClouds - 20150313
    
    修改标识：ChainClouds - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using ChainClouds.Weixin.Entities;

namespace ChainClouds.Weixin.QY.Entities
{
    /// <summary>
    /// jsapi_ticket请求后的JSON返回格式
    /// </summary>
    public class JsApiTicketResult : QyJsonResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
