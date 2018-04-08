/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/21 19:15:53
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
using System.IO;
using System.Net;
using System.Text;

namespace JIT.Utility.Web
{
    /// <summary>
    /// HTTP WEB请求 
    /// </summary>
    public static class HttpWebClient
    {
        /// <summary>
        /// 发送JSON请求
        /// </summary>
        /// <param name="pURL">请求的URL</param>
        /// <param name="pJSON">请求内容,内容格式为JSON</param>
        /// <returns></returns>
        public static string DoHttpRequest(string pURL, string pJSON)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pURL);
            byte[] reqContent = Encoding.UTF8.GetBytes(pJSON);
            //执行请求
            request.Method = "POST";
            request.ContentLength = reqContent.Length;
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;
            //将请求内容写入请求流中
            using (var stream = request.GetRequestStream())
            {
                stream.Write(reqContent, 0, reqContent.Length);
            }
            //
            using (var rsp = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(rsp.GetResponseStream(), Encoding.UTF8))
                {
                    var rspContent = reader.ReadToEnd();
                    return rspContent;
                }
            }
        }
    }
}
