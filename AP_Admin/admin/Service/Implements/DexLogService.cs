using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Jayrock.Json.Conversion;
using cPos.Admin.Component;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Dex;
using cPos.Admin.Model.User;
using cPos.Admin.Service.Interfaces;

namespace cPos.Admin.Service.Implements
{
    public class DexLogService : BaseService, IDexLogService
    {
        /// <summary>
        /// 获取平台列表
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAppList()
        {
            IList<string> list = new List<string>();
            list.Add("AP");
            list.Add("BS");
            list.Add("Client");
            list.Add("DEX");
            return list;
        }

        /// <summary>
        /// 获取日志类型列表
        /// </summary>
        /// <returns></returns>
        public IList<string> GetLogTypes()
        {
            IList<string> list = new List<string>();
            list.Add("Error");
            list.Add("Trace");
            return list;
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="maxRowCount">返回最大行数</param>
        /// <param name="startRowIndex">起始行索引</param>
        /// <returns></returns>
        public IList<LogInfo> GetLogs(LoggingSessionInfo loggingSession, 
            Hashtable condition, int maxRowCount, int startRowIndex)
        {
            string uri = string.Format(ConfigurationManager.AppSettings["dex_url"].Trim() +
                "/logservice/getlogs?user_id={0}&user_pwd={1}&start_row={2}&rows_count={3}",
                loggingSession.UserID, new UserService().GetUserPassword(loggingSession.UserID),
                startRowIndex, maxRowCount);
            string method = "POST";
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(condition, writer);
            string content = writer.ToString();
            string data = DexUtils.GetRemoteData(uri, method, content);
            var contract = JsonConvert.Import<DexLogsContract>(data);
            return contract.Logs;
        }

        /// <summary>
        /// 获取日志列表数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public int GetLogsCount(LoggingSessionInfo loggingSession, Hashtable condition)
        {
            string uri = string.Format(ConfigurationManager.AppSettings["dex_url"].Trim() +
                "/logservice/getlogscount?user_id={0}&user_pwd={1}",
                loggingSession.UserID, new UserService().GetUserPassword(loggingSession.UserID));
            string method = "POST";
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(condition, writer);
            string content = writer.ToString();
            string data = DexUtils.GetRemoteData(uri, method, content);
            var contract = JsonConvert.Import<DexCountContract>(data);
            return contract.count;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>日志对象</returns>
        public LogInfo GetLog(LoggingSessionInfo loggingSession, string logId)
        {
            string uri = string.Format(ConfigurationManager.AppSettings["dex_url"].Trim() +
                "/logservice/getlog?user_id={0}&user_pwd={1}&log_id={2}",
                loggingSession.UserID, new UserService().GetUserPassword(loggingSession.UserID), 
                logId);
            string method = "GET";
            string data = DexUtils.GetRemoteData(uri, method, string.Empty);
            var contract = JsonConvert.Import<DexLogContract>(data);
            return contract.Log;
        }
    }

    public class DexBaseContract
    {
        public string status { get; set; }
        public string error_code { get; set; }
        public string error_desc { get; set; }
    }

    public class DexCountContract : DexBaseContract
    {
        public int count { get; set; }
    }

    public class DexLogsContract : DexBaseContract
    {
        public IList<LogInfo> Logs { get; set; }
    }

    public class DexLogContract : DexBaseContract
    {
        public LogInfo Log { get; set; }
    }

    #region DexUtils
    public class DexUtils
    {
        public static string GetRemoteData(string uri, string method, string content)
        {
            string respData = "";
            method = method.ToUpper();
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = method.ToUpper();
            req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            if (method == "POST")
            {
                byte[] buffer = Encoding.ASCII.GetBytes(content);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/json";
                Stream postStream = req.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Close();
            }
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            respData = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();
            return respData;
        }

        internal class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public AcceptAllCertificatePolicy()
            { }

            public bool CheckValidationResult(ServicePoint sPoint,
                System.Security.Cryptography.X509Certificates.X509Certificate cert,
                WebRequest wRequest, int certProb)
            {
                return true;
            }
        }
    }
    #endregion
}
