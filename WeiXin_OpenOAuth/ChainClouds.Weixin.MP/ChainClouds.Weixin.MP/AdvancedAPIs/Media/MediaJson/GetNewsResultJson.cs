﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：GetNewsResultJson.cs
    文件功能描述：获取图文类型永久素材返回结果
    
    
    创建标识：ChainClouds - 20150324
----------------------------------------------------------------*/

using System.Collections.Generic;
using ChainClouds.Weixin.Entities;
using ChainClouds.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace ChainClouds.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 获取图文类型永久素材返回结果
    /// </summary>
    public class GetNewsResultJson : WxJsonResult
    {
        public List<ForeverNewsItem> news_item { get; set; }
    }

    public class ForeverNewsItem : NewsModel
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
    }
}
