using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.TradeCenter.Framework.DataContract;
using JIT.TradeCenter.Framework;
using JIT.Utility.ExtensionMethod;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using JIT.TradeCenter.BLL;

namespace JIT.TradeCenter.Service
{
    /// <summary>
    /// DevPayTest 的摘要说明
    /// 用于开发人员调用,因为正式环境启用了IP过滤,开发人员IP地址不是固定的,所以调用此接口以访问支付接口
    /// </summary>
    public class DevPayTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //var request = context.Request;
                //context.Response.ContentType = "text/plain";
                //string Action = request["Action"];
                //var tRequest = request["request"].DeserializeJSONTo<TradeRequest>();
                ////获取正式支付接口
                //var Url = ConfigurationManager.AppSettings["DevGetewayUrl"];
                //string parameter = string.Format("action={0}&request={1}", Action, tRequest.ToJSON());
                //var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
                ////调用正式的接口
                //var res = GetResponseStr(Url, data);
                //context.Response.Write(res);

                OrderQueryBLL bll = new OrderQueryBLL(new Utility.BasicUserInfo());
                bll.SetNotificationFailed();


            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToJSON());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string GetResponseStr(string url, byte[] data)
        {
            Encoding code = Encoding.GetEncoding("utf-8");

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                var stream = myReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

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

                return strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}