using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebAPI.Test
{
    public static class HttpHelper
    {
        /// <summary>
        /// 以POST 形式请求数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="reqType">默认的请求类型</param>
        /// <returns></returns>
        public static string PostData(string requestPara, string url,
            string reqType = "application/x-www-form-urlencoded")
        {
            return PostData(requestPara, url, 60, reqType);
        }

        /// <summary>
        /// 以POST 形式请求数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="reqType">默认的请求类型</param>
        /// <returns></returns>
        public static string PostData(string requestPara, string url, int timeout,
            string reqType = "application/x-www-form-urlencoded")
        {
            string responseString = string.Empty;
            try
            {
                WebRequest webRequest = WebRequest.Create(url);

                byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(requestPara);
                webRequest.ContentType = reqType;
                webRequest.ContentLength = buf.Length;
                webRequest.Timeout = timeout * 1000;
                webRequest.Method = "POST";

                HttpWebResponse response;//= (HttpWebResponse)webRequest.GetResponse();
                try
                {
                    response = (HttpWebResponse)webRequest.GetResponse();
                }
                catch (WebException ex)
                {

                    response = (HttpWebResponse)ex.Response;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        responseString = reader.ReadToEnd();
                    }
                    throw new Exception(responseString);
                }


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    responseString = reader.ReadToEnd();
                }

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// 以GET 形式获取数据 默认utf-8
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string requestPara, string url, int timeout, Hashtable ht)
        {
            return GetData(requestPara, url, timeout, ht, Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// 以GET 形式获取数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static string GetData(string requestPara, string url, Hashtable ht, Encoding code)
        {
            return GetData(requestPara, url, 60, ht, code);
        }

        /// <summary>
        /// 以GET 形式获取数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static string GetData(string request, string url, int timeout, Hashtable ht, Encoding code)
        {
            string responseString = string.Empty;
            request = request.IndexOf('?') > -1 ? (request) : ("?" + request);
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Accept = "application/json";
                webRequest.Method = "GET";
                webRequest.Timeout = timeout * 1000;


                foreach (DictionaryEntry de in ht)
                {
                    webRequest.Headers.Add(de.Key.ToString(), de.Value.ToString());
                }

                HttpWebResponse response;//= (HttpWebResponse)webRequest.GetResponse();
                try
                {
                    response = (HttpWebResponse)webRequest.GetResponse();
                }
                catch (WebException ex)
                {

                    response = (HttpWebResponse)ex.Response;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        responseString = reader.ReadToEnd();
                    }
                    throw new Exception(responseString);
                }


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    responseString = reader.ReadToEnd();
                }

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        /// <summary>
        /// Http Soap 请求 默认超时时间1分钟
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <param name="reqType">请求参数类型</param>
        /// <param name="resType">返回参数类型</param>
        /// <returns></returns>
        public static string SendSoapRequest(string request, string url,
            string reqType = "application/x-www-form-urlencoded",
            string resType = "application/json")
        {
            return SendSoapRequest(request, url, 60, new Hashtable(), reqType, resType);
        }


        /// <summary>
        /// Http Soap 请求
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="reqType">请求参数类型</param>
        /// <param name="resType">返回参数类型</param>
        /// <returns></returns>
        public static string SendSoapRequest(string request, string url, int timeout, Hashtable ht, string reqType = "application/x-www-form-urlencoded", string resType = "application/json")
        {

            string responseString = string.Empty;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = reqType;// + ";charset=\"utf-8\""
            webRequest.Accept = resType;// resType;

            webRequest.Method = "POST";
            webRequest.Timeout = timeout * 1000;

           
            try
            {


                foreach (DictionaryEntry de in ht)
                {
                    webRequest.Headers.Add(de.Key.ToString(), de.Value.ToString());
                }
                byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(request);
                webRequest.ContentLength = bytes.Length;
                webRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse response;//= (HttpWebResponse)webRequest.GetResponse();
                try
                {
                    response = (HttpWebResponse)webRequest.GetResponse();
                }
                catch (WebException ex)
                {

                    response = (HttpWebResponse)ex.Response;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        responseString = reader.ReadToEnd();
                    }
                    throw new Exception(responseString);
                }


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    responseString = reader.ReadToEnd();
                }

                return responseString;
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }

}