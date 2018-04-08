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
using cPos.Model.Advertise;

namespace cPos.Dex.WcfService
{
    /// <summary>
    /// OrderService
    /// </summary>
    public class OrderService : BaseService, IOrderService
    {
        #region UploadInoutOrders
        /// <summary>
        /// C102-上传出入库单接口
        /// </summary>
        public UploadInoutOrdersContract UploadInoutOrders(TransType transType,
            IList<InoutOrderContract> orders, string userId, string token, string unitId, string orderType)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.UploadInoutOrders";
            string ifCode = "C002";
            var data = new UploadInoutOrdersContract();
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
                htParams.Add("orders", orders);
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
                htParams.Add("order_type", orderType);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                CertInfo certInfo = null;

                #region 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("单据处理类型", orderType, 1, 30, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
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
                Dex.ServicesBs.OrderService orderService = new Dex.ServicesBs.OrderService();
                htError = orderService.CheckInoutOrders(orderType, orders);
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
                        orderService.SaveInoutOrders(orders, customerId, unitId, userId, orderType);
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

        public UploadInoutOrdersContract UploadInoutOrdersJson(IList<InoutOrderContract> orders, 
            string userId, string token, string unitId, string orderType)
        {
            return UploadInoutOrders(TransType.JSON, orders, userId, token, unitId, orderType);
        }

        public UploadInoutOrdersContract UploadInoutOrdersXml(IList<InoutOrderContract> orders,
            string userId, string token, string unitId, string orderType)
        {
            return UploadInoutOrders(TransType.XML, orders, userId, token, unitId, orderType);
        }
        #endregion

        #region UploadOrders
        /// <summary>
        /// C101-上传订单接口
        /// </summary>
        public UploadOrdersContract UploadOrders(TransType transType,
            IList<OrderContract> orders, string userId, string token, string unitId, string orderType)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.UploadOrders";
            string ifCode = "C101";
            var data = new UploadOrdersContract();
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
                htParams.Add("orders", orders);
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
                htParams.Add("order_type", orderType);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                CertInfo certInfo = null;

                #region 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("单据处理类型", orderType, 1, 30, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadOrdersContract>(htResult);
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

                // 检查Token是否不匹配或过期
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

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
                Dex.ServicesBs.OrderService orderService = new Dex.ServicesBs.OrderService();
                htError = orderService.CheckOrders(orders);
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
                        orderService.SaveOrders(orders, customerId, unitId, userId, orderType);
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

        public UploadOrdersContract UploadOrdersJson(IList<OrderContract> orders,
            string userId, string token, string unitId, string orderType)
        {
            return UploadOrders(TransType.JSON, orders, userId, token, unitId, orderType);
        }

        public UploadOrdersContract UploadOrdersXml(IList<OrderContract> orders,
            string userId, string token, string unitId, string orderType)
        {
            return UploadOrders(TransType.XML, orders, userId, token, unitId, orderType);
        }
        #endregion

        #region UploadCcOrders
        /// <summary>
        /// 上传盘点单接口
        /// </summary>
        public UploadCcOrdersContract UploadCcOrders(TransType transType,
            IList<CcOrderContract> orders, string userId, string token, string unitId, string orderType)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.UploadCcOrders";
            string ifCode = "C017";
            var data = new UploadCcOrdersContract();
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
                htParams.Add("orders", orders);
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
                htParams.Add("order_type", orderType);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = false;
                Hashtable htError = null;
                CertInfo certInfo = null;

                #region 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("单据处理类型", orderType, 1, 30, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                if (orderType != "MOBILE")
                {
                    htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                    if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                }
                #endregion

                #region 检查权限
                Dex.Services.AuthService authService = new Dex.Services.AuthService();

