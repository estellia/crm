using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Collections;

namespace WebAPI.Test.Controllers
{
    public class TokenController : ApiController
    {
        static string errMsg;
        private readonly static string _tokenUrl = "token";      
        static string BaseUrl,pUserName, pPwd;      
        static string statusCode, _accToken;
        static int _timeout = 10;



        public static void Login()
        {
            string errMsg = string.Empty;
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            pUserName = ConfigurationManager.AppSettings["WebAPIUserName"];
            pPwd = ConfigurationManager.AppSettings["WebAPIPWD"];
            var SessionID = Token(pUserName, pPwd);
        }


        public static string Token(string pUserName, string pPwd)
        {
            string postData = string.Format("grant_type=password&username={0}&password={1}&ran={2}", pUserName, pPwd, Guid.NewGuid().ToString("N"));

            return GetResponse(postData, _tokenUrl, out statusCode);
        }

        private static string GetResponse(string pReqPar, string pUrl, out string statusCode, string pReqType = "application/json")
        {
            // todo 记录日志
            string resultStr = string.Empty;
            try
            {

                Hashtable ht = new Hashtable();
                // todo 判断acctoken是否过期
                ht.Add("Authorization", string.Format("Bearer {0}", _accToken));


                resultStr = HttpHelper.SendSoapRequest(pReqPar, string.Format("{0}{1}", BaseUrl, pUrl), _timeout, ht, pReqType, "text/json"); //BaseUrl
                statusCode = "1";
                return resultStr;
            }

            catch (WebException ex)
            {
                if ((((System.Net.HttpWebResponse)ex.Response).StatusCode) == HttpStatusCode.Unauthorized)
                {
                    statusCode = "401";
                }
                else
                if ((((System.Net.HttpWebResponse)ex.Response).StatusCode) == HttpStatusCode.GatewayTimeout)
                {
                    statusCode = "504";//连接超时

                }
                else
                    statusCode = "-1";
                return resultStr = string.Empty;
            }
        }






    }
}

