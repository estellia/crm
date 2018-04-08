using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Web.Script.Serialization;
using JIT.Utility.AppVersion.Framework;

namespace JIT.Utility.AppVersion.Service.Base
{
    public abstract class BaseHandler : IHttpHandler
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseHandler()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 当前的HTTP上下文
        /// </summary>
        protected HttpContext CurrentContext { get; private set; }

        /// <summary>
        /// 是否对传输的内容进行压缩
        /// </summary>
        protected bool IsCompress { get; set; }
        #endregion

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext pContext)
        {
            AppMgrResponse req = null;
            string action = string.Empty;
            try
            {
                //认证
                this.Authenticate();
                //设置HTTP上下文的引用
                this.CurrentContext = pContext;
                //TODO:从QueryString和Form中获得action和reqContent
                action = pContext.Request["Action"];
                var request = HttpUtility.HtmlDecode(pContext.Request["request"]).DeserializeJSONTo<AppMgrRequest>();
                //处理请求
                req = this.ProcessAction(action, request);
            }
            catch (ThreadAbortException)//Response.End会通过ThreadAbortException来结束响应
            {
            }
            catch (Exception ex)
            {
                //记录日志
                Loggers.Exception(new ExceptionLogInfo(ex));
                //返回错误响应
                if (req == null)
                    req = new AppMgrResponse();
                req.ResultCode = 500;
                req.Message = ex.Message;
                req.Data = null;
            }
            //将req序列化成JSON返回
            if (req == null)
            {
                req = new AppMgrResponse();
                req.ResultCode = 500;
                req.Message = string.Format("无效的API请求操作[{0}].", action);
            }
            this.ResponseJSON(req);
        }

        #region 认证
        private void Authenticate()
        {

        }
        #endregion

        #region 工具方法

        #region 受保护

        #region 输出JSON格式的HTTP响应
        /// <summary>
        /// 输出JSON格式的HTTP响应
        /// </summary>
        /// <param name="pResponseData"></param>
        protected void ResponseJSON(object pResponseData)
        {
            string content = string.Empty;
            if (pResponseData == null)
            {
                content = "null";
            }
            else
            {
                content = pResponseData.ToJSON();
            }
            this.CurrentContext.Response.Clear();
            this.CurrentContext.Response.ContentType = "text/json";
            this.DoResponse(content);
        }
        #endregion

        #region 输出内容
        /// <summary>
        /// 输出HTTP响应内容
        /// </summary>
        /// <param name="pResponseContent">HTTP响应内容</param>
        protected void ResponseContent(string pResponseContent)
        {
            this.DoResponse(pResponseContent);
        }
        #endregion

        #region 解析JSON格式的HTTP请求的内容
        /// <summary>
        /// 解析JSON格式的Http请求内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T DeserializeJSONContent<T>()
        {
            using (StreamReader sr = new StreamReader(this.CurrentContext.Request.InputStream))
            {
                string postContent = sr.ReadToEnd();
                T t = default(T);
                if (!string.IsNullOrEmpty(postContent))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    t = serializer.Deserialize<T>(postContent);
                }
                return t;
            }
        }
        #endregion

        #endregion

        #region 私有方法

        #region 执行响应输出
        /// <summary>
        /// 执行响应输出
        /// </summary>
        /// <param name="pContent">响应内容</param>
        protected void DoResponse(string pContent)
        {
            bool hasResponse = false;
            if (this.IsCompress)
            {
                var acceptEncoding = this.CurrentContext.Request.Headers["Accept-Encoding"];
                acceptEncoding = acceptEncoding.Trim().ToLower();
                var bytes = this.CurrentContext.Response.ContentEncoding.GetBytes(pContent);
                if (acceptEncoding.Contains("gzip"))    //采用Http默认支持的gzip压缩
                {
                    this.CurrentContext.Response.AppendHeader("Content-Encoding", "gzip");
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new GZipStream(stream, CompressionMode.Compress))
                        {
                            writer.Write(bytes, 0, bytes.Length);
                        }
                        bytes = stream.ToArray();
                    }
                    this.CurrentContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    hasResponse = true;
                }
                else if (acceptEncoding.Contains("deflate"))    //采用Http默认支持的deflate压缩
                {
                    this.CurrentContext.Response.AppendHeader("Content-Encoding", "deflate");
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new DeflateStream(stream, CompressionMode.Compress))
                        {
                            writer.Write(bytes, 0, bytes.Length);
                        }
                        bytes = stream.ToArray();
                    }
                    this.CurrentContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    hasResponse = true;
                }
            }
            if (!hasResponse)   //如果不支持压缩，则采用默认的输出
            {
                this.CurrentContext.Response.Write(pContent);
            }
            this.CurrentContext.Response.End();
        }
        #endregion

        #endregion

        #endregion

        #region 抽象方法
        /// <summary>
        /// 处理请求操作
        /// </summary>
        /// <param name="pAction">请求的方法</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns></returns>
        protected abstract AppMgrResponse ProcessAction(string pAction, AppMgrRequest pRequest);
        #endregion
    }
}