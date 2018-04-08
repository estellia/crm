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
    public class PackageSkuProps
    {
        public BaseContract Run(PackageGenTypeMethod packageGenType,
            string batId, string customerId, string unitId, string userId)
        {
            //int pageSize = 500; // 每页数量
            AppType appType = AppType.BS;
            string bizId = Utils.NewGuid();
            string methodKey = "Task.PackageSkuProps";
            string ifCode = TaskCode.T006.ToString();
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

                var dataService = new ExchangeBsService.SkuBsService();

                string pkgTypeCode = PackageTypeMethod.SKUPROPS.ToString();
                string pkgGenTypeCode = packageGenType.ToString();
                PackageService pkgService = new PackageService();

                // 判断是否已打包
                var isPackaged = pkgService.CheckIsPackaged(customerId, pkgTypeCode);
                if (isPackaged > 0)
                {
                    data.status = Utils.GetStatus(true);
                    return data;
                }

                // 获取数据
                var items = dataService.GetSkuPropListPackage(customerId, userId, unitId);
                if (items == null || items.Count <= 0)
                {
                    data.status = Utils.GetStatus(true);
                    return data;
                }

                // 创建数据包
                Hashtable htPkg = pkgService.CreatePackage(appType, batId, pkgTypeCode,
                    customerId, null, userId, pkgGenTypeCode, null);
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

                // 生成数据包文件
                Hashtable htPkgf = pkgService.CreateSkuPropsPackageFile(
                    appType, batId, pkgId, userId, null, items);
                if (!Convert.ToBoolean(htPkgf["status"]))
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = htPkgf["error_code"].ToString();
                    data.error_full_desc = htPkgf["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    Console.WriteLine(data.error_full_desc);
                    return data;
                }
                
                // 发布数据包
                Hashtable htPkgPublish = pkgService.PublishPackage(appType, batId, pkgId, userId);
                if (!Convert.ToBoolean(htPkgPublish["status"]))
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = htPkgPublish["error_code"].ToString();
                    data.error_full_desc = htPkgPublish["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    Console.WriteLine(data.error_full_desc);
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
                Console.WriteLine(data.error_full_desc);
            }
            return data;
        }
    }
}
