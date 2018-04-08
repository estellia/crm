
using adminProject.ApplicationInterface.DTO.Base;
using adminProject.ApplicationInterface.DTO.ValueObject;
using cPos.Admin.Component;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace adminProject.ApplicationInterface
{
    public abstract class BaseGateway :IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //参数解析
                var action = context.Request.QueryString["action"];
                //var strVersion = context.Request.QueryString["v"];
                var strAPIType = context.Request.QueryString["type"];
                var reqContent = context.Request["req"];
                var type = APITypes.Product;
                reqContent = HttpUtility.UrlDecode(HttpUtility.UrlEncode(reqContent));
                //获取请求参数的公共参数部分
                var commonRequest = reqContent.DeserializeJSONTo<EmptyRequest>();   //将请求反序列化为空接口请求,获得接口的公共参数
                if (commonRequest == null)
                {
                    throw new APIException(ERROR_CODES.INVALID_REQUEST_REQUEST_DESERIALIZATION_FAILED, "缺少请求参数.");
                }
                else
                {
                    this.JSONP = commonRequest.JSONP;
                }
                #region 插入日志信息  Add by changjian.tian 2014-04-24

                log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
                if (reqContent != null && reqContent.Length > 0)
                {
                    //Default.ReqData reqObj = reqContent.DeserializeJSONTo<Default.ReqData>();
                    //logger.Info(new LogContent(action, context.Request["req"], commonRequest.UserID, commonRequest.CustomerID, commonRequest.UserID, commonRequest.OpenID, HttpContext.Current.GetClientIP(), "", "", null));
                }

                #endregion
                //
                if (Enum.TryParse<APITypes>(strAPIType, out type))
                {
                    var rsp = this.ProcessAction(strAPIType, action, reqContent);
                    //输出
                    this.DoResponse(context, rsp);
                }
                else
                {
                    throw new APIException(ERROR_CODES.INVALID_REQUEST_LACK_TYPE_IN_QUERYSTRING, "请求的QueryString中缺少type节.");
                }
            }
            catch (APIException ex)
            {
                //Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                //ErrorResponse rsp = new ErrorResponse(ex.ErrorCode, ex.Message);
                //this.DoResponse(context, rsp.ToJSON());
                throw new APIException(ex.Message);
            }
            //catch (ThreadAbortException) { }    //Response.End通过抛出ThreadAbortException异常来实现中止执行后续代码的功能
            catch (Exception ex)
            {
                //Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                //ErrorResponse rsp = new ErrorResponse(ERROR_CODES.DEFAULT_ERROR, ex.Message);
                //ErrorResponse rsp = new ErrorResponse(ERROR_CODES.DEFAULT_ERROR, "操作失败，请联系管理员。");
                //this.DoResponse(context, rsp.ToJSON());
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行请求处理
        /// </summary>
        /// <param name="pType">接口类别</param>
        /// <param name="pAction">请求操作</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns>响应结果</returns>
        protected abstract string ProcessAction(string pType, string pAction, string pRequest);

        #region 工具方法
        /// <summary>
        /// 执行响应输出
        /// </summary>
        /// <param name="pContext">Http上下文</param>
        /// <param name="pRspContent">响应内容</param>
        protected void DoResponse(HttpContext pContext, string pRspContent)
        {
            if (!string.IsNullOrWhiteSpace(this.JSONP))
            {
                pRspContent = string.Format("{0}({1})", this.JSONP, pRspContent);
            }

            pContext.Response.Clear();
            pContext.Response.StatusCode = 200;
            pContext.Response.Write(pRspContent);
            //pContext.Response.End();
        }

        #endregion

        #region 属性集
        /// <summary>
        /// javascript的跨域支持
        /// </summary>
        protected string JSONP { get; set; }
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
        #endregion

        /// <summary>
        /// 序列化  针对时间做了处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, t);
                    string strJson = Encoding.UTF8.GetString(ms.ToArray());
                    //替换Json的date字符串
                    string p = @"\\/Date\((\d+)\+\d+\)\\/";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDataToDateString);

                    Regex reg = new Regex(p);
                    strJson = reg.Replace(strJson, matchEvaluator);
                    return strJson;
                }

            }
            catch (IOException)
            {

                return null;
            }
        }
        /// <summary>
        /// Converts the json data to date string.
        /// </summary>
        ///<param name="m">
        /// <returns>System.String.</returns>
        /// <remarks></remarks>
        private static string ConvertJsonDataToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();//转换为本地时间
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;

        }
        
    }
}