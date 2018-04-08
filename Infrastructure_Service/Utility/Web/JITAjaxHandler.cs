/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/19 14:11:29
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;

using JIT.Const;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web
{
    /// <summary>
    /// JIT的Ajax请求的处理者 
    /// </summary>
    public abstract class JITAjaxHandler<TUserInfo> : IHttpHandler, IRequiresSessionState where TUserInfo : BasicUserInfo
    {
        #region 一般处理程序的入口
        /// <summary>
        /// 一般处理程序的入口
        /// </summary>
        /// <param name="pContext">Http上下文</param>
        public void ProcessRequest(HttpContext pContext)
        {
            try
            {
                //认证
                this.Authenticate();
                ////设置HTTP上下文的引用
                this.CurrentContext = pContext;
                ////由子类来负责对请求做响应
                ProcessAjaxRequest(pContext);
            }
            catch (ThreadAbortException)//Response.End会通过ThreadAbortException来结束响应
            {
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)//如果是子页面异常，则记录子页面异常信息
                {
                    ex = ex.InnerException;
                }
                Loggers.Exception(this.CurrentUserInfo, ex);
                this.ErrorHandling(pContext, ex.Message);
            }
        }
        #endregion

        #region 属性集
        /// <summary>
        /// HTTP请求的上下文
        /// </summary>
        protected HttpContext CurrentContext { get; private set; }

        /// <summary>
        /// 是否对传输的内容进行压缩
        /// </summary>
        protected bool IsCompress { get; set; }
        #endregion

        #region 抽象方法&虚方法
        /// <summary>
        /// 当前的用户信息
        /// </summary>
        protected abstract TUserInfo CurrentUserInfo { get; }

        /// <summary>
        /// 认证
        /// </summary>
        protected abstract void Authenticate();

        /// <summary>
        /// 本方法实现请求处理逻辑
        /// </summary>
        protected abstract void ProcessAjaxRequest(HttpContext pContext);
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

        #region 私有
        
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

        #region 基础结构
        /// <summary>
        /// 是否允许并发访问
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="pErrorMessage"></param>
        private void ErrorHandling(HttpContext pContext, string pErrorMessage)
        {
            pContext.Response.Clear();
            pContext.Response.StatusCode = 500;
            pContext.Response.TrySkipIisCustomErrors = true;    //禁用IIS7.0的自定义错误页面
            pContext.Response.Write(pErrorMessage);
            pContext.Response.End();
        }
        #endregion
    }
}
