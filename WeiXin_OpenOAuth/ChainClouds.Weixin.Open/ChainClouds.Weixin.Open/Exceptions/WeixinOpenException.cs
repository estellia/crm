/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：WeixinOpenException.cs
    文件功能描述：微信开放平台异常处理类
    
    
    创建标识：ChainClouds - 20151004

----------------------------------------------------------------*/

using System;
using ChainClouds.Weixin.Exceptions;
using ChainClouds.Weixin.Open.CommonAPIs;
using ChainClouds.Weixin.Open.ComponentAPIs;

namespace ChainClouds.Weixin.Open.Exceptions
{
    public class WeixinOpenException : WeixinException
    {
        public ComponentBag ComponentBag { get; set; }

        public WeixinOpenException(string message, ComponentBag componentBag = null, Exception inner=null)
            : base(message, inner)
        {
            ComponentBag = ComponentBag;
        }
    }
}
