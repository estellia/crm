using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using System.Configuration;
using System.ServiceModel;
using JIT.Utility.Log;
using Utility.Sync.WCFService.DataAccess;
using Utility.Sync.WCFService.WCF.API;
using Utility.Sync.WCFService.Entity;
using JIT.Utility;

using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Utility.Sync.WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Sync”。
    public class Sync : ISync
    {
        public void DoWork()
        {
        }

        public WCF.API.SyncRespose SyncLog(WCF.API.SyncRequest sRequest)
        {
            //返回参数
            SyncRespose response = new SyncRespose();
            response.ResultCode = 0;
            SyncLogDAO dao = new SyncLogDAO(new BasicUserInfo());
            try
            { 
                
                string url = ConfigurationManager.AppSettings["ALDMemberService"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("未配置ALD接口URL:ALDMemberService");

                string json = "action=CollectionItem&ReqContent={\"Parameters\":{\"ItemID\":\""
                    + sRequest.SourceItemID + "\",\"MemberID\":\"" +
                    sRequest.MemberID + "\",\"CustomerID\":\"" +
                    sRequest.ClientID +"\",\"CollectionType\":\""+ 
                    sRequest.SourceType+"\"},\"AreaID\":0,\"UserID\":\"\",\"Token\":\"\",\"Locale\":1}";

                var rtnJson = SendHttpRequest(url, json);
                JObject jObject = (JObject)JsonConvert.DeserializeObject(rtnJson);//序列化

                //同步日志对象
                SyncLogEntity entity = new SyncLogEntity
                {
                    LogID = Guid.NewGuid(),
                    SourceItemID = sRequest.SourceItemID,
                    ClientID = sRequest.ClientID,
                    MemberID = sRequest.MemberID
                };
                if (int.Parse(jObject["ResultCode"].ToString()) == 200)
                {
                    response.ResultCode = 200;
                    entity.IsNotSync = 0; //同步成功
                }
                else
                {
                    response.ResultCode = 100;
                    entity.IsNotSync = 1;//同步失败
                }
                //sourceType：1：收藏；2：取消收藏
                if (sRequest.SourceType == 1)
                    entity.SourceType = 1;
                else
                    entity.SourceType = 2;
                    
                //调用创建同步日志
               dao.Create(entity);
            }
            catch (Exception ex)
            {
                response.ResultCode = 100;
                response.Message = ex.Message;
                Loggers.Exception(new ExceptionLogInfo(ex));
            }

            return response;
        }

        public static string SendHttpRequest(string requestURI, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = requestURI;
            System.Net.HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            //Content-type: application/json; charset=utf-8

            //myRequest.ContentType = "text/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }

    }
}
