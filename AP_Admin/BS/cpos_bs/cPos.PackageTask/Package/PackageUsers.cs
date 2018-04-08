using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Common;
using cPos.Dex.Services;
using cPos.Dex;
using cPos.Dex.ContractModel;

namespace cPos.PackageTask
{
    public class PackageUsers
    {
        public BaseContract Run(PackageGenTypeMethod packageGenType, 
            string batId, string customerId, string unitId, string userId)
        {
            AppType appType = AppType.BS;
            string bizId = Utils.NewGuid();
            string methodKey = "Task.PackageUsers";
            string ifCode = TaskCode.T001.ToString();
            var data = new BaseContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = null;
            htLogExt["customer_id"] = customerId;
            htLogExt["unit_code"] = null;
            htLogExt["unit_id"] = unitId;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = userId;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = appType.ToString();
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("package_gen_type", packageGenType);
                htParams.Add("bat_id", batId);
                htParams.Add("customer_id", customerId);
                htParams.Add("unit_id", unitId);
                htParams.Add("user_id", userId);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("客户ID", customerId, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) throw new Exception(string.Format("批次{0}：客户ID不能为空", batId));
                htResult = ErrorService.CheckLength("门店ID", unitId, 0, 50, true, true, ref paramCheckFlag);
                if (!paramCheckFlag) throw new Exception(string.Format("批次{0}：门店ID不能为空", batId));
                #endregion

                htLogExt["customer_id"] = customerId;

                // 循环生成数据包
                int count = 1000; // 获取总数量
                //int pageSize = 500; // 每页数量
                int num = 0;

                while (num <= count)
                {
                    ExchangeBsService.UserAuthService bsUserAuthService = new ExchangeBsService.UserAuthService();
                    cPos.Model.BaseInfo usersInfo = bsUserAuthService.GetUserBaseByUserId(userId, customerId, unitId);
                    string pkgBatId = Utils.NewGuid();
                    string pkgTypeCode = PackageTypeMethod.USERS.ToString();
                    string pkgGenTypeCode = packageGenType.ToString();
                    PackageService pkgService = new PackageService();
                    Hashtable htPkg = pkgService.CreatePackage(appType, pkgBatId, pkgTypeCode,
                        customerId, unitId, userId, pkgGenTypeCode, null, null, AppType.POS.ToString());
                    if (!Convert.ToBoolean(htPkg["status"]))
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = htPkg["error_code"].ToString();
                        data.error_full_desc = htPkg["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        Console.WriteLine(data.error_full_desc);
                        return data;
                    }
                    string pkgId = htPkg["package_id"].ToString();
                    Hashtable htPkgf = pkgService.CreateUsersProfilePackageFile(
                        AppType.Client, pkgBatId, pkgId, userId, null,
                        usersInfo.CurrMenuInfoList, usersInfo.CurrRoleInfoList,
                        usersInfo.CurrRoleMenuInfoList, usersInfo.CurrSalesUserInfoList,
                        usersInfo.CurrSalesUserRoleInfoList);
                    if (!Convert.ToBoolean(htPkgf["status"]))
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = htPkgf["error_code"].ToString();
                        data.error_full_desc = htPkgf["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        Console.WriteLine(data.error_full_desc);
                        return data;
                    }
                    Hashtable htPkgPublish = pkgService.PublishPackage(AppType.Client, pkgBatId, pkgId, userId);
                    if (!Convert.ToBoolean(htPkgPublish["status"]))
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = htPkgPublish["error_code"].ToString();
                        data.error_full_desc = htPkgPublish["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        Console.WriteLine(data.error_full_desc);
                        return data;
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
                Console.WriteLine(data.error_full_desc);
            }
            return data;
        }
    }
}
