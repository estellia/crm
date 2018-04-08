using JIT.Utility.Pay.Alipay.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace JIT.Utility.Pay.Alipay.Interface.Scan
{
    public static class AliPayScanGeteway
    {
        static AliPayScanGeteway()
        {
            Geteway = "https://openapi.alipay.com/gateway.do?";
        }

        public static readonly string Geteway;


        public static string GetResponseStr(RequestScanEntity pRequest, string scanDetailJson, string key)
        {
            Encoding code = Encoding.GetEncoding("utf-8");
            pRequest.biz_content = scanDetailJson;
            string requestPar = AliPayFunction.GetSignContent(pRequest.Paras);

            string sign = RSAFromPkcs8.Sign(requestPar, key, "utf-8"); //生成签名
            //构造请求地址
            string strUrl = Geteway + requestPar + "&sign=" + HttpUtility.UrlEncode(sign);  //请求参数

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                //发送POST数据请求服务器
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();

                //获取服务器返回信息
                StreamReader reader = new StreamReader(myStream, code);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }

                //释放
                myStream.Close();

                strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return HttpUtility.UrlDecode(strResult);
        }
    }

}
