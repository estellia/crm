using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using cPos.Dex.Common;
using cPos.Dex.Model;

namespace cPos.Dex.Services
{
    public class LogService
    {
        public static string GetHashtableLog(string key, Hashtable ht)
        {
            return Dex.Common.Json.GetJsonString(ht);
        }

        public static void Write(LogMethod method, string bizId, 
            string key, string logCode, string msg, string createUserId, Hashtable logExt)
        {
            try
            {
                LogDBService dbService = new LogDBService();
                LogInfo logInfo = new LogInfo();
                logInfo.LogId = Utils.NewGuid();
                logInfo.BizId = bizId;
                logInfo.BizName = key;
                logInfo.LogCode = logCode;
                logInfo.LogBody = msg;
                logInfo.CreateUserId = createUserId;

                if (logExt["customer_code"] != null)
                    logInfo.CustomerCode = logExt["customer_code"].ToString();
                if (logExt["customer_id"] != null)
                    logInfo.CustomerId = logExt["customer_id"].ToString();
                if (logExt["unit_code"] != null)
                    logInfo.UnitCode = logExt["unit_code"].ToString();
                if (logExt["unit_id"] != null)
                    logInfo.UnitId = logExt["unit_id"].ToString();
                if (logExt["user_code"] != null)
                    logInfo.UserCode = logExt["user_code"].ToString();
                if (logExt["user_id"] != null)
                    logInfo.UserId = logExt["user_id"].ToString();
                if (logExt["if_code"] != null)
                    logInfo.IfCode = logExt["if_code"].ToString();
                if (logExt["app_code"] != null)
                    logInfo.AppCode = logExt["app_code"].ToString();

                logInfo.LogTypeId = method == LogMethod.Error ?
                    dbService.GetLogTypeByCode(Config.LogErrorTypeCode).LogTypeId :
                    dbService.GetLogTypeByCode(Config.LogTraceTypeCode).LogTypeId;

                dbService.InsertLog(logInfo);
            }
            catch (Exception ex)
            {
                SaveFile("WriteLogException", ex.ToString());
            }
        }

        public static void WriteError(string bizId, string key, 
            string logCode, string msg, string createUserId, Hashtable logExt)
        {
            Write(LogMethod.Error, bizId, key, logCode, msg, createUserId, logExt);
        }

        public static void WriteError(string bizId, string key, 
            string logCode, Hashtable ht, string createUserId, Hashtable logExt)
        {
            WriteError(bizId, key, logCode, GetHashtableLog(key, ht), createUserId, logExt);
        }

        public static void WriteTrace(string bizId, string key, 
            string logCode, string msg, string createUserId, Hashtable logExt)
        {
            return;
            Write(LogMethod.Trace, bizId, key, logCode, msg, createUserId, logExt);
        }

        public static void WriteTrace(string bizId, string key,
            string logCode, Hashtable ht, string createUserId, Hashtable logExt)
        {
            WriteTrace(bizId, key, logCode, GetHashtableLog(key, ht), createUserId, logExt);
        }

        public static void SaveFile(string key, string msg)
        {
            Utils.SaveFile(Dex.Common.Config.LogFolder() + key, 
                Utils.GetNow() + "_" + Utils.NewGuid() + ".txt", msg);
        }
    }
}
