/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
  
    文件名：Passport.cs
    文件功能描述：P2P通行证
    
    
    创建标识：ChainClouds - 20150319
----------------------------------------------------------------*/

using System;

namespace ChainClouds.Weixin.MP.AppStore
{
    /// <summary>
    /// P2P通行证
    /// </summary>
    public class Passport : IAppResultData
    {
        public string Token { get; set; }
        public string AppKey { get; set; }
        public string Secret { get; set; }
        /// <summary>
        /// API常规URL
        /// </summary>
        public string ApiUrl { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 供连锁掌柜服务器记录唯一开发人员ID
        /// </summary>
        public int DeveloperId { get; set; }
    }
}