                // 检查User和Customer
                certInfo = authService.GetCertByUserId(userId);
                if (certInfo == null)
                {
                    htError = ErrorService.OutputError(ErrorCode.A006, "用户不存在", true);
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

                // 查询凭证
                //certInfo = authService.GetCertByUserId(userId);
                if (orderType != UploadCcOrderType.MOBILE.ToString())
                {
                    if (certInfo == null || certInfo.CustomerId == null || certInfo.CustomerId.Length == 0)
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
                #endregion

                #region 检查单据参数
                Dex.ServicesBs.OrderService orderService = new Dex.ServicesBs.OrderService();
                Dex.ServicesAP.OrderService apOrderService = new Dex.ServicesAP.OrderService();

                if (orderType == UploadCcOrderType.MOBILE.ToString())
                {
                    htError = apOrderService.CheckCcOrders(orders, orderType);
                }
                else
                {
                    htError = orderService.CheckCcOrders(orders, orderType);
                }
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
                bool enableConnectPosBS = false;
                if (orderType != UploadCcOrderType.MOBILE.ToString())
                {
                    enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                    if (!enableConnectPosBS)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A012, "连接业务平台数据通道已关闭", true);
                        data.status = Utils.GetStatus(false);
                        data.error_code = htError["error_code"].ToString();
                        data.error_full_desc = htError["error_desc"].ToString();
                        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                        return data;
                    }
                }

