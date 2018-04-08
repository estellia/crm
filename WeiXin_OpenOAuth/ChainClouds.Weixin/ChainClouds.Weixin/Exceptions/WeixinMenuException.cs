/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：WeixinException.cs
    文件功能描述：微信菜单异常处理类
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace ChainClouds.Weixin.Exceptions
{
    public class WeixinMenuException : WeixinException
    {
        public WeixinMenuException(string message)
            : base(message, null)
        {
        }

        public WeixinMenuException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
