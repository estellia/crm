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
using Jayrock.Json;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    /// <summary>
    /// AuthService
    /// </summary>
    public class AuthService : BaseService, IAuthService
    {
        #region Validate
        /// <summary>
        /// 用户凭证验证
        /// </summary>
        public ValidateContract Validate(TransType transType, string userCode, 
            string customerCode, string password, string type)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "AuthService.Validate";
            string ifCode = "C003";
            var data = new ValidateContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = customerCode;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = userCode;
            htLogExt["user_id"] = null;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("user_code", userCode);
                htParams.Add("customer_code", customerCode);
                htParams.Add("password", password);
                htParams.Add("type", type);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, null, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                string userId = string.Empty;
                string token = string.Empty;
                CertInfo certInfo = null;

                if (type == null || type == string.Empty)
                {
                    type = CertType.POS.ToString();
                }

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户代码", userCode, 1, 40, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<ValidateContract>(htResult);
                htResult = ErrorService.CheckLength("用户密码", password, 1, 40, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<ValidateContract>(htResult);

                if (type == CertType.POS.ToString())
                {
                    htResult = ErrorService.CheckLength("客户代码", customerCode, 1, 40, true, false, ref paramCheckFlag);
                    if (!paramCheckFlag) return ErrorConvert.Export<ValidateContract>(htResult);
                }
                else
                {
                    htResult = ErrorService.CheckLength("客户代码", customerCode, 0, 40, true, false, ref paramCheckFlag);
                    if (!paramCheckFlag) return ErrorConvert.Export<ValidateContract>(htResult);
                }
                #endregion

                // 查询用户或客户是否存在
                //certInfo = authService.GetCertByUserId(userId);
                //if (certInfo == null)
                //{
                //    htError = ErrorService.OutputError(ErrorCode.A006, "用户ID不存在", true);
                //    data.status = Utils.GetStatus(false);
                //    data.error_code = htError["error_code"].ToString();
                //    data.error_desc = htError["error_desc"].ToString();
                //    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId);
                //    return data;
                //}

                // 查询凭证
                Hashtable htCert = new Hashtable();
                htCert["UserCode"] = userCode;
                htCert["CustomerCode"]= customerCode;
                htCert["CertPwd"] = password;
                if (type == CertType.MOBILE.ToString())
                {
                    htCert["CustomerCode"] = null;
                    htCert["CertTypeCode"] = type.ToLower();
                }
                statusFlag = authService.Validate(htCert, ref certInfo);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A009, "用户代码与密码/用户代码与客户代码不匹配", true);
                    if (type == CertType.MOBILE.ToString())
                    {
                        htError = ErrorService.OutputError(ErrorCode.A009, "用户代码与密码不匹配", true);
                    }
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                }
                else
                {
                    userId = certInfo.UserId;
                    // 获取令牌
                    var certTokenInfo = GetCertToken(certInfo.CertId, certInfo.UserId, true);
                    token = certTokenInfo.CertToken;
                    data.user_id = userId;
                    data.token = token;
                    htLogExt["user_id"] = userId;
                }

                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
            }
            catch (Exception ex)
            {
                data.status = Utils.GetStatus(false);
                data.error_code = ErrorCode.A000.ToString();
                data.error_full_desc = ex.ToString();
                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), null, htLogExt);
            }
            return data;
        }

        public ValidateContract ValidateJson(string userCode, string customerCode, 
            string password, string type)
        {
            return Validate(TransType.JSON, userCode, customerCode, password, type);
        }
        #endregion

        #region GetCertToken
        /// <summary>
        /// GetCertToken
        /// </summary>
        public CertTokenInfo GetCertToken(string userId)
        {
            CertInfo certInfo = null;
            Dex.Services.AuthService service = new Services.AuthService();
            certInfo = service.GetCertByUserId(userId);
            var contract = Validate(TransType.JSON, certInfo.UserCode, 
                certInfo.CustomerCode, certInfo.CertPwd, string.Empty);
            CertTokenInfo certTokenInfo = null;
            certTokenInfo = service.GetCertTokenByCertId(certInfo.CertId);
            return certTokenInfo;
        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// 修改用户密码接口
        /// </summary>
        public BaseContract ChangePassword(TransType transType,
            string userId, string token, string unitId, string newPassword)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "AuthService.ChangePassword";
            string ifCode = "C029";
            var data = new BaseContract();
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
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
                htParams.Add("new_password", newPassword);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, null, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                CertInfo certInfo = null;

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<BaseContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<BaseContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<BaseContract>(htResult);
                htResult = ErrorService.CheckLength("用户新密码", newPassword, 1, 40, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<BaseContract>(htResult);
                #endregion

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
                // 检查Token是否不匹配或过期
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

                // 提交
                var bsAuthService = new ServicesBs.AuthService();
                bsAuthService.ChangePassword(customerId, unitId, userId, newPassword);
                authService.UpdateCertPwdByUserId(userId, newPassword);

                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
            }
            catch (Exception ex)
            {
                data.status = Utils.GetStatus(false);
                data.error_code = ErrorCode.A000.ToString();
                data.error_full_desc = ex.ToString();
                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), null, htLogExt);
            }
            return data;
        }

        public BaseContract ChangePasswordJson(string userId, string token, string unitId, string newPassword)
        {
            return ChangePassword(TransType.JSON, userId, token, unitId, newPassword);
        }
        #endregion
    }
}