                try
                {
                    if (orderType == UploadCcOrderType.MOBILE.ToString())
                    {
                        apOrderService.SaveCcOrders(orders, customerId, unitId, userId, orderType);
                    }
                    else
                    {
                        orderService.SaveCcOrders(orders, customerId, unitId, userId, orderType);
                    }
                }
                catch (Exception ex)
                {
                    data.status = Utils.GetStatus(false);
                    data.error_code = ErrorCode.A018.ToString();
                    data.error_full_desc = ex.ToString();
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

        public UploadCcOrdersContract UploadCcOrdersJson(IList<CcOrderContract> orders,
            string userId, string token, string unitId, string orderType)
        {
            return UploadCcOrders(TransType.JSON, orders, userId, token, unitId, orderType);
        }
        #endregion

        #region DownloadInoutOrdersContract
        /// <summary>
        /// 下载Inout单据接口
        /// </summary>
        public DownloadInoutOrdersContract DownloadInoutOrders(TransType transType, string userId, string token,
            string unitId, string orderType)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.DownloadAJOrders";
            string ifCode = "C020";
            var data = new DownloadInoutOrdersContract();
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
                htParams.Add("order_type", orderType);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("单据类型", orderType, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
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
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
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

                // 获取数据
                ConfigService cfgService = new ConfigService();
                bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectPosBS)
                {
                    var bsOrderService = new ServicesBs.OrderService();
                    IList<cPos.Model.InoutInfo> orders = null;
                    if (orderType == "AJ")
                    {
                        var ccService = new ExchangeBsService.CCAuthService();
                        orders = ccService.GetAJList(customerId, unitId, userId);
                    }
                    data.orders = bsOrderService.ToInoutContracts(orders);
                    data.status = Utils.GetStatus(statusFlag);
                    LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
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

        public DownloadInoutOrdersContract DownloadInoutOrdersJson(string userId, string token,
            string unitId, string orderType)
        {
            return DownloadInoutOrders(TransType.JSON, userId, token, unitId, orderType);
        }

        public DownloadInoutOrdersContract DownloadInoutOrdersXml(string userId, string token,
            string unitId, string orderType)
        {
            return DownloadInoutOrders(TransType.XML, userId, token, unitId, orderType);
        }
        #endregion

        #region DownloadCcOrdersContract
        /// <summary>
        /// 下载Cc单据接口
        /// </summary>
        public DownloadCcOrdersContract DownloadCcOrders(TransType transType, string userId, string token,
            string unitId, string orderType)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.DownloadCcOrders";
            string ifCode = "C021";
            var data = new DownloadCcOrdersContract();
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
                htParams.Add("order_type", orderType);
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("单据类型", orderType, 1, 50, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<DownloadCcOrdersContract>(htResult);
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
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
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

                // 获取数据
                ConfigService cfgService = new ConfigService();
                bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectPosBS)
                {
                    var bsOrderService = new ServicesBs.OrderService();
                    IList<cPos.Model.CCInfo> orders = null;
                    if (orderType == "CC")
                    {
                        var ccService = new ExchangeBsService.CCAuthService();
                        //orders = ccService.GetCCList(customerId, userId, unitId);
                    }
                    data.orders = bsOrderService.ToCcContracts(orders);
                    data.status = Utils.GetStatus(statusFlag);
                    LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
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

        public DownloadCcOrdersContract DownloadCcOrdersJson(string userId, string token,
            string unitId, string orderType)
        {
            return DownloadCcOrders(TransType.JSON, userId, token, unitId, orderType);
        }

        public DownloadCcOrdersContract DownloadCcOrdersXml(string userId, string token,
            string unitId, string orderType)
        {
            return DownloadCcOrders(TransType.XML, userId, token, unitId, orderType);
        }
        #endregion

        #region GetPriceOrders
        /// <summary>
        /// C023-获取调价单集合
        /// </summary>
        public GetPriceOrdersContract GetPriceOrders(TransType transType, string userId, string token,
            string unitId, int seq, int startRow, int rowsCount)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.GetPriceOrders";
            string ifCode = "C023";
            var data = new GetPriceOrdersContract();
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
                htParams.Add("seq", seq);
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
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("开始序号", seq, 0, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("起始行索引", startRow, 1, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("返回行数量", rowsCount, 1, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                #endregion

                #region 检查数值范围
                htResult = ErrorService.CheckNumArrange("起始行索引", startRow, 0, Config.TopMaxVal, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
                htResult = ErrorService.CheckNumArrange("返回行数量", rowsCount, 0, Config.QueryDBMaxCount, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetPriceOrdersContract>(htResult);
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

                // 检查Token是否不匹配或过期
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

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

                // 查询
                Hashtable htQuery = new Hashtable();
                var adService = new ExchangeBsService.AdjustmentOrderBsService();
                var list = adService.GetAdjustmentOrderListPackaged(
                    customerId, userId, unitId, seq, startRow, rowsCount);
                data.price_orders = new List<PriceOrderContract>();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        #region order
                        var obj = new PriceOrderContract();
                        obj.order_id = item.order_id;
                        obj.order_no = item.order_no;
                        obj.order_date = item.order_date;
                        obj.begin_date = item.begin_date;
                        obj.end_date = item.end_date;
                        obj.item_price_type_id = item.item_price_type_id;
                        obj.item_price_type_name = item.item_price_type_name;
                        obj.status = item.status;
                        obj.status_desc = item.status_desc;
                        obj.create_user_id = item.create_user_id;
                        obj.create_time = item.create_time;
                        obj.modify_user_id = item.modify_user_id;
                        obj.modify_time = item.modify_time;
                        obj.remark = item.remark;
                        obj.no = Utils.GetStrVal(item.no);
                        #endregion

                        #region item_details
                        if (item.AdjustmentOrderDetailItemList != null)
                        {
                            obj.item_details = new List<PriceOrderDetailItemContract>();
                            foreach (var detail in item.AdjustmentOrderDetailItemList)
                            {
                                var objDetail = new PriceOrderDetailItemContract();
                                objDetail.order_detail_item_id = detail.order_detail_item_id;
                                objDetail.order_id = detail.order_id;
                                objDetail.item_id = detail.item_id;
                                objDetail.price = Utils.GetStrVal(detail.price);
                                obj.item_details.Add(objDetail);
                            }
                        }
                        #endregion
                        
                        #region sku_details
                        if (item.AdjustmentOrderDetailSkuList != null)
                        {
                            obj.sku_details = new List<PriceOrderDetailSkuContract>();
                            foreach (var detail in item.AdjustmentOrderDetailSkuList)
                            {
                                var objDetail = new PriceOrderDetailSkuContract();
                                objDetail.order_detail_sku_id = detail.order_detail_sku_id;
                                objDetail.order_id = detail.order_id;
                                objDetail.sku_id = detail.sku_id;
                                objDetail.price = Utils.GetStrVal(detail.price);
                                obj.sku_details.Add(objDetail);
                            }
                        }
                        #endregion
                        
                        #region unit_details
                        if (item.AdjustmentOrderDetailUnitList != null)
                        {
                            obj.unit_details = new List<PriceOrderDetailUnitContract>();
                            foreach (var detail in item.AdjustmentOrderDetailUnitList)
                            {
                                var objDetail = new PriceOrderDetailUnitContract();
                                objDetail.order_detail_unit_id = detail.order_detail_unit_id;
                                objDetail.order_id = detail.order_id;
                                objDetail.unit_id = detail.unit_id;
                                obj.unit_details.Add(objDetail);
                            }
                        }
                        #endregion

                        data.price_orders.Add(obj);
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

        public GetPriceOrdersContract GetPriceOrdersJson(string userId, string token,
            string unitId, int seq, int startRow, int rowsCount)
        {
            return GetPriceOrders(TransType.JSON, userId, token,
                unitId, seq, startRow, rowsCount);
        }

        public GetPriceOrdersContract GetPriceOrdersXml(string userId, string token,
            string unitId, int seq, int startRow, int rowsCount)
        {
            return GetPriceOrders(TransType.XML, userId, token,
                unitId, seq, startRow, rowsCount);
        }
        #endregion

        #region GetPriceOrdersCount
        /// <summary>
        /// C024-获取调价单数量
        /// </summary>
        public GetCountContract GetPriceOrdersCount(
            TransType transType, string userId, string token,
            string unitId, int seq)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.GetPriceOrdersCount";
            string ifCode = "C024";
            var data = new GetCountContract();
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
                htParams.Add("seq", seq);
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
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("开始序号", seq, 0, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
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

                // 检查Token是否不匹配或过期
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

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

                // 查询
                Hashtable htQuery = new Hashtable();
                var adService = new ExchangeBsService.AdjustmentOrderBsService();
                data.count = adService.GetAdjustmentOrderNotPackagedCount(
                    customerId, userId, unitId, seq);

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

        public GetCountContract GetPriceOrdersCountJson(string userId, string token,
            string unitId, int seq)
        {
            return GetPriceOrdersCount(TransType.JSON, userId, token,
                unitId, seq);
        }

        public GetCountContract GetPriceOrdersCountXml(string userId, string token,
            string unitId, int seq)
        {
            return GetPriceOrdersCount(TransType.XML, userId, token,
                unitId, seq);
        }
        #endregion

        #region SetInoutOrdersDldFlag
        /// <summary>
        /// 更新Inout单据下载标识接口
        /// </summary>
        public BaseContract SetInoutOrdersDldFlag(TransType transType,
            string userId, string token, string unitId, string batId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.SetInoutOrdersDldFlag";
            string ifCode = "C025";
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
                htParams.Add("bat_id", batId);
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
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
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

                //// 检查Token是否不匹配或过期
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

                var orderService = new ExchangeBsService.InoutBsService();

                // 保存
                ConfigService cfgService = new ConfigService();
                bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectPosBS)
                {
                    try
                    {
                        orderService.SetInoutIfFlagInfo(customerId, userId, unitId, batId);
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

        public BaseContract SetInoutOrdersDldFlagJson(
            string userId, string token, string unitId, string batId)
        {
            return SetInoutOrdersDldFlag(TransType.JSON, userId, token, unitId, batId);
        }
        #endregion

        #region SetCcOrdersDldFlag
        /// <summary>
        /// 更新Cc单据下载标识接口
        /// </summary>
        public BaseContract SetCcOrdersDldFlag(TransType transType,
            IList<CcOrderContract> orders, string userId, string token, string unitId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.SetCcOrdersDldFlag";
            string ifCode = "C026";
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
                htParams.Add("orders", orders);
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
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadCcOrdersContract>(htResult);
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

                // 检查Token是否不匹配或过期
                statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
                if (!statusFlag)
                {
                    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
                    data.status = Utils.GetStatus(false);
                    data.error_code = htError["error_code"].ToString();
                    data.error_full_desc = htError["error_desc"].ToString();
                    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
                    return data;
                }

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
                var orderService = new Dex.ServicesBs.OrderService();
                htError = orderService.CheckCcOrderIds(orders);
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
                        orderService.SetCcOrdersDldFlag(orders, customerId, unitId, userId);
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

        public BaseContract SetCcOrdersDldFlagJson(IList<CcOrderContract> orders,
            string userId, string token, string unitId)
        {
            return SetCcOrdersDldFlag(TransType.JSON, orders, userId, token, unitId);
        }

        public BaseContract SetCcOrdersDldFlagXml(IList<CcOrderContract> orders,
            string userId, string token, string unitId)
        {
            return SetCcOrdersDldFlag(TransType.XML, orders, userId, token, unitId);
        }
        #endregion

        #region UploadAdLogs
        ///// <summary>
        ///// 上传广告日志接口
        ///// </summary>
        //public UploadInoutOrdersContract UploadAdLogs(TransType transType,
        //    IList<InoutOrderContract> orders, string userId, string token, string unitId, string orderType)
        //{
        //    string bizId = Utils.NewGuid();
        //    string methodKey = "OrderService.UploadInoutOrders";
        //    string ifCode = "C002";
        //    var data = new UploadInoutOrdersContract();
        //    Hashtable htLogExt = new Hashtable();
        //    htLogExt["customer_code"] = null;
        //    htLogExt["customer_id"] = null;
        //    htLogExt["unit_code"] = null;
        //    htLogExt["unit_id"] = unitId;
        //    htLogExt["user_code"] = null;
        //    htLogExt["user_id"] = userId;
        //    htLogExt["if_code"] = ifCode;
        //    htLogExt["app_code"] = AppType.Client;
        //    try
        //    {
        //        Hashtable htParams = new Hashtable();
        //        htParams.Add("trans_type", transType);
        //        htParams.Add("orders", orders);
        //        htParams.Add("user_id", userId);
        //        htParams.Add("token", token);
        //        htParams.Add("unit_id", unitId);
        //        htParams.Add("order_type", orderType);
        //        LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

        //        bool statusFlag = false;
        //        Hashtable htError = null;
        //        CertInfo certInfo = null;

        //        #region 检查参数
        //        Hashtable htResult = new Hashtable();
        //        bool paramCheckFlag = false;
        //        htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
        //        if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
        //        //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
        //        //if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
        //        htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
        //        if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
        //        htResult = ErrorService.CheckLength("单据处理类型", orderType, 1, 30, true, false, ref paramCheckFlag);
        //        if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
        //        #endregion

        //        #region 检查权限
        //        Dex.Services.AuthService authService = new Dex.Services.AuthService();

        //        // 检查User和Customer
        //        certInfo = authService.GetCertByUserId(userId);
        //        if (certInfo == null)
        //        {
        //            htError = ErrorService.OutputError(ErrorCode.A006, "用户ID不存在", true);
        //            data.status = Utils.GetStatus(false);
        //            data.error_code = htError["error_code"].ToString();
        //            data.error_full_desc = htError["error_desc"].ToString();
        //            LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //            return data;
        //        }

        //        statusFlag = true;
        //        //// 检查Token是否不匹配或过期
        //        //statusFlag = authService.CheckCertToken(token, certInfo.CertId, userId);
        //        //if (!statusFlag)
        //        //{
        //        //    htError = ErrorService.OutputError(ErrorCode.A005, "令牌不匹配或过期", true);
        //        //    data.status = Utils.GetStatus(false);
        //        //    data.error_code = htError["error_code"].ToString();
        //        //    data.error_full_desc = htError["error_desc"].ToString();
        //        //    LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //        //    return data;
        //        //}

        //        // 查询凭证
        //        certInfo = authService.GetCertByUserId(userId);
        //        if (certInfo == null || certInfo.CustomerId == null || certInfo.CustomerId.Length == 0)
        //        {
        //            htError = ErrorService.OutputError(ErrorCode.A007, "获取后台数据（客户ID）失败", true);
        //            data.status = Utils.GetStatus(false);
        //            data.error_code = htError["error_code"].ToString();
        //            data.error_full_desc = htError["error_desc"].ToString();
        //            LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //            return data;
        //        }

        //        string customerId = certInfo.CustomerId;

        //        htLogExt["customer_code"] = certInfo.CustomerCode;
        //        htLogExt["customer_id"] = certInfo.CustomerId;
        //        htLogExt["user_code"] = certInfo.UserCode;
        //        #endregion

        //        #region 检查单据参数
        //        Dex.ServicesBs.OrderService orderService = new Dex.ServicesBs.OrderService();
        //        htError = orderService.CheckInoutOrders(orderType, orders);
        //        if (!Convert.ToBoolean(htError["status"]))
        //        {
        //            data.status = Utils.GetStatus(false);
        //            data.error_code = htError["error_code"].ToString();
        //            data.error_full_desc = htError["error_desc"].ToString();
        //            LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //            return data;
        //        }
        //        #endregion

        //        // 保存
        //        ConfigService cfgService = new ConfigService();
        //        bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
        //        if (enableConnectPosBS)
        //        {
        //            try
        //            {
        //                orderService.SaveInoutOrders(orders, customerId, unitId, userId, orderType);
        //            }
        //            catch (Exception ex)
        //            {
        //                data.status = Utils.GetStatus(false);
        //                data.error_code = ErrorCode.A018.ToString();
        //                data.error_full_desc = ex.ToString();
        //                LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //                return data;
        //            }
        //        }
        //        else
        //        {
        //            htError = ErrorService.OutputError(ErrorCode.A012, "连接业务平台数据通道已关闭", true);
        //            data.status = Utils.GetStatus(false);
        //            data.error_code = htError["error_code"].ToString();
        //            data.error_full_desc = htError["error_desc"].ToString();
        //            LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //            return data;
        //        }

        //        data.status = Utils.GetStatus(statusFlag);
        //        LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
        //    }
        //    catch (Exception ex)
        //    {
        //        data.status = Utils.GetStatus(false);
        //        data.error_code = ErrorCode.A000.ToString();
        //        data.error_full_desc = ex.ToString();
        //        LogService.WriteError(bizId, methodKey, data.error_code, data.ToString(), userId, htLogExt);
        //    }
        //    return data;
        //}

        //public UploadInoutOrdersContract UploadInoutOrdersJson(IList<InoutOrderContract> orders,
        //    string userId, string token, string unitId, string orderType)
        //{
        //    return UploadInoutOrders(TransType.JSON, orders, userId, token, unitId, orderType);
        //}

        //public UploadInoutOrdersContract UploadInoutOrdersXml(IList<InoutOrderContract> orders,
        //    string userId, string token, string unitId, string orderType)
        //{
        //    return UploadInoutOrders(TransType.XML, orders, userId, token, unitId, orderType);
        //}
        #endregion

        #region GetAdOrders
        /// <summary>
        /// 获取广告订单集合
        /// </summary>
        public GetAdOrdersContract GetAdOrders(TransType transType, string userId, string token,
            string unitId, int startRow, int rowsCount)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.GetAdOrders";
            string ifCode = "C005";
            var data = new GetAdOrdersContract();
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
                startRow = 0;

                Hashtable htParams = new Hashtable();
                htParams.Add("trans_type", transType);
                htParams.Add("user_id", userId);
                htParams.Add("token", token);
                htParams.Add("unit_id", unitId);
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
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("起始行索引", startRow, 1, 12, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("返回行数量", rowsCount, 1, 10, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                #endregion

                #region 检查数值范围
                htResult = ErrorService.CheckNumArrange("起始行索引", startRow, 0, Config.TopMaxVal, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
                htResult = ErrorService.CheckNumArrange("返回行数量", rowsCount, 0, Config.QueryDBMaxCount, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetAdOrdersContract>(htResult);
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

                // 查询
                Hashtable htQuery = new Hashtable();
                var adService = new ExchangeBsService.AdvertiseOrderBsService();
                var list = adService.GetAdvertiseOrderListPackaged(
                    customerId, userId, unitId, startRow, rowsCount);
                data.bat_id = Utils.NewGuid();
                data.ad_orders = new List<AdOrderContract>();
                if (list != null)
                {
                    IList<AdvertiseOrderUnitInfo> adUnitList = new List<AdvertiseOrderUnitInfo>();
                    foreach (var item in list)
                    {
                        #region order
                        var obj = new AdOrderContract();
                        obj.order_id = item.order_id;
                        obj.order_code = item.order_code;
                        obj.order_date = item.order_date;
                        obj.date_start = item.date_start;
                        obj.date_end = item.date_end;
                        obj.time_start = item.time_start;
                        obj.time_end = item.time_end;
                        obj.playbace_no = item.playbace_no.ToString();
                        obj.url_address = item.url_address;
                        obj.status = item.status;
                        obj.status_desc = item.status_desc;
                        obj.create_user_id = item.create_user_id;
                        obj.create_time = item.create_time;
                        obj.modify_user_id = item.modify_user_id;
                        obj.modify_time = item.modify_time;
                        #endregion

                        #region ad list
                        var itemAdList = adService.GetAdvertiseListPackaged(
                            customerId, userId, unitId, item.order_id);
                        if (itemAdList != null)
                        {
                            obj.ad_list = new List<AdContract>();
                            foreach (var detail in itemAdList)
                            {
                                var objDetail = new AdContract();
                                objDetail.advertise_id = detail.advertise_id;
                                objDetail.order_id = item.order_id;
                                objDetail.advertise_order_advertise_id = detail.advertise_order_advertise_id;
                                objDetail.advertise_name = detail.advertise_name;
                                objDetail.advertise_code = detail.advertise_code;
                                objDetail.file_size = detail.file_size;
                                objDetail.file_format = detail.file_format;
                                objDetail.display = detail.display;
                                objDetail.playback_time = detail.playback_time;
                                objDetail.url_address = detail.url_address;
                                objDetail.brand_customer_id = detail.brand_customer_id;
                                objDetail.brand_id = detail.brand_id;
                                objDetail.status = detail.status;
                                objDetail.create_time = detail.create_time;
                                objDetail.create_user_id = detail.create_user_id;
                                objDetail.modify_time = detail.modify_time;
                                objDetail.modify_user_id = detail.modify_user_id;
                                obj.ad_list.Add(objDetail);
                            }
                        }
                        #endregion

                        #region order_ad_list
                        //var itemOrderAdList = adService.GetOrderAdvertiseListPackaged(
                        //    customerId, userId, unitId, item.order_id);
                        //if (itemOrderAdList != null)
                        //{
                        //    obj.ad_list = new List<AdContract>();
                        //    foreach (var detail in itemOrderAdList)
                        //    {
                        //        var objDetail = new OrderAdContract();
                        //        objDetail.id = detail.id;
                        //        objDetail.order_id = item.order_id;
                        //        objDetail.advertise_id = detail.advertise_id;
                        //        obj.order_ad_list.Add(objDetail);
                        //    }
                        //}
                        #endregion

                        data.ad_orders.Add(obj);

                        AdvertiseOrderUnitInfo adUnitObj = new AdvertiseOrderUnitInfo();
                        adUnitObj.order_id = item.order_id;
                        adUnitObj.unit_id = unitId;
                        adUnitList.Add(adUnitObj);

                        /////
                    }

                    // 记录批次号
                    bool setFlag = adService.SetAdvertiseOrderBatInfo(customerId, userId, unitId, data.bat_id, adUnitList);
                    if (!setFlag) throw new Exception("记录批次号失败");
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

        public GetAdOrdersContract GetAdOrdersJson(string userId, string token,
            string unitId, int startRow, int rowsCount)
        {
            return GetAdOrders(TransType.JSON, userId, token,
                unitId, startRow, rowsCount);
        }
        #endregion

        #region GetAdOrdersCount
        /// <summary>
        /// 获取广告订单数量
        /// </summary>
        public GetCountContract GetAdOrdersCount(
            TransType transType, string userId, string token,
            string unitId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.GetAdOrdersCount";
            string ifCode = "C006";
            var data = new GetCountContract();
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
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
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

                // 查询
                Hashtable htQuery = new Hashtable();
                var adService = new ExchangeBsService.AdvertiseOrderBsService();
                data.count = adService.GetAdvertiseOrderNotPackagedCount(
                    customerId, userId, unitId);

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

        public GetCountContract GetAdOrdersCountJson(string userId, string token,
            string unitId)
        {
            return GetAdOrdersCount(TransType.JSON, userId, token,
                unitId);
        }
        #endregion

        #region SetAdOrdersFlag
        /// <summary>
        /// 更新广告订单下载标记
        /// </summary>
        public BaseContract SetAdOrdersFlag(
            TransType transType, string userId, string token,
            string unitId, string batId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.SetAdOrdersFlag";
            string ifCode = "C007";
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
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
                htResult = ErrorService.CheckLength("批次号", batId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetCountContract>(htResult);
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

                // 查询
                Hashtable htQuery = new Hashtable();
                var adService = new ExchangeBsService.AdvertiseOrderBsService();
                bool setFlag = adService.SetAdvertiseOrderIfFlagInfo(
                    customerId, userId, unitId, batId);
                if (!setFlag) throw new Exception("更新批次号失败");

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

        public BaseContract SetAdOrdersFlagJson(string userId, string token,
            string unitId, string batId)
        {
            return SetAdOrdersFlag(TransType.JSON, userId, token,
                unitId, batId);
        }
        #endregion

        #region UploadAdOrderLogs
        /// <summary>
        /// 上传广告日志接口
        /// </summary>
        public BaseContract UploadAdOrderLogs(TransType transType,
            IList<AdOrderLogContract> logs, string userId, string token, string unitId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.UploadAdOrderLogs";
            string ifCode = "C008";
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
                htParams.Add("logs", logs);
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
                if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<UploadInoutOrdersContract>(htResult);
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
                Dex.ServicesBs.OrderService orderService = new Dex.ServicesBs.OrderService();
                htError = orderService.CheckAdOrderLogs(logs);
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
                        orderService.SaveAdOrderLogs(logs, customerId, unitId, userId);
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

        public BaseContract UploadAdOrderLogsJson(IList<AdOrderLogContract> logs,
            string userId, string token, string unitId)
        {
            return UploadAdOrderLogs(TransType.JSON, logs, userId, token, unitId);
        }
        #endregion

        #region GetDeliveryOrdersContract
        /// <summary>
        /// 下载配送订单集合接口
        /// </summary>
        public GetDeliveryOrdersContract GetDeliveryOrders(TransType transType, string userId, string token,
            string unitId)
        {
            string bizId = Utils.NewGuid();
            string methodKey = "OrderService.GetDeliveryOrders";
            string ifCode = "C021";
            var data = new GetDeliveryOrdersContract();
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
                LogService.WriteTrace(bizId, methodKey, TraceLogType.Params.ToString(), htParams, userId, htLogExt);

                bool statusFlag = true;
                Hashtable htError = null;
                CertInfo certInfo = null;

                // 检查参数
                Hashtable htResult = new Hashtable();
                bool paramCheckFlag = false;
                #region Check Length
                htResult = ErrorService.CheckLength("用户ID", userId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetDeliveryOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("令牌", token, 1, 32, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
                htResult = ErrorService.CheckLength("门店ID", unitId, 1, 32, true, false, ref paramCheckFlag);
                if (!paramCheckFlag) return ErrorConvert.Export<GetDeliveryOrdersContract>(htResult);
                //htResult = ErrorService.CheckLength("单据类型", orderType, 1, 50, true, false, ref paramCheckFlag);
                //if (!paramCheckFlag) return ErrorConvert.Export<DownloadInoutOrdersContract>(htResult);
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
                bool enableConnectPosBS = cfgService.GetEnableConnectPosBSCfg();
                if (enableConnectPosBS)
                {
                    var bsOrderService = new ServicesBs.OrderService();
                    IList<cPos.Model.InoutInfo> orders = null;
                    var distributionService = new ExchangeBsService.DistributionService();
                    orders = distributionService.GetDistributionList(customerId, unitId, userId);
                    data.bat_id = Utils.NewGuid();
                    data.orders = bsOrderService.ToInoutContracts(orders);

                    if (data.orders != null && data.orders.Count > 0)
                    {
                        // 记录批次号
                        bool setFlag = distributionService.SetDistributionBatInfo(customerId, userId, unitId, data.bat_id, orders);
                        if (!setFlag) throw new Exception("记录批次号失败");
                    }

                    data.status = Utils.GetStatus(statusFlag);
                    LogService.WriteTrace(bizId, methodKey, TraceLogType.Return.ToString(), data.ToString(), userId, htLogExt);
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

        public GetDeliveryOrdersContract GetDeliveryOrdersJson(string userId, string token,
            string unitId)
        {
            return GetDeliveryOrders(TransType.JSON, userId, token, unitId);
        }
        #endregion
    }
}