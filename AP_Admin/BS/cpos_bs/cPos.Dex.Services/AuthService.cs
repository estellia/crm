using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Model.User;

namespace cPos.Dex.Services
{
    public class AuthService
    {
        #region ApplyUserCertificate
        /// <summary>
        /// D001-申请用户凭证方法
        /// </summary>
        public Hashtable ApplyUserCertificate(AppType appType, string apply_user_id, string apply_user_pwd,
            string user_id, string user_code, string customer_id, string customer_code, string user_password,
            IList<UserRoleInfo> user_role_info_list)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("ApplyUserId", apply_user_id);
            ht.Add("ApplyUserPwd", apply_user_pwd);
            ht.Add("UserId", user_id);
            ht.Add("UserCode", user_code);
            ht.Add("CustomerId", customer_id);
            ht.Add("CustomerCode", customer_code);
            ht.Add("UserPassword", user_password);

            string bizId = Utils.NewGuid();
            string methodKey = "AuthService.ApplyUserCertificate";
            string ifCode = "D001";
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = customer_code;
            htLogExt["customer_id"] = customer_id;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = apply_user_id;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = appType;
            LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), ht, apply_user_id, htLogExt);

            // 检查参数（可选步骤）
            bool paramCheckFlag = false;
            #region Check Length
            htResult = ErrorService.CheckLength("申请人ID", apply_user_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户ID", user_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户代码", user_code, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("客户ID", customer_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("客户代码", customer_code, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("密码", user_password, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            // 检查是否已经存在凭证
            if (CheckCertByCode(ht))
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A010.ToString();
                htResult["error_desc"] = "用户凭证已存在";
                return htResult;
            }

            // 插入数据
            CertInfo certInfo = new CertInfo();
            certInfo.CertId = Utils.NewGuid();
            certInfo.UserId = user_id;
            certInfo.UserCode = user_code;
            certInfo.CustomerId = customer_id;
            certInfo.CustomerCode = customer_code;
            certInfo.CertPwd = user_password;
            certInfo.CreateUserId = apply_user_id;
            certInfo.CreateTime = Utils.GetNow();
            certInfo.ModifyUserId = apply_user_id;
            certInfo.ModifyTime = Utils.GetNow();
            InsertCert(certInfo);

            // 插入或更新用户与门店关系信息
            UpdateCertUnitRelation(certInfo.CertId, certInfo.UserId, user_role_info_list, apply_user_id);

            htResult["status"] = true;
            LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), htResult, apply_user_id, htLogExt);
            return htResult;
        }
        #endregion

        #region UpdateUserCertificate
        /// <summary>
        /// 更新用户凭证方法
        /// </summary>
        public Hashtable UpdateUserCertificate(AppType appType, 
            string apply_user_id, string apply_user_pwd,
            string user_id, string user_pwd_new, string user_code_new, string user_name_new,
            IList<UserRoleInfo> user_role_info_list)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("ApplyUserId", apply_user_id);
            ht.Add("ApplyUserPwd", apply_user_pwd);
            ht.Add("UserId", user_id);
            ht.Add("user_pwd_new", user_pwd_new);
            ht.Add("user_code_new", user_code_new);
            ht.Add("user_name_new", user_name_new);

            string bizId = Utils.NewGuid();
            string methodKey = "AuthService.UpdateUserCertificate";
            string ifCode = "D014";
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = apply_user_id;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = appType;
            LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), ht, apply_user_id, htLogExt);

            // 检查参数（可选步骤）
            bool paramCheckFlag = false;
            #region Check Length
            htResult = ErrorService.CheckLength("申请人ID", apply_user_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户ID", user_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户新密码", user_pwd_new, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户新代码", user_code_new, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("用户新名称", user_name_new, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            //// 检查是否已经存在凭证
            //if (CheckCertByCode(ht))
            //{
            //    htResult["status"] = false;
            //    htResult["error_code"] = ErrorCode.A010.ToString();
            //    htResult["error_desc"] = "用户凭证已存在";
            //    return htResult;
            //}

            // 插入数据
            CertInfo certInfo = new CertInfo();
            certInfo.UserId = user_id;
            certInfo.CertPwd = user_pwd_new;
            certInfo.UserCode = user_code_new;
            certInfo.UserName = user_name_new;
            certInfo.ModifyUserId = apply_user_id;
            certInfo.ModifyTime = Utils.GetNow();
            UpdateCertByUserId(certInfo);

            // 插入或更新用户与门店关系信息
            UpdateCertUnitRelation(certInfo.CertId, certInfo.UserId, user_role_info_list, apply_user_id);

            htResult["status"] = true;
            LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), htResult, apply_user_id, htLogExt);
            return htResult;
        }
        #endregion

        /// <summary>
        /// 验证凭证是否存在
        /// </summary>
        /// <param name="ht">UserCode, CustomerCode</param>
        /// <returns>true:存在，false:不存在</returns>
        public bool CheckCertByCode(Hashtable ht)
        {
            var result = SqlMapper.Instance().QueryForObject<int>("CertInfo.CheckCertByCode", ht);
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取凭证列表
        /// </summary>
        /// <param name="ht">UserCode, CustomerCode, CertId</param>
        public IList<CertInfo> GetCerts(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<CertInfo>("CertInfo.GetCerts", ht);
        }

        /// <summary>
        /// 通过凭证ID获取凭证
        /// </summary>
        public CertInfo GetCertByCertId(string certId)
        {
            return SqlMapper.Instance().QueryForObject<CertInfo>("CertInfo.GetCertByCertId", certId);
        }

        /// <summary>
        /// 通过用户ID获取凭证
        /// </summary>
        public CertInfo GetCertByUserId(string userId)
        {
            return SqlMapper.Instance().QueryForObject<CertInfo>("CertInfo.GetCertByUserId", userId);
        }

        /// <summary>
        /// 通过用户ID和客户ID获取凭证
        /// </summary>
        public CertInfo GetCertByUserIdAndCustomerId(string userId, string customerId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("UserId", userId);
            ht.Add("CustomerId", customerId);
            return SqlMapper.Instance().QueryForObject<CertInfo>("CertInfo.GetCertByUserIdAndCustomerId", ht);
        }

        /// <summary>
        /// 插入凭证
        /// </summary>
        public bool InsertCert(CertInfo certInfo)
        {
            ConfigService cfgService = new ConfigService();
            if (certInfo.CertTypeId == null)
                certInfo.CertTypeId = cfgService.GetPosBsCertTypeCodeCfg();
            if (certInfo.CertStatus == null)
                certInfo.CertStatus = "0";
            if (certInfo.CreateTime == null)
                certInfo.CreateTime = Utils.GetNow();
            if (certInfo.ModifyTime == null)
                certInfo.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("CertInfo.InsertCert", certInfo);
            return true;
        }

        /// <summary>
        /// 更新凭证
        /// </summary>
        public bool UpdateCert(CertInfo certInfo)
        {
            SqlMapper.Instance().Update("CertInfo.UpdateCert", certInfo);
            return true;
        }

        /// <summary>
        /// 更新凭证By UserId
        /// </summary>
        public bool UpdateCertByUserId(CertInfo certInfo)
        {
            SqlMapper.Instance().Update("CertInfo.UpdateCertByUserId", certInfo);
            return true;
        }

        /// <summary>
        /// 禁用凭证By UserId
        /// </summary>
        public bool DisableCertByUserId(CertInfo certInfo)
        {
            SqlMapper.Instance().Update("CertInfo.DisableCertByUserId", certInfo);
            return true;
        }

        /// <summary>
        /// 删除凭证
        /// </summary>
        public bool DeleteCert(CertInfo certInfo)
        {
            SqlMapper.Instance().Update("CertInfo.DeleteCert", certInfo);
            return true;
        }

        /// <summary>
        /// 验证凭证
        /// </summary>
        /// <param name="ht">UserCode, CustomerCode, CertPwd, UserId, CertId</param>
        public bool Validate(Hashtable ht, ref CertInfo certInfo)
        {
            IList<CertInfo> list = null;
            list = SqlMapper.Instance().QueryForList<CertInfo>("CertInfo.GetCerts", ht);
            if (list != null && list.Count > 0)
            {
                certInfo = list[0];
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取令牌列表
        /// </summary>
        /// <param name="ht">CertId, CertTypeId, UserId, UserCode, CustomerId, CustomerCode, CertStatus, CertPwd</param>
        public IList<CertTokenInfo> GetCertTokens(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<CertTokenInfo>("CertInfo.GetCertTokens", ht);
        }

        /// <summary>
        /// 通过凭证ID获取令牌
        /// </summary>
        public CertTokenInfo GetCertTokenByCertId(string userId)
        {
            return SqlMapper.Instance().QueryForObject<CertTokenInfo>("CertInfo.GetCertTokenByCertId", userId);
        }

        /// <summary>
        /// 比较令牌
        /// </summary>
        /// <param name="token"></param>
        /// <param name="certId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckCertToken(string token, string certId, string userId)
        {
            CertTokenInfo certTokenInfo = GetCurrentCertToken(certId, userId, false);
            if (certTokenInfo == null || certTokenInfo.CertToken != token)
                return false;
            return true;
        }

        /// <summary>
        /// 获取当前令牌(可刷新令牌)
        /// </summary>
        /// <param name="certId">凭证ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="refresh">true:立即刷新，false:过期刷新</param>
        /// <returns></returns>
        public CertTokenInfo GetCurrentCertToken(string certId, string userId, bool refresh)
        {
            // 获取当前Token
            CertTokenInfo certTokenInfo = GetCertTokenByCertId(certId);

            // 没有则生成Token
            if (certTokenInfo == null)
            {
                CertTokenInfo newCertTokenInfo = new CertTokenInfo();
                newCertTokenInfo.CertId = certId;
                newCertTokenInfo.UserId = userId;
                newCertTokenInfo.CertToken = CreateTokenString();
                InsertCertToken(newCertTokenInfo);
                return newCertTokenInfo;
            }
            else // 存在，则判断是否过期
            {
                if (refresh)
                {
                    RefreshCertToken(certTokenInfo);
                    return certTokenInfo;
                }
                ConfigService configService = new ConfigService();
                int cycleTime = configService.GetCertTokenCycleTimeCfg();
                certTokenInfo.CycleTime = cycleTime;
                DateTime dtUpdateTime = Convert.ToDateTime(certTokenInfo.UpdateTime);
                DateTime dtEndTime = dtUpdateTime.AddMilliseconds(cycleTime);
                DateTime dtNow = DateTime.Now;
                if (dtNow > dtEndTime) // 过期
                {
                    certTokenInfo.IsOverTime = true;
                }
                else
                {
                    certTokenInfo.IsOverTime = false;
                    RefreshCertToken(certTokenInfo);
                }
            }
            return certTokenInfo;
        }

        public CertTokenInfo RefreshCertToken(CertTokenInfo obj)
        {
            obj.UpdateTime = Utils.GetNow();
            UpdateCertToken(obj);
            return obj;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        public string CreateTokenString()
        {
            return Utils.NewGuid();
        }

        /// <summary>
        /// 插入凭证令牌
        /// </summary>
        public bool InsertCertToken(CertTokenInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.UpdateTime == null)
                info.UpdateTime = Utils.GetNow();

            SqlMapper.Instance().Insert("CertInfo.InsertCertToken", info);
            return true;
        }

        /// <summary>
        /// 更新凭证令牌
        /// </summary>
        public bool UpdateCertToken(CertTokenInfo info)
        {
            if (info.UpdateTime == null)
                info.UpdateTime = Utils.GetNow();

            SqlMapper.Instance().Update("CertInfo.UpdateCertToken", info);
            return true;
        }

        /// <summary>
        /// 插入或更新用户与门店关系信息
        /// </summary>
        public void UpdateCertUnitRelation(string certId, string userId,
            IList<UserRoleInfo> unitList, string applyUserId)
        {
            DeleteCertUnitsByUserId(userId);

            if (unitList != null && unitList.Count > 0)
            {
                foreach (var item in unitList)
                {
                    if (item != null)
                    {
                        var obj = new CertUnitInfo();
                        obj.CertId = certId;
                        obj.UserId = userId;
                        obj.UnitId = item.UnitId;
                        //obj.UnitCode = item.UnitCode;
                        obj.UnitName = item.UnitName;
                        obj.CreateUserId = applyUserId;
                        obj.ModifyUserId = applyUserId;
                        InsertCertUnit(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 插入用户与门店关系
        /// </summary>
        public bool InsertCertUnit(CertUnitInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("CertInfo.InsertCertUnit", info);
            return true;
        }

        /// <summary>
        /// 删除用户与门店的所有关系
        /// </summary>
        public bool DeleteCertUnitsByUserId(string userId)
        {
            SqlMapper.Instance().Delete("CertInfo.DeleteCertUnitsByUserId", userId);
            return true;
        }

        /// <summary>
        /// 更新凭证密码
        /// </summary>
        public bool UpdateCertPwdByUserId(string userId, string newPassword)
        {
            CertInfo certInfo = new CertInfo();
            certInfo.UserId = userId;
            certInfo.CertPwd = newPassword;
            SqlMapper.Instance().Update("CertInfo.UpdateCertByUserId", certInfo);
            return true;
        }
    }
}
