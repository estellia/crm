using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.DataAccess;
using System.Net;
using System.IO;
using JIT.Utility.Log;
using JIT.MessageService.Base;
using JIT.MessageService.Entity;
using JIT.MessageService;

namespace JIT.MessageService
{
    public class MessageHandler
    {
        public string Process(SMSSendEntity entity)
        {
            var NO = entity.MobileNO;
            string param = "URL{0}";
            if (PhoneNOHelper.IsCMCC(NO))
                param = param.Fmt(2);
            else if (PhoneNOHelper.IsCUCC(NO))
                param = param.Fmt(3);
            else if (PhoneNOHelper.IsCTCC(NO))
                param = param.Fmt(4);
            else
                param = param.Fmt(2);
            string URL = ConfigurationManager.AppSettings[param];
            int retryTime = 0;
            bool isSuccess = false;
            String postReturn = string.Empty;

            while (isSuccess == false && retryTime < 3)
            {
                try
                {
                    postReturn = doPostRequest(URL, entity.GetRequest().GetCommandStrData());
                    isSuccess = true;
                    retryTime++;
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    if (retryTime >= 3)
                        throw ex;
                }
            }
            return postReturn;
        }

        public string process(List<SMSSendEntity> entities)
        {
            BaseRequest request = entities.GetMultiMTRequest();
            string URL = ConfigurationManager.AppSettings["URL2"];
            int retryTime = 0;
            Exception ex = new Exception("发送失败,请检查网络");
            while (retryTime < 3)
            {
                try
                {
                    String postReturn = doPostRequest(URL, request.GetCommandStrData());
                    return postReturn;
                }
                catch (Exception ee)
                {
                    ex = ee;
                    retryTime++;
                    URL = ConfigurationManager.AppSettings["URL2"];
                    Loggers.Exception(new ExceptionLogInfo(ee));
                }
            }
            throw ex;
        }

        private static string doPostRequest(string url, byte[] bData)
        {

            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;
            string strResult;
            hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            hwRequest.Timeout = 5000;
            hwRequest.Method = "POST";
            hwRequest.ContentType = "application/x-www-form-urlencoded";
            hwRequest.ContentLength = bData.Length;

            using (var smWrite = hwRequest.GetRequestStream())
            {
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }

            hwResponse = (HttpWebResponse)hwRequest.GetResponse();
            using (var srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII))
            {
                strResult = srReader.ReadToEnd();
            }
            return strResult;
        }

        //GET方式发送得结果
        private static string doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                Loggers.Exception(new ExceptionLogInfo(err));
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                Loggers.Exception(new ExceptionLogInfo(err));
            }

            return strResult;
        }
    }
}
