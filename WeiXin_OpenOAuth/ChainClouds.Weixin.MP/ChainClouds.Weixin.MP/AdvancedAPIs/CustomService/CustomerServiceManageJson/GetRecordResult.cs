/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：GetRecordResult.cs
    文件功能描述：聊天记录结果
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using ChainClouds.Weixin.Entities;

namespace ChainClouds.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 聊天记录结果
    /// </summary>
    public class GetRecordResult : WxJsonResult
    {
        /// <summary>
        /// 官方文档暂没有说明
        /// </summary>
        public int retcode { get; set; }
        public List<RecordJson> recordlist { get; set; }
    }
}
