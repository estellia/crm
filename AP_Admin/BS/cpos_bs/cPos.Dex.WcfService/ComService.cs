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
    /// ComService
    /// </summary>
    public class ComService : BaseService, IComService
    {
        #region UploadMonitorLog
        /// <summary>
        /// 上传 MonitorLog 信息接口
        /// </summary>
        public UploadContract UploadMonitorLog(TransType transType,
            MonitorLogContract order, string userId, string token, string unitId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "ComService.UploadMonitorLog";
            string ifCode = "C001";
            var data = new UploadContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = unitId;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = userId;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("order", order);
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                CertInfo certInfo = null;

                #region 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadContract>(htResult);
                //htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadContract>(htResult);
                #endregion

                #region 检查权限
                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User和Customer
                certInfo = authService.GetCertByUserId(userId);
                if (certInfo == null)
                {
                    htError = ErrorService.OutputError(ErrorCode.A006, "用户ID不存在", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                statusFlag = true;
                //// 检查Token是否不匹配或过期
                //statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                //if (!statusFlag)
                //{
                //    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
                //    data.status = Utils.GetStatus(false);
                //    data.error_code = htError["error_code"].ToString();
                //    data.error_full_desc = htError["error_desc"].ToString();
                //    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                //    return data;
                //}

                // 查询凭证
                certInfo = authService.GetCertByUserId(userId);
                if (certInfo == null || certInfo.CustomerId == null || certInfo.CustomerId.Length == 0)
                {
                    htError = ErrorService.OutputError(ErrorCode.A007, "获取后台数据（客户ID）失败", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                string customerId = certInfo.CustomerId;

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;
                #endregion

                #region 检查单据参数
                Dex.ServicesBs.ComService orderService = new Dex.ServicesBs.ComService();
                if (order.user_id == null || order.user_id.Trim().Length == 0)
                    order.user_id = userId;
                htError = orderService.CheckMonitorLog(order);
                if (!Convert.ToBoolean(htError["status"]))
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }
                #endregion

                // 保存
                ConfigService cfgService = new ConfigService();
                bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectPosBS)
                {
                    try
                    {
                        orderService.SaveMonitorLog(order, customerId, unitId, userId);
                    }
                    catch (Exception ex)
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = ErrorCode.A018.ToString();
                        data.error_full_desc = ex.ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }
                else
                {
                    htError = ErrorService.OutputError(ErrorCode.A012, "连接业务平台数据通道已关闭", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
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

        public UploadContract UploadMonitorLogJson(MonitorLogContract order, 
            string userId, string token, string unitId)
        {
            return UploadMonitorLog(TransType.JSON, order, userId, token, unitId);
        }
        #endregion
    }
}