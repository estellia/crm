/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/12/18 17:04:10
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
    /// 进行HTTP POST/GET请求的工具类 
    /// <remarks>
    /// <para>注意：</para>
    /// <para>1.请求和响应的编码必须为UTF-8</para>
    /// </remarks>
    /// </summary>
    public static class HttpClient
    {
        /// <summary>
        /// POST
        /// </summary>
        /// <param name="pUrl">提交的地址</param>
        /// <param name="pParams">参数</param>
        /// <returns></returns>
        public static string Post(string pUrl, Dictionary<string, string> pParams)
        {
            string postContent = string.Empty;
            if (pParams != null)
                postContent = GenerateLinkedParameters(pParams);
            //
            byte[] bytePostContent = Encoding.UTF8.GetBytes(postContent);
            //创建请求
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(pUrl);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            //
            req.ContentLength = bytePostContent.Length;
            //
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="content"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static Stream GetQueryStream(string pUrl, string content, string charset = "utf-8")
        {
            string url = string.Empty;
            if (!string.IsNullOrEmpty(content))
            {
                url = string.Format("{0}?{1}", pUrl.Trim('?'), content);
            }
            else
            {
                url = pUrl.Trim('?');
            }

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Timeout = 10000;
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            return myResponse.GetResponseStream();
        }

        /// <summary>
        /// GetQueryString
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="content"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetQueryString(string pUrl, string content = "", string charset = "utf-8")
        {
            using (var stream = GetQueryStream(pUrl, content))
            {
                using (var rd = new StreamReader(stream, Encoding.GetEncoding(charset)))
                {
                    return rd.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string PostQueryString(string pUrl, string content, string contentType = Const.HttpContentType.X_WWW_FORM_URLENCODED, string charset = "utf-8")
        {
            using (var stream = PostQueryStream(pUrl, content, contentType, charset))
            {
                using (var rd = new StreamReader(stream, Encoding.GetEncoding(charset)))
                {
                    return rd.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Post请求地址获取stream返回
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static Stream PostQueryStream(string pUrl, string content, string contentType, string charset)
        {
            //json格式请求数据
            string requestData = content;
            //拼接URL
            string serviceUrl = pUrl;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding(charset).GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            if (contentType == Const.HttpContentType.JSON)
                myRequest.Accept = contentType;
            myRequest.ContentType = contentType;
            myRequest.Timeout = 120000;
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            return myResponse.GetResponseStream();
        }


        #region 工具方法
        /// <summary>
        /// 将字典中的所有元素，按照 "参数名"="参数值" 的方式用"&"字符拼接成字符串
        /// </summary>
        /// <param name="pParams">参数字典</param>
        /// <returns></returns>
        public static string GenerateLinkedParameters(Dictionary<string, string> pParams)
        {
            if (pParams == null || pParams.Count <= 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in pParams)
            {
                sb.AppendFormat("&{0}={1}", item.Key, item.Value);
            }
            return sb.ToString(1, sb.Length - 1);
        }
        #endregion
    }
}
