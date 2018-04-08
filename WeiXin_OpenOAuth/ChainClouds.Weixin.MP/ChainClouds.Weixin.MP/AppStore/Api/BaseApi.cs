﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
  
    文件名：BaseApi.cs
    文件功能描述：API调用类基类
    
    
    创建标识：ChainClouds - 20150319
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.MP.AppStore.Api
{
    /// <summary>
    /// API调用类基类
    /// </summary>
    public class BaseApi
    {
        protected Passport _passport;
        protected ApiConnection ApiConnection { get; set; }

        public BaseApi(Passport passport)
        {
            _passport = passport;
            ApiConnection = new ApiConnection(passport);
        }
    }
}
