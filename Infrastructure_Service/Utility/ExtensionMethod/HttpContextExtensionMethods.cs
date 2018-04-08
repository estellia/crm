/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/5/16 16:52:35
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// HttpContext的扩展方法
    /// </summary>
    public static class HttpContextExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取客户端的IP地址
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string GetClientIP(this HttpContext pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            //
            string ip = pCaller.Request.UserHostAddress;
            //如果请求是来自本地的,则需要将127.0.0.1转换为机器的具体IP
            if (pCaller.Request.IsLocal)
            {
                string hostName = System.Net.Dns.GetHostName();
                var ips = System.Net.Dns.GetHostAddresses(hostName);
                if (ips != null && ips.Length > 0)
                {
                    foreach (var item in ips)
                    {
                        if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            return item.ToString();
                    }
                }
            }
            return ip;
        }
    }
}
