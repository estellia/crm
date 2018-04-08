/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：IRequestMessageEventKey.cs
    文件功能描述：具有EventKey属性的RequestMessage接口
    
    
    创建标识：ChainClouds - 20150313
    
    修改标识：ChainClouds - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.QY.Entities
{
    /// <summary>
    /// 具有EventKey属性的RequestMessage接口
    /// </summary>
    public interface IRequestMessageEventKey
    {
        string EventKey { get; set; }
    }
}
