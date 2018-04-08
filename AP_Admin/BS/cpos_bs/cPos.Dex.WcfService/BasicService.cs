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
using cPos.Model;
using cPos.Model.User;

namespace cPos.Dex.WcfService
{
    /// <summary>
    /// BasicService
    /// </summary>
    public class BasicService : BaseService, IBasicService
    {
        #region GetPackages
        /// <summary>
        /// 获取数据包列表
        /// </summary>
        public GetPackagesContract GetPackages(TransType transType, string userId, string token,
            string unitId, string pkgSeq, string pkgtCode, long startRow, long rowsCount)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetPackages";
            string ifCode = "C010";
            var data = new GetPackagesContract();
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
                htParams.Add("pkg_seq", pkgSeq);
                htParams.Add("pkgt_code", pkgtCode);
                htParams.Add("start_row", startRow);
                htParams.Add("rows_count", rowsCount);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                if (pkgtCode != PackageTypeMethod.MOBILE.ToString())
                {
                    htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                    if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                }
                htResult = ErrorService.CheckLength("包开始序号", pkgSeq, 0, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                htResult = ErrorService.CheckLength("包类型代码", pkgtCode, 0, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                htResult = ErrorService.CheckLength("起始行索引", startRow, 1, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                //htResult = ErrorService.CheckLength("返回行数量", rowsCount, 1, 10, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                if (rowsCount == null || rowsCount <= 0) rowsCount = Config.QueryDBMaxCount;
                #endregion

                #region 检查数值范围
                htResult = ErrorService.CheckNumArrange("起始行索引", startRow, 0, Config.TopMaxVal, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                //htResult = ErrorService.CheckNumArrange("返回行数量", rowsCount, 0, Config.QueryDBMaxCount, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesContract>(htResult);
                #endregion

                if (pkgSeq == null || pkgSeq.ToString().Trim().Length == 0)
                {
                    pkgSeq = "0";
                }

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User和Customer
                certInfo = authService.GetCertByUserId(userId);

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

                // 检查User
                //certInfo = authService.GetCertByUserId(userId);
                if (certInfo == null)
                {
                    htError = ErrorService.OutputError(ErrorCode.A006, "用户ID不存在", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }
                // 检查客户
                if (pkgtCode != PackageTypeMethod.MOBILE.ToString())
                {
                    if (certInfo.CustomerId == null || certInfo.CustomerId.Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A007, "获取后台数据（客户ID）失败", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }

                string customerId = certInfo.CustomerId;

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;

                // 获取FTP及账户
                string ftpIp = string.Empty;
                string ftpAccountName = string.Empty;
                string ftpAccountPwd = string.Empty;
                ConfigService configServcie = new ConfigService();
                ServerService serverService = new ServerService();
                ServerFtpInfo ftpInfo = serverService.GetActiveServerFtp();
                string accountType = configServcie.GetFtpServerReadAccountType();
                ServerAccountTypeInfo accountTypeInfo = serverService.GetServerAccountTypeByCode(accountType);
                ServerAccountInfo accountInfo = serverService.GetActiveServerAccount(
                    ftpInfo.ServerId, accountTypeInfo.AccountTypeId);
                ftpIp = ftpInfo.FtpIp;
                ftpAccountName = accountInfo.AccountName;
                ftpAccountPwd = accountInfo.AccountPwd;

                // 查询
                Hashtable htQuery = new Hashtable();
                PackageService pkgService = new PackageService();
                PackageTypeInfo pkgTypeInfo = null;
                if (pkgtCode != null && pkgtCode.Trim().Length > 0)
                {
                    pkgTypeInfo = pkgService.GetPackageTypeByCode(pkgtCode);
                    if (pkgTypeInfo == null || pkgTypeInfo.PkgTypeId == null)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A006, "包类型代码不存在", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }

                if (pkgTypeInfo != null)
                {
                    htQuery["PkgTypeId"] = pkgTypeInfo.PkgTypeId;
                }
                if (pkgtCode == null || pkgtCode.Trim().Length == 0)
                {
                    AppTypeInfo appTypeInfo = pkgService.GetAppTypeByCode(AppType.POS.ToString());
                    htQuery["AppTypeId"] = appTypeInfo.AppTypeId;
                }
                else if (pkgtCode == PackageTypeMethod.MOBILE.ToString())
                {
                    AppTypeInfo appTypeInfo = pkgService.GetAppTypeByCode(pkgtCode);
                    if (appTypeInfo == null)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A006, "未查找到应用程序类型", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                    htQuery["AppTypeId"] = appTypeInfo.AppTypeId;
                }
                htQuery["UnitId"] = unitId;
                htQuery["CustomerId"] = customerId;
                htQuery["PkgSeq"] = pkgSeq;
                htQuery["PkgStatus"] = "1";
                htQuery["StartRow"] = startRow;
                htQuery["RowsCount"] = rowsCount;
                if (pkgtCode == PackageTypeMethod.MOBILE.ToString())
                {
                    htQuery["UserId"] = userId;
                }
                var zipFilePathList = new List<string>();
                IList<PackageInfo> packages = pkgService.GetPackages(htQuery);
                data.packages = new List<Package>();
                if (packages != null)
                {
                    foreach (var package in packages)
                    {
                        Package pkgObj = new Package();
                        pkgObj.package_id = package.PkgId;
                        pkgObj.package_type_id = package.PkgTypeId;
                        pkgObj.package_type_code = package.PkgTypeCode;
                        pkgObj.package_gen_type_id = package.PkgGenTypeId;
                        pkgObj.package_gen_type_code = package.PkgGenTypeCode;
                        pkgObj.unit_id = package.UnitId;
                        pkgObj.package_name = package.PkgName;
                        pkgObj.package_desc = package.PkgDesc;
                        pkgObj.package_seq = package.PkgSeq.ToString();
                        pkgObj.package_status = package.PkgStatus;
                        pkgObj.package_files_count = 0;
                        #region package_files
                        pkgObj.package_files = new List<PackageFile>();
                        Hashtable htQueryFiles = new Hashtable();
                        htQueryFiles.Add("PkgId", package.PkgId);
                        htQueryFiles.Add("PkgfStatus", "0");
                        IList<PackageFileInfo> pkgfInfoList = pkgService.GetPackageFiles(htQueryFiles);
                        if (pkgfInfoList != null)
                        {
                            pkgObj.package_files_count = pkgfInfoList.Count;
                            foreach (var pkgfInfo in pkgfInfoList)
                            {
                                PackageFile pkgfObj = new PackageFile();
                                pkgfObj.file_id = pkgfInfo.PkgfId;
                                pkgfObj.package_id = pkgfInfo.PkgId;
                                pkgfObj.file_name = pkgfInfo.PkgfName;
                                pkgfObj.file_seq = pkgfInfo.PkgfSeq.ToString();
                                pkgfObj.file_path = pkgfInfo.UrlPath;
                                pkgfObj.file_status = pkgfInfo.PkgfStatus;
                                pkgfObj.ftp_ip = ftpIp;
                                pkgfObj.ftp_account_name = ftpAccountName;
                                pkgfObj.ftp_account_pwd = ftpAccountPwd;
                                pkgObj.package_files.Add(pkgfObj);

                                if (pkgfObj.file_path != null && pkgfObj.file_path.Trim().Length > 0)
                                {
                                    zipFilePathList.Add(pkgfObj.file_path.Replace("/", "\\"));
                                }
                            }
                        }
                        #endregion
                        data.packages.Add(pkgObj);
                    }
                }

                // Zip files
                if (zipFilePathList.Count > 20)
                {
                    ConfigService cfgService = new ConfigService();
                    string fileServerFolder = cfgService.GetFileServerFolder();
                    string zipFileName = Utils.NewGuid();
                    string zipFilePath = string.Format(@"/init/{0}.zip", zipFileName);
                    string zipFileTempPath = string.Format(@"/init_temp/{0}.zip", zipFileName);
                    string error = "";
                    var tmp = cPos.Dex.WcfServiceCommon.Common.ZipFile(
                        zipFilePathList.ToArray(),
                        fileServerFolder + zipFileTempPath.Replace("/", "\\"),
                        fileServerFolder,
                        ref error);
                    if (tmp)
                    {
                        File.Copy(fileServerFolder + zipFileTempPath.Replace("/", "\\"),
                           fileServerFolder + zipFilePath.Replace("/", "\\"));
                    }
                    else
                    {
                        throw new Exception("压缩文件出错:" + error);
                    }
                    data.zip_file = zipFilePath;
                    data.ftp_ip = ftpIp;
                    data.ftp_account_name = ftpAccountName;
                    data.ftp_account_pwd = ftpAccountPwd;
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

        public GetPackagesContract GetPackagesJson(string userId, string token,
            string unitId, string pkgSeq, string pkgtCode, long startRow, long rowsCount)
        {
            return GetPackages(TransType.JSON, userId, token,
                unitId, pkgSeq, pkgtCode, startRow, rowsCount);
        }
        #endregion

        #region GetPackagesCount
        /// <summary>
        /// 获取数据包数量
        /// </summary>
        public GetPackagesCountContract GetPackagesCount(
            TransType transType, string userId, string token,
            string unitId, string pkgSeq, string pkgtCode)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetPackagesCount";
            string ifCode = "C012";
            var data = new GetPackagesCountContract();
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
                htParams.Add("pkg_seq", pkgSeq);
                htParams.Add("pkgt_code", pkgtCode);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesCountContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesCountContract>(htResult);
                if (pkgtCode != PackageTypeMethod.MOBILE.ToString())
                {
                    htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                    if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesCountContract>(htResult);
                }
                htResult = ErrorService.CheckLength("包开始序号", pkgSeq, 0, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesCountContract>(htResult);
                htResult = ErrorService.CheckLength("包类型代码", pkgtCode, 0, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackagesCountContract>(htResult);
                #endregion

                if (pkgSeq == null || pkgSeq.ToString().Trim().Length == 0)
                    pkgSeq = "0";

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
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

                // 检查客户
                //certInfo = authService.GetCertByUserId(userId);
                if (pkgtCode != PackageTypeMethod.MOBILE.ToString())
                {
                    if (certInfo.CustomerId == null || certInfo.CustomerId.Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A007, "获取后台数据（客户ID）失败", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }

                string customerId = certInfo.CustomerId;

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;

                // 查询
                Hashtable htQuery = new Hashtable();
                PackageService pkgService = new PackageService();
                PackageTypeInfo pkgTypeInfo = null;
                if (pkgtCode != null && pkgtCode.Trim().Length > 0)
                {
                    pkgTypeInfo = pkgService.GetPackageTypeByCode(pkgtCode);
                    if (pkgTypeInfo == null || pkgTypeInfo.PkgTypeId == null)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A006, "包类型代码不存在", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }

                if (pkgTypeInfo != null)
                {
                    htQuery.Add("PkgTypeId", pkgTypeInfo.PkgTypeId);
                }
                if (pkgtCode == null || pkgtCode.Trim().Length == 0)
                {
                    AppTypeInfo appTypeInfo = pkgService.GetAppTypeByCode(AppType.POS.ToString());
                    htQuery["AppTypeId"] = appTypeInfo.AppTypeId;
                }
                else if (pkgtCode == PackageTypeMethod.MOBILE.ToString())
                {
                    AppTypeInfo appTypeInfo = pkgService.GetAppTypeByCode(pkgtCode);
                    if (appTypeInfo == null)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A006, "未查找到应用程序类型", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                    htQuery["AppTypeId"] = appTypeInfo.AppTypeId;
                }
                htQuery.Add("UnitId", unitId);
                htQuery.Add("CustomerId", customerId);
                htQuery.Add("PkgSeq", pkgSeq);
                htQuery.Add("PkgStatus", "1");
                data.count = pkgService.GetPackagesCount(htQuery);

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

        public GetPackagesCountContract GetPackagesCountJson(string userId, string token,
            string unitId, string pkgSeq, string pkgtCode)
        {
            return GetPackagesCount(TransType.JSON, userId, token,
                unitId, pkgSeq, pkgtCode);
        }
        #endregion

        #region GetPackageFiles
        /// <summary>
        /// 获取单个数据包中文件列表接口
        /// </summary>
        public GetPackageFilesContract GetPackageFiles(TransType transType, string userId, string token,
            string unitId, string packageId, long startRow, long rowsCount)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetPackageFiles";
            string ifCode = "C011";
            var data = new GetPackageFilesContract();
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
                htParams.Add("package_id", packageId);
                htParams.Add("start_row", startRow);
                htParams.Add("rows_count", rowsCount);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                htResult = ErrorService.CheckLength("包ID", packageId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                htResult = ErrorService.CheckLength("起始行索引", startRow, 1, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                htResult = ErrorService.CheckLength("返回行数量", rowsCount, 1, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                #endregion

                #region 检查数值范围
                htResult = ErrorService.CheckNumArrange("起始行索引", startRow, 0, Config.TopMaxVal, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                htResult = ErrorService.CheckNumArrange("返回行数量", rowsCount, 0, Config.QueryDBMaxCount, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesContract>(htResult);
                #endregion

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

                // 获取FTP及账户
                string ftpIp = string.Empty;
                string ftpAccountName = string.Empty;
                string ftpAccountPwd = string.Empty;
                ConfigService configServcie = new ConfigService();
                ServerService serverService = new ServerService();
                ServerFtpInfo ftpInfo = serverService.GetActiveServerFtp();
                string accountType = configServcie.GetFtpServerReadAccountType();
                ServerAccountTypeInfo accountTypeInfo = serverService.GetServerAccountTypeByCode(accountType);
                ServerAccountInfo accountInfo = serverService.GetActiveServerAccount(
                    ftpInfo.ServerId, accountTypeInfo.AccountTypeId);
                ftpIp = ftpInfo.FtpIp;
                ftpAccountName = accountInfo.AccountName;
                ftpAccountPwd = accountInfo.AccountPwd;

                // 查询
                Hashtable htQuery = new Hashtable();
                PackageService pkgService = new PackageService();
                htQuery.Add("PkgId", packageId);
                htQuery.Add("PkgfStatus", "0");
                htQuery.Add("StartRow", startRow);
                htQuery.Add("RowsCount", rowsCount);
                IList<PackageFileInfo> packageFiles = pkgService.GetPackageFiles(htQuery);
                data.package_files = new List<PackageFile>();
                if (packageFiles != null)
                {
                    foreach (var packageFile in packageFiles)
                    {
                        PackageFile pkgfObj = new PackageFile();
                        pkgfObj.file_id = packageFile.PkgfId;
                        pkgfObj.package_id = packageFile.PkgId;
                        pkgfObj.file_name = packageFile.PkgfName;
                        pkgfObj.file_seq = packageFile.PkgfSeq.ToString();
                        pkgfObj.file_path = packageFile.UrlPath;
                        pkgfObj.file_status = packageFile.PkgfStatus;
                        pkgfObj.ftp_ip = ftpIp;
                        pkgfObj.ftp_account_name = ftpAccountName;
                        pkgfObj.ftp_account_pwd = ftpAccountPwd;
                        data.package_files.Add(pkgfObj);
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

        public GetPackageFilesContract GetPackageFilesJson(string userId, string token,
            string unitId, string packageId, long startRow, long rowsCount)
        {
            return GetPackageFiles(TransType.JSON, userId, token,
                unitId, packageId, startRow, rowsCount);
        }
        #endregion

        #region GetPackageFilesCount
        /// <summary>
        /// 获取单个数据包中文件数量接口
        /// </summary>
        public GetPackageFilesCountContract GetPackageFilesCount(
            TransType transType, string userId, string token,
            string unitId, string packageId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetPackageFilesCount";
            string ifCode = "C013";
            var data = new GetPackageFilesCountContract();
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
                htParams.Add("package_id", packageId);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesCountContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesCountContract>(htResult);
                htResult = ErrorService.CheckLength("包ID", packageId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPackageFilesCountContract>(htResult);
                #endregion

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
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

                // 获取FTP及账户
                string ftpIP = string.Empty;
                string ftpAccountName = string.Empty;
                string ftpAccountPwd = string.Empty;

                ConfigService configServcie = new ConfigService();
                ServerService serverService = new ServerService();
                ServerFtpInfo ftpInfo = serverService.GetActiveServerFtp();
                string accountType = configServcie.GetFtpServerReadAccountType();
                ServerAccountTypeInfo accountTypeInfo = serverService.GetServerAccountTypeByCode(accountType);
                ServerAccountInfo accountInfo = serverService.GetActiveServerAccount(
                    ftpInfo.ServerId, accountTypeInfo.AccountTypeId);
                ftpIP = ftpInfo.FtpIp;
                ftpAccountName = accountInfo.AccountName;
                ftpAccountPwd = accountInfo.AccountPwd;

                // 查询
                Hashtable htQuery = new Hashtable();
                PackageService pkgService = new PackageService();
                htQuery.Add("PkgId", packageId);
                htQuery.Add("PkgfStatus", "0");
                int count = pkgService.GetPackageFilesCount(htQuery);
                data.count = count;

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

        public GetPackageFilesCountContract GetPackageFilesCountJson(string userId, string token,
            string unitId, string packageId)
        {
            return GetPackageFilesCount(TransType.JSON, userId, token,
                unitId, packageId);
        }
        #endregion

        #region CheckVersion
        /// <summary>
        /// 检查终端版本更新接口
        /// </summary>
        public CheckVersionContract CheckVersion(TransType transType, string userId, string token,
            string unitId, string posId, string version, string dbVersion)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.CheckVersion";
            string ifCode = "C004";
            var data = new CheckVersionContract();
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
                htParams.Add("pos_id", posId);
                htParams.Add("version", version);
                htParams.Add("db_version", dbVersion);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                htResult = ErrorService.CheckLength("终端ID", posId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                htResult = ErrorService.CheckLength("终端版本号", version, 1, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                #endregion

                #region 验证权限
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

                // 查询
                Hashtable htQuery = new Hashtable();
                var basicService = new Dex.Services.BasicService();
                var configService = new Dex.Services.ConfigService();
                Dex.Model.PosVersionInfo posVersionInfo = basicService.GetLastPubPosVersion(version);
                if (posVersionInfo != null)
                {
                    PosVersionContract versionObj = new PosVersionContract();
                    versionObj.version_id = posVersionInfo.VersionId;
                    versionObj.version_no = posVersionInfo.VersionNo;
                    //versionObj.version_path = posVersionInfo.VersionPath;
                    //versionObj.version_url = posVersionInfo.VersionUrl;
                    //versionObj.file_name = posVersionInfo.FileName;
                    //versionObj.version_size = Utils.GetStrVal(posVersionInfo.VersionSize);
                    versionObj.remark = posVersionInfo.Remark;
                    data.pos_version = versionObj;

                    // files
                    IList<PosVersionFileContract> files = new List<PosVersionFileContract>();
                    if (posVersionInfo.VersionPath != null && posVersionInfo.VersionPath.Trim().Length > 0)
                    {
                        if (posVersionInfo.VersionUrl == null || posVersionInfo.VersionUrl.Trim().Length == 0)
                             throw new Exception("未找到版本URL路径");
                        if (!posVersionInfo.VersionUrl.EndsWith(@"/")) posVersionInfo.VersionUrl += @"/";

                        string folderPath = configService.GetPosVersionFolder() + posVersionInfo.VersionPath;
                        folderPath = folderPath.Replace(@"\\", @"\");
                        if (!folderPath.EndsWith(@"\")) folderPath += @"\";

                        GetFilesByFolder(folderPath, posVersionInfo.VersionUrl, versionObj, ref files); 
                    }
                    versionObj.files = files;
                    versionObj.files_count = versionObj.files.Count;
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

        public CheckVersionContract CheckVersionJson(string userId, string token,
            string unitId, string posId, string version, string dbVersion)
        {
            return CheckVersion(TransType.JSON, userId, token,
                unitId, posId, version, dbVersion);
        }

        private void GetFilesByFolder(string folderPath, string url,
            PosVersionContract versionObj, 
            ref IList<PosVersionFileContract> files)
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);
            if (!d.Exists) throw new Exception("版本文件夹不存在");
            foreach (FileInfo f in d.GetFiles("*.*", SearchOption.AllDirectories))
            {
                var tmpFileUrl = f.FullName.Replace(folderPath, string.Empty).Replace(@"\", @"/");
                var versionFileObj = new PosVersionFileContract();
                versionFileObj.version_id = versionObj.version_id;
                versionFileObj.file_id = Utils.NewGuid();
                //versionFileObj.Extension = f.Extension;
                versionFileObj.file_name = f.Name;
                versionFileObj.file_size = f.Length.ToString();
                versionFileObj.file_url = url + (url.EndsWith(@"/") ? tmpFileUrl : @"/" + tmpFileUrl);
                versionFileObj.remark = string.Empty;
                files.Add(versionFileObj);

                //foreach (DirectoryInfo subDir in d.GetDirectories("*.*", SearchOption.TopDirectoryOnly))
                //{
                //    GetFilesByFolder(subDir.FullName, url, versionObj, ref files);
                //}
            }
        }
        #endregion

        #region GetUnitXmlConfigInfo
        /// <summary>
        /// 获取门店终端XML配置信息接口
        /// </summary>
        public GetUnitXmlConfigInfoContract GetUnitXmlConfigInfo(TransType transType, 
            string customerCode, string unitCode, string posSn)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetUnitXmlConfigInfo";
            string ifCode = "C014";
            var data = new GetUnitXmlConfigInfoContract();
            Hashtable htLogExt = new Hashtable();
            htLogExt["customer_code"] = customerCode;
            htLogExt["customer_id"] = null;
            htLogExt["unit_code"] = unitCode;
            htLogExt["unit_id"] = null;
            htLogExt["user_code"] = null;
            htLogExt["user_id"] = null;
            htLogExt["if_code"] = ifCode;
            htLogExt["app_code"] = AppType.Client;
            try
            {
                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("customer_code", customerCode);
                htParams.Add("unit_code", unitCode);
                htParams.Add("pos_sn", posSn);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, null, htLogExt);

                bool statusFlag = true;
                //Hashtable htError = null;
                //CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                //htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                htResult = ErrorService.CheckLength("客户号", customerCode, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUnitXmlConfigInfoContract>(htResult);
                htResult = ErrorService.CheckLength("门店号", unitCode, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUnitXmlConfigInfoContract>(htResult);
                htResult = ErrorService.CheckLength("终端序列号", posSn, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUnitXmlConfigInfoContract>(htResult);
                #endregion

                // 查询
                Hashtable htQuery = new Hashtable();
                var basicService = new Dex.ServicesBs.BasicService();
                var cfgService = new Dex.Services.ConfigService();

                var pcInitInfo = basicService.GetPCInitInfo(customerCode, unitCode, posSn);
                var customerId = pcInitInfo.customerInfo.ID;
                var customerName = pcInitInfo.customerInfo.Name;
                var unitInfo = pcInitInfo.unitInfo;
                var warehouseInfo = pcInitInfo.warehouseInfo;
                string unitDate = pcInitInfo.posUnitInfo.AllocateTime.ToString("yyyy-MM-dd");
                string ccType = "CC";
                string posNo = pcInitInfo.posUnitInfo.PosNo;
                string posId = pcInitInfo.posUnitInfo.Pos.ID;

                string wcfIP = cfgService.GetIP();
                string wsUrl = string.Format("https://{0}/", wcfIP);
                string uploadTime = "2";
                string userId = pcInitInfo.userInfo.User_Id;

                if (pcInitInfo.posUnitInfo.Pos.WS != null && pcInitInfo.posUnitInfo.Pos.WS.Trim().Length > 0)
                {
                    wsUrl = pcInitInfo.posUnitInfo.Pos.WS.Trim();
                }
                else if (pcInitInfo.posUnitInfo.Pos.WS2 != null && pcInitInfo.posUnitInfo.Pos.WS2.Trim().Length > 0)
                {
                    wsUrl = pcInitInfo.posUnitInfo.Pos.WS2.Trim();
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<UnitConfiguration></UnitConfiguration>");
                XmlNode cfgNode = doc.SelectSingleNode("/UnitConfiguration");

                #region Customer
                XmlNode customerNode = doc.CreateElement("Data");
                XmlAttribute customerNodeNameAtt = doc.CreateAttribute("name");
                customerNodeNameAtt.Value = "Customer";
                customerNode.Attributes.Append(customerNodeNameAtt);

                XmlNode customerIdNode = doc.CreateElement("Customer_id");
                customerIdNode.InnerText = customerId;
                customerNode.AppendChild(customerIdNode);

                XmlNode customerNameNode = doc.CreateElement("Customer_name");
                customerNameNode.InnerText = customerName;
                customerNode.AppendChild(customerNameNode);

                XmlNode customerCodeNode = doc.CreateElement("Customer_code");
                customerCodeNode.InnerText = customerCode;
                customerNode.AppendChild(customerCodeNode);

                cfgNode.AppendChild(customerNode);
                #endregion

                #region Unit
                XmlNode unitNode = doc.CreateElement("Data");
                XmlAttribute unitNodeNameAtt = doc.CreateAttribute("name");
                unitNodeNameAtt.Value = "Unit";
                unitNode.Attributes.Append(unitNodeNameAtt);

                XmlNode unitIdNode = doc.CreateElement("unit_id");
                unitIdNode.InnerText = unitInfo.Id;
                unitNode.AppendChild(unitIdNode);

                XmlNode typeIdNode = doc.CreateElement("type_id");
                typeIdNode.InnerText = unitInfo.TypeId;
                unitNode.AppendChild(typeIdNode);

                XmlNode unitCodeNode = doc.CreateElement("unit_code");
                unitCodeNode.InnerText = unitInfo.Code;
                unitNode.AppendChild(unitCodeNode);

                XmlNode unitNameNode = doc.CreateElement("unit_name");
                unitNameNode.InnerText = unitInfo.Name;
                unitNode.AppendChild(unitNameNode);

                XmlNode unitNameEnNode = doc.CreateElement("unit_name_en");
                unitNameEnNode.InnerText = unitInfo.EnglishName;
                unitNode.AppendChild(unitNameEnNode);

                XmlNode unitNameShortNode = doc.CreateElement("unit_name_short");
                unitNameShortNode.InnerText = unitInfo.ShortName;
                unitNode.AppendChild(unitNameShortNode);

                XmlNode ftpImagerURLNode = doc.CreateElement("ftp_image_url");
                ftpImagerURLNode.InnerText = unitInfo.ftpImagerURL;
                unitNode.AppendChild(ftpImagerURLNode);

                XmlNode imagerURLNode = doc.CreateElement("image_url");
                imagerURLNode.InnerText = unitInfo.imageURL;
                unitNode.AppendChild(imagerURLNode);

                XmlNode dimensionalCodeURLNode = doc.CreateElement("dimensional_code_url");
                dimensionalCodeURLNode.InnerText = unitInfo.dimensionalCodeURL;
                unitNode.AppendChild(dimensionalCodeURLNode);

                XmlNode webserversURLNode = doc.CreateElement("webservers_url");
                webserversURLNode.InnerText = unitInfo.webserversURL;
                unitNode.AppendChild(webserversURLNode);

                XmlNode weixinidNode = doc.CreateElement("weixin_id");
                weixinidNode.InnerText = unitInfo.weiXinId;
                unitNode.AppendChild(weixinidNode);

                XmlNode unitCityIdNode = doc.CreateElement("unit_city_id");
                unitCityIdNode.InnerText = unitInfo.CityId;
                unitNode.AppendChild(unitCityIdNode);

                XmlNode unitAddressNode = doc.CreateElement("unit_address");
                unitAddressNode.InnerText = unitInfo.Address;
                unitNode.AppendChild(unitAddressNode);

                XmlNode unitContactNode = doc.CreateElement("unit_contant");
                unitContactNode.InnerText = unitInfo.Contact;
                unitNode.AppendChild(unitContactNode);

                XmlNode unitTelNode = doc.CreateElement("unit_tel");
                unitTelNode.InnerText = unitInfo.Telephone;
                unitNode.AppendChild(unitTelNode);

                XmlNode unitFaxNode = doc.CreateElement("unit_fax");
                unitFaxNode.InnerText = unitInfo.Fax;
                unitNode.AppendChild(unitFaxNode);

                XmlNode unitEmailNode = doc.CreateElement("unit_email");
                unitEmailNode.InnerText = unitInfo.Email;
                unitNode.AppendChild(unitEmailNode);

                XmlNode unitPostcodeNode = doc.CreateElement("unit_postcode");
                unitPostcodeNode.InnerText = unitInfo.Postcode;
                unitNode.AppendChild(unitPostcodeNode);

                XmlNode unitStatusNode = doc.CreateElement("unit_status");
                unitStatusNode.InnerText = unitInfo.Status;
                unitNode.AppendChild(unitStatusNode);

                XmlNode parentUnitIdNode = doc.CreateElement("parent_unit_id");
                parentUnitIdNode.InnerText = unitInfo.Parent_Unit_Id;
                unitNode.AppendChild(parentUnitIdNode);

                XmlNode createTimeNode = doc.CreateElement("create_time");
                createTimeNode.InnerText = unitInfo.Create_Time;
                unitNode.AppendChild(createTimeNode);

                XmlNode createUserIdNode = doc.CreateElement("create_user_id");
                createUserIdNode.InnerText = unitInfo.Create_User_Id;
                unitNode.AppendChild(createUserIdNode);

                XmlNode unitDateNode = doc.CreateElement("unit_date");
                unitDateNode.InnerText = unitDate;
                unitNode.AppendChild(unitDateNode);

                XmlNode cctypeNode = doc.CreateElement("ccType");
                cctypeNode.InnerText = ccType;
                unitNode.AppendChild(cctypeNode);

                XmlNode posNoNode = doc.CreateElement("pos_no");
                posNoNode.InnerText = posNo;
                unitNode.AppendChild(posNoNode);

                XmlNode posIdNode = doc.CreateElement("pos_id");
                posIdNode.InnerText = posId;
                unitNode.AppendChild(posIdNode);

                cfgNode.AppendChild(unitNode);
                #endregion

                #region Warehouse
                XmlNode warehouseNode = doc.CreateElement("Data");
                XmlAttribute warehouseNodeNameAtt = doc.CreateAttribute("name");
                warehouseNodeNameAtt.Value = "Warehouse";
                warehouseNode.Attributes.Append(warehouseNodeNameAtt);

                XmlNode warehouseIdNode = doc.CreateElement("WAREHOUSE_ID");
                warehouseIdNode.InnerText = warehouseInfo.ID;
                warehouseNode.AppendChild(warehouseIdNode);

                XmlNode whCodeNode = doc.CreateElement("WH_CODE");
                whCodeNode.InnerText = warehouseInfo.Code;
                warehouseNode.AppendChild(whCodeNode);

                XmlNode whNameNode = doc.CreateElement("WH_NAME");
                whNameNode.InnerText = warehouseInfo.Name;
                warehouseNode.AppendChild(whNameNode);

                XmlNode whNameEnNode = doc.CreateElement("WH_NAME_EN");
                whNameEnNode.InnerText = warehouseInfo.EnglishName;
                warehouseNode.AppendChild(whNameEnNode);

                XmlNode whUnitIdNode = doc.CreateElement("UNIT_ID");
                whUnitIdNode.InnerText = warehouseInfo.Unit.Id;
                warehouseNode.AppendChild(whUnitIdNode);

                XmlNode whAddressNode = doc.CreateElement("WH_ADDRESS");
                whAddressNode.InnerText = warehouseInfo.Address;
                warehouseNode.AppendChild(whAddressNode);

                XmlNode whContacterNode = doc.CreateElement("WH_CONTACTER");
                whContacterNode.InnerText = warehouseInfo.Contacter;
                warehouseNode.AppendChild(whContacterNode);

                XmlNode whTelNode = doc.CreateElement("WH_TEL");
                whTelNode.InnerText = warehouseInfo.Tel;
                warehouseNode.AppendChild(whTelNode);

                XmlNode whFaxNode = doc.CreateElement("WH_FAX");
                whFaxNode.InnerText = warehouseInfo.Fax;
                warehouseNode.AppendChild(whFaxNode);

                XmlNode whStatusNode = doc.CreateElement("WH_STATUS");
                whStatusNode.InnerText = warehouseInfo.Status.ToString();
                warehouseNode.AppendChild(whStatusNode);

                XmlNode whDefaultNode = doc.CreateElement("WH_DEFAULT");
                whDefaultNode.InnerText = warehouseInfo.IsDefault.ToString();
                warehouseNode.AppendChild(whDefaultNode);

                XmlNode whRemarkNode = doc.CreateElement("WH_REMARK");
                whRemarkNode.InnerText = warehouseInfo.Remark;
                warehouseNode.AppendChild(whRemarkNode);

                cfgNode.AppendChild(warehouseNode);
                #endregion

                #region WSURL
                XmlNode wsurlNode = doc.CreateElement("Data");
                XmlAttribute wsurlNodeNameAtt = doc.CreateAttribute("name");
                wsurlNodeNameAtt.Value = "WSURL";
                wsurlNode.Attributes.Append(wsurlNodeNameAtt);

                XmlNode wsurlValueNode = doc.CreateElement("Value");
                wsurlValueNode.InnerText = wsUrl;
                wsurlNode.AppendChild(wsurlValueNode);

                cfgNode.AppendChild(wsurlNode);
                #endregion

                #region UploadTime
                XmlNode uploadTimeNode = doc.CreateElement("Data");
                XmlAttribute uploadTimeNodeNameAtt = doc.CreateAttribute("name");
                uploadTimeNodeNameAtt.Value = "UploadTime";
                uploadTimeNode.Attributes.Append(uploadTimeNodeNameAtt);

                XmlNode uploadTimeValueNode = doc.CreateElement("Value");
                uploadTimeValueNode.InnerText = uploadTime;
                uploadTimeNode.AppendChild(uploadTimeValueNode);

                cfgNode.AppendChild(uploadTimeNode);
                #endregion

                #region User
                XmlNode userNode = doc.CreateElement("Data");
                XmlAttribute userNodeNameAtt = doc.CreateAttribute("name");
                userNodeNameAtt.Value = "User";
                userNode.Attributes.Append(userNodeNameAtt);

                XmlNode userValueNode = doc.CreateElement("User_id");
                userValueNode.InnerText = userId;
                userNode.AppendChild(userValueNode);

                XmlNode userCodeNode = doc.CreateElement("user_code");
                userCodeNode.InnerText = pcInitInfo.userInfo.User_Code;
                userNode.AppendChild(userCodeNode);

                XmlNode userPwdNode = doc.CreateElement("user_password");
                userPwdNode.InnerText = pcInitInfo.userInfo.User_Password;
                userNode.AppendChild(userPwdNode);

                XmlNode userNameNode = doc.CreateElement("user_name");
                userNameNode.InnerText = pcInitInfo.userInfo.User_Name;
                userNode.AppendChild(userNameNode);

                cfgNode.AppendChild(userNode);
                #endregion

                data.xml_config = doc.OuterXml;
                data.status = Utils.GetStatus(statusFlag);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), null, htLogExt);
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

        public GetUnitXmlConfigInfoContract GetUnitXmlConfigInfoJson(
            string customerCode, string unitCode, string posSn)
        {
            return GetUnitXmlConfigInfo(TransType.JSON, customerCode, unitCode, posSn);
        }
        #endregion

        #region GetItemInfo
        /// <summary>
        /// 通过商品条码获取商品信息
        /// </summary>
        public GetItemInfoContract GetItemInfo(TransType transType, string userId, string token,
            string unitId, string barcode)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetItemInfo";
            string ifCode = "C015";
            var data = new GetItemInfoContract();
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
                htParams.Add("barcode", barcode);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetItemInfoContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<CheckVersionContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetItemInfoContract>(htResult);
                htResult = ErrorService.CheckLength("条码", barcode, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetItemInfoContract>(htResult);
                #endregion

                #region 验证权限
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

                // 查询
                var apwsService = new Dex.WsService.AP.APBasicService();
                var itemInfo = apwsService.GetItemInfoByBarcode(barcode);
                if (itemInfo != null && 
                    itemInfo.ItemInfoByBarcode != null && 
                    itemInfo.SkuInfoByBarcode !=null)
                {
                    data.status = Utils.GetStatus(true);
                    data.item = Dex.WcfServiceCommon.Common.ToItem(itemInfo.ItemInfoByBarcode);
                    data.sku = Dex.WcfServiceCommon.Common.ToSku(itemInfo.SkuInfoByBarcode);
                }
                else
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = ErrorCode.A006.ToString();
                    data.error_desc = "商品不存在";
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

        public GetItemInfoContract GetItemInfoJson(string userId, string token,
            string unitId, string barcode)
        {
            return GetItemInfo(TransType.JSON, userId, token,
                unitId, barcode);
        }
        #endregion

        #region GetPosCode
        /// <summary>
        /// 获取终端编码接口
        /// </summary>
        public GetPosCodeContract GetPosCode(TransType transType, string userId, string token,
            string type, string unitId, string posSn)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetPosCode";
            string ifCode = "C019";
            var data = new GetPosCodeContract();
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
                htParams.Add("sn", posSn);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPosCodeContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPosCodeContract>(htResult);
                //htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetPosCodeContract>(htResult);
                htResult = ErrorService.CheckLength("终端序列号", posSn, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPosCodeContract>(htResult);
                #endregion

                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                #region 检查User
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

                // 检查Token是否不匹配或过期
                statusFlag = true;
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

                string customerId = certInfo.CustomerId;

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;
                #endregion

                // 获取数据
                ConfigService cfgService = new ConfigService();
                bool enableConnectMposBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectMposBS)
                {
                    var posExchangeService = new ExchangeBsService.PosExchangeService();
                    data.pos_code = posExchangeService.GetPosCode(customerId, userId, unitId, posSn).Trim();
                    if (data.pos_code.Length == 0 || data.pos_code.Length > 4)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A004, "后台生成的终端编码长度超出范围", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
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

        public GetPosCodeContract GetPosCodeJson(string userId, string token,
            string type, string unitId, string posSn)
        {
            return GetPosCode(TransType.JSON, userId, token, type, unitId, posSn);
        }
        #endregion

        #region ApplyCustomerAndUnit
        /// <summary>
        /// 申请客户及门店接口
        /// </summary>
        public ApplyCustomerAndUnitContract ApplyCustomerAndUnit(TransType transType, string userId, string token,
            string type, CustomerUnitApply customerUnitApply)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.ApplyCustomerAndUnit";
            string ifCode = "C018";
            var data = new ApplyCustomerAndUnitContract();
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
                htParams.Add("token", token);
                htParams.Add("customer_unit_apply", customerUnitApply);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<ApplyCustomerAndUnitContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<ApplyCustomerAndUnitContract>(htResult);
                #endregion

                #region 验证权限
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
                if (type != CertType.MOBILE.ToString() && 
                    (certInfo.CustomerId == null || certInfo.CustomerId.Length == 0))
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
                var apService = new Dex.ServicesAP.BasicService();
                htError = apService.CheckApplyCustomerAndUnit(customerUnitApply, type);
                if (!Convert.ToBoolean(htError["status"]))
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }
                #endregion

                // 处理
                apService.ApplyCustomerAndUnit(customerUnitApply, null, null, userId);

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

        public ApplyCustomerAndUnitContract ApplyCustomerAndUnitJson(string userId, string token,
            string type, CustomerUnitApply customerUnitApply)
        {
            return ApplyCustomerAndUnit(TransType.JSON, userId, token,
                type, customerUnitApply);
        }
        #endregion

        #region GetUserUnitRelations
        /// <summary>
        /// 下载用户信息与客户关系接口
        /// </summary>
        public GetUserUnitRelationsContract GetUserUnitRelations(TransType transType, string userId, string token,
            string type)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetUserUnitRelations";
            string ifCode = "C009";
            var data = new GetUserUnitRelationsContract();
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
                htParams.Add("token", token);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                #region 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUserUnitRelationsContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 50, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetUserUnitRelationsContract>(htResult);
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
                if (certInfo == null)
                {
                    htError = ErrorService.OutputError(ErrorCode.A006, "未查找到匹配的凭证信息", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }
                if (type != CertType.MOBILE.ToString() && (certInfo.CustomerId == null || certInfo.CustomerId.Length == 0))
                {
                    htError = ErrorService.OutputError(ErrorCode.A007, "获取后台数据（客户ID）失败", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;
                #endregion

                // 查询
                ConfigService cfgService = new ConfigService();
                var apBasicService = new cPos.Dex.WsService.AP.APBasicService();
                var userInfo = apBasicService.GetUserUnitRelations(userId, type);
                data.user = new User();
                data.user.user_id = userInfo.ID;
                data.user.user_code = userInfo.Account;
                data.user.user_name = userInfo.Name;

                // customers
                data.customers = new List<Customer>();
                if (userInfo.CustomerList != null)
                {
                    foreach (var tmpCustomer in userInfo.CustomerList)
                    {
                        var objCustomerInfo = new Customer();
                        objCustomerInfo.customer_id = tmpCustomer.ID;
                        objCustomerInfo.customer_code = tmpCustomer.Code;
                        objCustomerInfo.customer_name = tmpCustomer.Name;
                        objCustomerInfo.customer_name_en = tmpCustomer.EnglishName;
                        objCustomerInfo.address = tmpCustomer.Address;
                        objCustomerInfo.post_code = tmpCustomer.PostCode;
                        objCustomerInfo.contacter = tmpCustomer.Contacter;
                        objCustomerInfo.tel = tmpCustomer.Tel;
                        objCustomerInfo.fax = tmpCustomer.Fax;
                        objCustomerInfo.email = tmpCustomer.Email;
                        objCustomerInfo.cell = tmpCustomer.Cell;
                        objCustomerInfo.memo = tmpCustomer.Memo;
                        objCustomerInfo.start_date = tmpCustomer.StartDate;
                        objCustomerInfo.status = tmpCustomer.Status.ToString();
                        data.customers.Add(objCustomerInfo);
                    }
                }

                // units
                data.units = new List<Unit>();
                if (userInfo.CustomerShopList != null)
                {
                    foreach (var tmpCustomerShop in userInfo.CustomerShopList)
                    {
                        var objCustomerShopInfo = new Unit();
                        objCustomerShopInfo.unit_id = tmpCustomerShop.ID;
                        objCustomerShopInfo.customer_id = tmpCustomerShop.Customer.ID;
                        //objCustomerShopInfo.type_id = tmpCustomerShop.type_id;
                        objCustomerShopInfo.unit_code = tmpCustomerShop.Code;
                        objCustomerShopInfo.unit_name = tmpCustomerShop.Name;
                        objCustomerShopInfo.unit_name_en = tmpCustomerShop.EnglishName;
                        objCustomerShopInfo.unit_name_short = tmpCustomerShop.ShortName;
                        objCustomerShopInfo.unit_city_id = tmpCustomerShop.City;
                        objCustomerShopInfo.unit_address = tmpCustomerShop.Address;
                        objCustomerShopInfo.unit_contact = tmpCustomerShop.Contact;
                        objCustomerShopInfo.unit_tel = tmpCustomerShop.Tel;
                        objCustomerShopInfo.unit_fax = tmpCustomerShop.Fax;
                        objCustomerShopInfo.unit_email = tmpCustomerShop.Email;
                        objCustomerShopInfo.unit_postcode = tmpCustomerShop.PostCode;
                        objCustomerShopInfo.unit_remark = tmpCustomerShop.Remark;
                        objCustomerShopInfo.unit_status = tmpCustomerShop.Status.ToString();
                        //objCustomerShopInfo.unit_props = tmpCustomerShop.unit_props;
                        //objCustomerShopInfo.warehouses = tmpCustomerShop.warehouses;
                        objCustomerShopInfo.longitude = tmpCustomerShop.longitude;
                        objCustomerShopInfo.dimension = tmpCustomerShop.dimension;
                        objCustomerShopInfo.shop_url1 = tmpCustomerShop.shop_url1;
                        objCustomerShopInfo.shop_url2 = tmpCustomerShop.shop_url2;
                        data.units.Add(objCustomerShopInfo);
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

        public GetUserUnitRelationsContract GetUserUnitRelationsJson(string userId, string token, string type)
        {
            return GetUserUnitRelations(TransType.JSON, userId, token, type);
        }
        #endregion

        #region GetUsersProfile
        /// <summary>
        /// 下载用户配置信息接口
        /// </summary>
        public GetUsersProfileContract GetUsersProfile(
            TransType transType, string userId, string token,
            string unitId, int packageSeq)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "BasicService.GetUsersProfile";
            string ifCode = "C020";
            var data = new GetUsersProfileContract();
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
                htParams.Add("package_seq", packageSeq);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUsersProfileContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 50, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetUsersProfileContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUsersProfileContract>(htResult);
                htResult = ErrorService.CheckLength("包开始序号", packageSeq.ToString(), 1, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetUsersProfileContract>(htResult);
                #endregion

                #region 检查权限
                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User
                certInfo = authService.GetCertByUserId(userId);
                if (certInfo == null)
                {
                    htError = ErrorService.OutputError(ErrorCode.A006, "用户ID/客户ID不存在", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

                // 检查Token是否不匹配或过期
                statusFlag = true;
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
                #endregion

                string customerId = certInfo.CustomerId;

                htLogExt["customer_code"] = certInfo.CustomerCode;
                htLogExt["customer_id"] = certInfo.CustomerId;
                htLogExt["user_code"] = certInfo.UserCode;

                // 生成数据包
                if (true)
                {
                    ExchangeBsService.UserAuthService bsUserAuthService = new ExchangeBsService.UserAuthService();
                    cPos.Model.BaseInfo usersInfo = bsUserAuthService.GetUserBaseByUserId(userId, customerId, unitId);
                    string pkgBatId = Utils.NewGuid();
                    string pkgTypeCode = PackageTypeMethod.USERS.ToString();
                    string pkgGenTypeCode = PackageGenTypeMethod.CLIENT_REQUEST.ToString();
                    PackageService pkgService = new PackageService();
                    Hashtable htPkg = pkgService.CreatePackage(AppType.Client, pkgBatId, pkgTypeCode,
                        customerId, unitId, userId, pkgGenTypeCode, null);
                    if (!Convert.ToBoolean(htPkg["status"]))
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = htPkg["error_code"].ToString();
                        data.error_full_desc = htPkg["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
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
                        return data;
                    }
                    Hashtable htPkgPublish = pkgService.PublishPackage(AppType.Client, pkgBatId, pkgId, userId);
                    if (!Convert.ToBoolean(htPkgPublish["status"]))
                    {
                        data.status = Utils.GetStatus(false);
                        data.error_code = htPkgPublish["error_code"].ToString();
                        data.error_full_desc = htPkgPublish["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
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
            }
            return data;
        }

        public GetUsersProfileContract GetUsersProfileJson(string userId, string token,
            string unitId, int packageSeq)
        {
            return GetUsersProfile(TransType.JSON, userId, token, unitId, packageSeq);
        }
        #endregion

    }
}