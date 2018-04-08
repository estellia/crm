using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace JIT.Utility.Pay.Alipay.Interface.Base
{
    public static class BaseGeteway
    {
        public static string GetResponseStr(BaseRequest pRequest, string pGeteway)
        {
            Encoding code = Encoding.GetEncoding(pRequest.InputCharset);

            //待请求参数数组字符串
            string strRequestData = pRequest.GetContent();

            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = strRequestData });

            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = pGeteway.Trim('?') + "?" + "_input_charset=" + pRequest.InputCharset;

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                //填充POST数据
                myReq.ContentLength = bytesRequestData.Length;
                Stream requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();

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
