using RedisOpenAPIClient.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;

namespace RedisOpenAPIClient
{
    /// <summary>
    /// 异步 Http 请求
    /// </summary>
    public class RedisOpenAPI
    {
        //
        // LM,2016/04/19
        //


        //
        private static string _URL = string.Empty;
        /// <summary>
        /// 请求地址（放在这里不用每次都取）
        /// </summary>
        private static string URL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_URL))
                {
                    _URL = AppConfigHelper.GetAppSettingsValue("RedisApiUrl");
                }
                return _URL;
            }
        }
        /// <summary>
        /// 控制器
        /// </summary>
        internal string Controller
        { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        internal string Action
        { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        internal string Content
        { get; set; }
        /// <summary>
        /// 请求数组
        /// </summary>
        private byte[] Buffer
        { get; set; }
        /// <summary>
        /// 请求流
        /// </summary>
        private Stream RequestStream
        { get; set; }
        /// <summary>
        /// 请求对象
        /// </summary>
        private HttpWebRequest Request
        { get; set; }
        /// <summary>
        /// 响应标识
        /// </summary>
        private bool ResponseFlag
        { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        private string Result
        { get; set; }
        /// <summary>
        /// 超时标识
        /// </summary>
        private bool TimeoutFlag
        { get; set; }
        /// <summary>
        /// 超时时间 ms
        /// </summary>
        internal int TimeoutTime
        { get; set; }
        /// <summary>
        /// 重试标识
        /// </summary>
        private bool RetryFlag
        { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        internal int RetryCount
        { get; set; }

        //
        private RedisOpenAPI()
        {
            //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
            this.Request = default(HttpWebRequest);

            //
            //this.URL = AppConfigHelper.GetAppSettingsValue("RedisApiUrl");
            this.Controller = string.Empty;
            this.Action = string.Empty;
            this.Content = string.Empty;
            this.Buffer = default(byte[]);
            this.RequestStream = default(Stream);
            this.ResponseFlag = false;
            this.Result = string.Empty;
            this.TimeoutFlag = false;
            this.TimeoutTime = 10 * 1000;
            this.RetryFlag = false;
            this.RetryCount = 3;
        }

        //
        /// <summary>
        /// 实例
        /// </summary>
        public static RedisOpenAPI Instance
        {
            get
            {
                return new RedisOpenAPI();
            }
        }

        // 
        private void RemoteNew(Action<RedisOpenAPI, string> action)  //一个具有RedisOpenAPI类和string参数的委托，把这样一个方法作为参数传递过来
        {
            //
            var reNum = 0;
            for (var i = 0; i < this.RetryCount; i++)
            {
                try
                {
                    //
                    var uri = URL + this.Controller + "/" + this.Action;

                    //
                    this.Request = WebRequest.Create(uri) as HttpWebRequest;
                    this.Request.KeepAlive = false;
                    this.Request.Method = "POST";
                    this.Request.Credentials = CredentialCache.DefaultCredentials;
                    this.Buffer = Encoding.UTF8.GetBytes(this.Content);
                    this.Request.ContentLength = this.Buffer.Length;
                    this.Request.ContentType = "application/json";
                    this.RequestStream = this.Request.GetRequestStream();
                    this.RequestStream.Write(this.Buffer, 0, this.Buffer.Length);
                    this.RequestStream.Close();

                    //
                    this.Request.BeginGetResponse((arr) =>
                    {
                        //
                        var state = arr.AsyncState as RedisOpenAPI;
                        //
                        var response = state.Request.EndGetResponse(arr) as HttpWebResponse;
                        var respStream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                        action(state, respStream.ReadToEnd());//调用以委托的形式传递过来的方法
                        respStream.Close();
                        response.Close();
                    }, this);
                    //
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(100);
                    reNum++;
                    if (reNum == this.RetryCount)
                    {
                        throw new Exception(string.Format("重试失败!重试次数:{0}次,失败原因:{1}", this.RetryCount, ex.Message));
                    }
                    continue;
                }
            }
        }

        //
        /// <summary>
        /// 获取响应数据
        /// </summary>
        internal string GetRemoteData()
        {
            // 调用机制  --  仿ajax/web回调机制,加入重试,超时
            RemoteNew(SetResult); //把SetResult方法通过委托的形式作为参数传递过去，在RemoteNew里面调用

            //
            var timeNum = 0;
            var sleepNum = 10;
            while (true)
            {
                if (ResponseFlag)
                {
                    break;
                }
                if (TimeoutFlag)
                {
                    throw new Exception(string.Format("请求超时!超时时间:{0}S", TimeoutTime / 1000));
                }
                timeNum += sleepNum;
                if (timeNum >= TimeoutTime)
                {
                    TimeoutFlag = true;
                }
                Thread.Sleep(sleepNum);
            }

            //
            return Result;
        }
         
        //
        private void SetResult(RedisOpenAPI state, string jsonData)
        {
            if (!jsonData.IsNullStr())
            {
                state.Result = jsonData;
                state.ResponseFlag = true;
            }
        }
    }
}
