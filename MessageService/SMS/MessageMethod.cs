using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using JIT.Utility.Log;

namespace JIT.Utility.SMS
{
    public class MessageMethod
    {
        /// <summary>
        /// Post方式发送短信
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bData"></param>
        /// <returns></returns>
        public static string doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;
            string strResult = string.Empty;
            hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            hwRequest.Timeout = 5000;
            hwRequest.Method = "POST";
            hwRequest.ContentType = "application/x-www-form-urlencoded";
            hwRequest.ContentLength = bData.Length;
            try
            {
                using (var smWrite = hwRequest.GetRequestStream())
                {
                    smWrite.Write(bData, 0, bData.Length);
                    smWrite.Close();
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                strResult = "发送失败:{0}".Fmt(ex.Message);
                return strResult;
            }
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                using (var srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII))
                {
                    strResult = srReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = "发送失败:{0}".Fmt(ex.Message);
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return strResult;
        }

        /// <summary>
        /// get方式发送短信
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 120000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                Loggers.Exception(new ExceptionLogInfo(err));
                strResult = "发送失败:{0}".Fmt(err.Message);
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.UTF8);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                strResult = "发送失败:{0}".Fmt(err.Message);
                Loggers.Exception(new ExceptionLogInfo(err));
            }

            return strResult;
        }
    }
}
