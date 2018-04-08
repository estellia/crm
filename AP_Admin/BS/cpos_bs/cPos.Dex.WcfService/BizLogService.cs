using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using cPos.Dex.Model;
using cPos.Dex.Common;
using System.Data;
using System.Collections;
using cPos.Dex.Services;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    /// <summary>
    /// BizLogService
    /// </summary>
    public class BizLogService : BaseService, IBizLogService
    {
        #region GetLogs
        /// <summary>
        /// 获取日志集合接口
        /// </summary>
        public GetLogsContract GetLogs(TransType transType, string userId, string userPwd,
            long startRow, long rowsCount, LogQueryInfo queryInfo)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "LogService.GetLogs";
            string ifCode = "C011";
            var data = new GetLogsContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = userId;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("user_id", userId);
                htParams.Add("user_pwd", userPwd);
                htParams.Add("start_row", startRow);
                htParams.Add("rows_count", rowsCount);
                htParams.Add("query_info", queryInfo);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("密码", userPwd, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);

                // queryInfo
                htResult = ErrorService.CheckLength("日志ID", queryInfo.log_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("业务ID", queryInfo.biz_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("业务名称", queryInfo.biz_name, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("日志类型ID", queryInfo.log_type_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("日志类型代码", queryInfo.log_type_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("日志代码", queryInfo.log_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("日志内容", queryInfo.log_body, 0, 200, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("开始创建时间", queryInfo.create_time_begin, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("结束创建时间", queryInfo.create_time_end, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("创建人ID", queryInfo.create_user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("开始修改时间", queryInfo.modify_time_begin, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("结束修改时间", queryInfo.modify_time_end, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("修改人ID", queryInfo.modify_user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("客户代码", queryInfo.customer_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("客户ID", queryInfo.customer_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("门店代码", queryInfo.unit_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", queryInfo.unit_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("用户代码", queryInfo.user_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("用户ID", queryInfo.user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("接口代码", queryInfo.if_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                htResult = ErrorService.CheckLength("平台代码", queryInfo.app_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogsContract>(htResult);
                #endregion

                Hashtable htQueryInfo = new Hashtable();
                htQueryInfo.Add("LogId", queryInfo.log_id);
                htQueryInfo.Add("BizId", queryInfo.biz_id);
                htQueryInfo.Add("BizName", queryInfo.biz_name);
                htQueryInfo.Add("LogTypeId", queryInfo.log_type_id);
                htQueryInfo.Add("LogTypeCode", queryInfo.log_type_code);
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

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
                Hashtable htUser = new Hashtable();
                htUser.Add("UserId", userId);
                htUser.Add("CertPwd", userPwd);
                statusFlag = authService.Validate(htUser, ref certInfo);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A009, "用户ID与密码不匹配", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                // 获取数据
                Services.LogDBService logDBService = new Services.LogDBService();
                IList<LogInfo> logs = logDBService.GetLogs(htQueryInfo, startRow, rowsCount);
                if (logs != null)
                {
                    data.Logs = new List<LogContract>();
                    foreach (var log in logs)
                    {
                        LogContract logContract = new LogContract();
                        logContract.log_id = log.LogId;
                        logContract.biz_id = log.BizId;
                        logContract.biz_name = log.BizName;
                        logContract.log_type_id = log.LogTypeId;
                        logContract.log_type_code = log.LogTypeCode;
                        logContract.log_code = log.LogCode;
                        logContract.log_body = log.LogBody;
                        logContract.create_time = log.CreateTime;
                        logContract.create_user_id = log.CreateUserId;
                        logContract.modify_time = log.ModifyTime;
                        logContract.modify_user_id = log.ModifyUserId;
                        logContract.customer_code = log.CustomerCode;
                        logContract.customer_id = log.CustomerId;
                        logContract.unit_code = log.UnitCode;
                        logContract.unit_id = log.UnitId;
                        logContract.user_code = log.UserCode;
                        logContract.user_id = log.UserId;
                        logContract.if_code = log.IfCode;
                        logContract.app_code = log.AppCode;
                        data.Logs.Add(logContract);
                    }
                }

                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
            }
            catch (Exception ex)
            {
                data.status = Utils.GetStatus(false);
                data.error_code = ErrorCode.A000.ToString();
                data.error_full_desc = ex.ToString();
                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
            }
            return data;
        }

        public GetLogsContract GetLogsJson(string userId, string userPwd,
            long startRow, long rowsCount, LogQueryInfo queryInfo)
        {
            return GetLogs(TransType.JSON, userId, userPwd, startRow, rowsCount, queryInfo);
        }
        #endregion

        #region GetLogsCount
        /// <summary>
        /// 获取日志集合数量接口
        /// </summary>
        public GetCountContract GetLogsCount(TransType transType, string userId, string userPwd,
            LogQueryInfo queryInfo)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "LogService.GetLogsCount";
            string ifCode = "C012";
            var data = new GetCountContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = userId;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("user_id", userId);
                htParams.Add("user_pwd", userPwd);
                htParams.Add("query_info", queryInfo);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("密码", userPwd, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);

                // queryInfo
                htResult = ErrorService.CheckLength("日志ID", queryInfo.log_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("业务ID", queryInfo.biz_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("业务名称", queryInfo.biz_name, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("日志类型ID", queryInfo.log_type_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("日志类型代码", queryInfo.log_type_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("日志代码", queryInfo.log_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("日志内容", queryInfo.log_body, 0, 200, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("开始创建时间", queryInfo.create_time_begin, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("结束创建时间", queryInfo.create_time_end, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("创建人ID", queryInfo.create_user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("开始修改时间", queryInfo.modify_time_begin, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("结束修改时间", queryInfo.modify_time_end, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("修改人ID", queryInfo.modify_user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("客户代码", queryInfo.customer_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("客户ID", queryInfo.customer_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店代码", queryInfo.unit_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", queryInfo.unit_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("用户代码", queryInfo.user_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("用户ID", queryInfo.user_id, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("接口代码", queryInfo.if_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("平台代码", queryInfo.app_code, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                #endregion

                Hashtable htQueryInfo = new Hashtable();
                htQueryInfo.Add("LogId", queryInfo.log_id);
                htQueryInfo.Add("BizId", queryInfo.biz_id);
                htQueryInfo.Add("BizName", queryInfo.biz_name);
                htQueryInfo.Add("LogTypeId", queryInfo.log_type_id);
                htQueryInfo.Add("LogTypeCode", queryInfo.log_type_code);
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

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
                Hashtable htUser = new Hashtable();
                htUser.Add("UserId", userId);
                htUser.Add("CertPwd", userPwd);
                statusFlag = authService.Validate(htUser, ref certInfo);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A009, "用户ID与密码不匹配", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                // 获取数据
                Services.LogDBService logDBService = new Services.LogDBService();
                data.count = logDBService.GetLogsCount(htQueryInfo);

                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
            }
            catch (Exception ex)
            {
                data.status = Utils.GetStatus(false);
                data.error_code = ErrorCode.A000.ToString();
                data.error_full_desc = ex.ToString();
                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
            }
            return data;
        }

        public GetCountContract GetLogsCountJson(string userId, string userPwd,
            LogQueryInfo queryInfo)
        {
            return GetLogsCount(TransType.JSON, userId, userPwd, queryInfo);
        }
        #endregion

        #region GetLog
        /// <summary>
        /// 获取日志接口
        /// </summary>
        public GetLogContract GetLog(TransType transType, string userId, string userPwd,
            string logId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "LogService.GetLog";
            string ifCode = "C013";
            var data = new GetLogContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = userId;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("user_id", userId);
                htParams.Add("user_pwd", userPwd);
                htParams.Add("log_id", logId);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogContract>(htResult);
                htResult = ErrorService.CheckLength("密码", userPwd, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogContract>(htResult);
                htResult = ErrorService.CheckLength("日志ID", logId, 1, 32, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetLogContract>(htResult);
                #endregion

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
                Hashtable htUser = new Hashtable();
                htUser.Add("UserId", userId);
                htUser.Add("CertPwd", userPwd);
                statusFlag = authService.Validate(htUser, ref certInfo);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A009, "用户ID与密码不匹配", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                // 获取数据
                Services.LogDBService logDBService = new Services.LogDBService();
                LogInfo log = logDBService.GetLogById(logId);
                if (log != null)
                {
                    LogContract logContract = new LogContract();
                    logContract.log_id = log.LogId;
                    logContract.biz_id = log.BizId;
                    logContract.biz_name = log.BizName;
                    logContract.log_type_id = log.LogTypeId;
                    logContract.log_type_code = log.LogTypeCode;
                    logContract.log_code = log.LogCode;
                    logContract.log_body = log.LogBody;
                    logContract.create_time = log.CreateTime;
                    logContract.create_user_id = log.CreateUserId;
                    logContract.modify_time = log.ModifyTime;
                    logContract.modify_user_id = log.ModifyUserId;
                    logContract.customer_code = log.CustomerCode;
                    logContract.customer_id = log.CustomerId;
                    logContract.unit_code = log.UnitCode;
                    logContract.unit_id = log.UnitId;
                    logContract.user_code = log.UserCode;
                    logContract.user_id = log.UserId;
                    logContract.if_code = log.IfCode;
                    logContract.app_code = log.AppCode;
                    data.Log = logContract;
                }

                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
            }
            catch (Exception ex)
            {
                data.status = Utils.GetStatus(false);
                data.error_code = ErrorCode.A000.ToString();
                data.error_full_desc = ex.ToString();
                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
            }
            return data;
        }

        public GetLogContract GetLogJson(string userId, string userPwd,
            string logId)
        {
            return GetLog(TransType.JSON, userId, userPwd, logId);
        }
        #endregion
    }
}