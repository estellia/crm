using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;

namespace cPos.Dex.Services
{
    public class LogDBService
    {
        #region GetLogs
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="ht">LogId, BizId, BizName, LogTypeId, LogTypeCode, LogCode, 
        ///     CreateTimeBegin, CreateTimeEnd, CreateUserId, ModifyTimeBegin, ModifyTimeEnd, ModifyUserId
        /// </param>
        public IList<LogInfo> GetLogs(Hashtable ht, long startRow, long rowsCount)
        {
            if (startRow == null || startRow < 0)
                startRow = 0;
            if (rowsCount == null || rowsCount < 0)
                rowsCount = Dex.Common.Config.QueryDBMaxCount;
            ht["StartRow"] = startRow;
            ht["EndRow"] = startRow + rowsCount;
            return SqlMapper.Instance().QueryForList<LogInfo>("LogInfo.GetLogs", ht);
        }

        public int GetLogsCount(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForObject<int>("LogInfo.GetLogsCount", ht);
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        public IList<Log> GetLogs(LogQueryInfo queryInfo, long startRow, long rowsCount)
        {
            Hashtable htQueryInfo = new Hashtable();
            htQueryInfo.Add("LogId", queryInfo.log_id);
            htQueryInfo.Add("BizId", queryInfo.biz_id);
            htQueryInfo.Add("BizName", queryInfo.biz_name);
            htQueryInfo.Add("LogTypeId", queryInfo.log_type_id);
            htQueryInfo.Add("LogCode", queryInfo.log_code);
            htQueryInfo.Add("LogBody", queryInfo.log_body);
            htQueryInfo.Add("CreateTimeBegin", queryInfo.create_time_begin);
            htQueryInfo.Add("CreateTimeEnd", queryInfo.create_time_end);
            htQueryInfo.Add("CreateUserId", queryInfo.create_user_id);
            htQueryInfo.Add("ModifyTimeBegin", queryInfo.modify_time_begin);
            htQueryInfo.Add("ModifyTimeEnd", queryInfo.modify_time_end);
            htQueryInfo.Add("ModifyUserId", queryInfo.modify_user_id);
            htQueryInfo.Add("CustomerCode", queryInfo.customer_code);
            htQueryInfo.Add("CustomerId", queryInfo.customer_id);
            htQueryInfo.Add("UnitCode", queryInfo.unit_code);
            htQueryInfo.Add("UnitId", queryInfo.unit_id);
            htQueryInfo.Add("UserCode", queryInfo.user_code);
            htQueryInfo.Add("UserId", queryInfo.user_id);
            htQueryInfo.Add("IfCode", queryInfo.if_code);
            htQueryInfo.Add("AppCode", queryInfo.app_code);

            var rows = GetLogs(htQueryInfo, startRow, rowsCount);
            if (rows == null) return null;
            IList<Log> list = new List<Log>();
            foreach (var row in rows)
            {
                Log item = new Log();
                item.log_id = row.LogId;
                item.biz_id = row.BizId;
                item.biz_name = row.BizName;
                item.log_type_id = row.LogTypeId;
                item.log_type_code = row.LogTypeCode;
                item.log_code = row.LogCode;
                item.log_body = row.LogBody;
                item.create_time = row.CreateTime;
                item.create_user_id = row.CreateUserId;
                item.modify_time = row.ModifyTime;
                item.modify_user_id = row.ModifyUserId;
                item.customer_code = row.CustomerCode;
                item.customer_id = row.CustomerId;
                item.unit_code = row.UnitCode;
                item.unit_id = row.UnitId;
                item.user_code = row.UserCode;
                item.user_id = row.UserId;
                item.if_code = row.IfCode;
                item.app_code = row.AppCode;
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 获取日志数量列表
        /// </summary>
        public int GetLogsCount(LogQueryInfo queryInfo)
        {
            Hashtable htQueryInfo = new Hashtable();
            htQueryInfo.Add("LogId", queryInfo.log_id);
            htQueryInfo.Add("BizId", queryInfo.biz_id);
            htQueryInfo.Add("BizName", queryInfo.biz_name);
            htQueryInfo.Add("LogTypeId", queryInfo.log_type_id);
            htQueryInfo.Add("LogCode", queryInfo.log_code);
            htQueryInfo.Add("LogBody", queryInfo.log_body);
            htQueryInfo.Add("CreateTimeBegin", queryInfo.create_time_begin);
            htQueryInfo.Add("CreateTimeEnd", queryInfo.create_time_end);
            htQueryInfo.Add("CreateUserId", queryInfo.create_user_id);
            htQueryInfo.Add("ModifyTimeBegin", queryInfo.modify_time_begin);
            htQueryInfo.Add("ModifyTimeEnd", queryInfo.modify_time_end);
            htQueryInfo.Add("ModifyUserId", queryInfo.modify_user_id);
            htQueryInfo.Add("CustomerCode", queryInfo.customer_code);
            htQueryInfo.Add("CustomerId", queryInfo.customer_id);
            htQueryInfo.Add("UnitCode", queryInfo.unit_code);
            htQueryInfo.Add("UnitId", queryInfo.unit_id);
            htQueryInfo.Add("UserCode", queryInfo.user_code);
            htQueryInfo.Add("UserId", queryInfo.user_id);
            htQueryInfo.Add("IfCode", queryInfo.if_code);
            htQueryInfo.Add("AppCode", queryInfo.app_code);
            return GetLogsCount(htQueryInfo);
        }
        #endregion

        /// <summary>
        /// 获取日志
        /// </summary>
        public LogInfo GetLogById(string id)
        {
            return SqlMapper.Instance().QueryForObject<LogInfo>("LogInfo.GetLogById", id);
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        public Log GetLog(string logId)
        {
            var row = GetLogById(logId);
            if (row == null) return null;
            Log item = new Log();
            item.log_id = row.LogId;
            item.biz_id = row.BizId;
            item.biz_name = row.BizName;
            item.log_type_id = row.LogTypeId;
            item.log_type_code = row.LogTypeCode;
            item.log_code = row.LogCode;
            item.log_body = row.LogBody;
            item.create_time = row.CreateTime;
            item.create_user_id = row.CreateUserId;
            item.modify_time = row.ModifyTime;
            item.modify_user_id = row.ModifyUserId;
            item.customer_code = row.CustomerCode;
            item.customer_id = row.CustomerId;
            item.unit_code = row.UnitCode;
            item.unit_id = row.UnitId;
            item.user_code = row.UserCode;
            item.user_id = row.UserId;
            item.if_code = row.IfCode;
            item.app_code = row.AppCode;
            return item;
        }

        /// <summary>
        /// 插入日志
        /// </summary>
        public bool InsertLog(LogInfo info)
        {
            string time = Utils.GetNowWithMillisecond();
            if (info.CreateTime == null)
                info.CreateTime = time;
            if (info.ModifyTime == null)
                info.ModifyTime = time;
            SqlMapper.Instance().Insert("LogInfo.InsertLog", info);
            return true;
        }

        ///// <summary>
        ///// 更新日志
        ///// </summary>
        //public bool UpdateLog(LogInfo info)
        //{
        //    SqlMapper.Instance().Update("LogInfo.UpdateLog", info);
        //    return true;
        //}

        /// <summary>
        /// 删除日志
        /// </summary>
        public bool DeleteLog(string id)
        {
            SqlMapper.Instance().Update("LogInfo.DeleteLog", id);
            return true;
        }


        /// <summary>
        /// 获取日志类型列表
        /// </summary>
        /// <param name="ht">LogTypeId, TypeCode, TypeName, TypeStatus</param>
        public IList<LogTypeInfo> GetLogTypes(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<LogTypeInfo>("LogInfo.GetLogTypes", ht);
        }

        /// <summary>
        /// 获取日志类型
        /// </summary>
        public LogTypeInfo GetLogTypeById(string id)
        {
            return SqlMapper.Instance().QueryForObject<LogTypeInfo>("LogInfo.GetLogTypeById", id);
        }

        /// <summary>
        /// 获取日志类型
        /// </summary>
        public LogTypeInfo GetLogTypeByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<LogTypeInfo>("LogInfo.GetLogTypeByCode", code);
        }

        /// <summary>
        /// 插入日志类型
        /// </summary>
        public bool InsertLogType(LogTypeInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();
            SqlMapper.Instance().Insert("LogInfo.InsertLogType", info);
            return true;
        }

        /// <summary>
        /// 更新日志类型
        /// </summary>
        public bool UpdateLogType(LogTypeInfo info)
        {
            SqlMapper.Instance().Update("LogInfo.UpdateLogType", info);
            return true;
        }

        /// <summary>
        /// 删除日志类型
        /// </summary>
        public bool DeleteLogType(string id)
        {
            SqlMapper.Instance().Update("LogInfo.DeleteLogType", id);
            return true;
        }
    }
}
