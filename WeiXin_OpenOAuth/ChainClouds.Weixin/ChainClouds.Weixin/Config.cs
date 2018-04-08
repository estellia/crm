/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：Config.cs
    文件功能描述：全局设置
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Diagnostics;
using System.IO;
using System.Web.Configuration;

namespace ChainClouds.Weixin
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;

        private static bool _isDebug = false;

        /// <summary>
        /// 指定是否是Debug状态，如果是，系统会自动输出日志
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return _isDebug;
            }
            set
            {
                _isDebug = value;

                //if (_isDebug)
                //{
                //    WeixinTrace.Open();
                //}
                //else
                //{
                //    WeixinTrace.Close();
                //}
            }
        }

        /// <summary>
        /// 链接地址URL
        /// </summary>
        public static string URL
        {
            get
            {
                return WebConfigurationManager.AppSettings["URL"];
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string ServerIP
        {
            get
            {
                return WebConfigurationManager.AppSettings["ServerIP"];
            }
        }

        /// <summary>
        /// Auth认证域名
        /// </summary>
        public static string AuthUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["AuthUrl"];
            }
        }

    }
}
