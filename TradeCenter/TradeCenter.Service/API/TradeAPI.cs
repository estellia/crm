using JIT.TradeCenter.BLL;
using JIT.TradeCenter.BLL.TonysFarmRecharge;
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.Entity.HuiFuPayEntity;
using JIT.TradeCenter.Entity.HuiFuPayEntity.Reponse;
using JIT.TradeCenter.Entity.HuiFuPayEntity.Request;
using JIT.TradeCenter.Framework;
using JIT.TradeCenter.Framework.DataContract;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.IO;
using JIT.Utility.Log;
using JIT.Utility.Pay.Alipay.Channel;
using JIT.Utility.Pay.Alipay.Interface.Offline;
using JIT.Utility.Pay.Alipay.Interface.Offline.CreateAndPay;
using JIT.Utility.Pay.Alipay.Interface.Offline.QRCodePre;
using JIT.Utility.Pay.Alipay.Interface.Scan;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.Utility.Pay.UnionPay.Interface.IVR;
using JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.Wap.Request;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.WeiXinPay;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.Interface.Native;
using JIT.Utility.Pay.WeiXinPay.Util;
using LitJson;
using PayCenterNotifyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace JIT.TradeCenter.Service.API
{
    /// <summary>
    /// 具体的支付接口类
    /// </summary>
    public static class TradeAPI
    {
        public static List<string> CacheOrder = new List<string>();

        #region 创建交易中心支付订单AppOrder
        /// <summary>
        /// 创建交易中心支付订单AppOrder
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public static CreateOrderResponse CreateOrder(TradeRequest pRequest)
        {
            var userInfo = pRequest.GetUserInfo();
            CreateOrderResponse response = new CreateOrderResponse();
            response.ResultCode = 0;
            CreateOrderParameters para = pRequest.GetParameter<CreateOrderParameters>();
            Loggers.Debug(new DebugLogInfo() { Message = "业务参数:" + para.ToJSON() });
            if (para == null)
                throw new Exception("Parameters参数不正确");
            AppOrderBLL bll = new AppOrderBLL(userInfo);
            AppOrderEntity entity;

            #region 支付等待5秒后可再次支付
            var appOrderEntity = bll.QueryByEntity(new AppOrderEntity() { ClientIP = pRequest.ClientID, AppOrderID = para.AppOrderID }, null).FirstOrDefault();
            if (appOrderEntity != null)
            {
                DateTime dtNow = DateTime.Now;
                TimeSpan ts = dtNow - appOrderEntity.CreateTime.Value;
                if (ts.TotalSeconds < 5)
                {
                    throw new Exception("支付已启动，请稍后再试");
                }

            }
            #endregion

            #region  在支付中心创建订单
            var tran = bll.CreateTran();
            using (tran.Connection)
            {
                try
                {
                    #region 删除已存在的订单
                    bll.DeleteByAppInfo(pRequest.ClientID, para.AppOrderID, pRequest.AppID.Value, tran);
                    #endregion

                    #region 创建订单
                    entity = new AppOrderEntity()
                    {
                        Status = 0,
                        MobileNO = para.MobileNO,
                        AppClientID = pRequest.ClientID,
                        AppUserID = pRequest.UserID,
                        AppID = pRequest.AppID,
                        AppOrderAmount = Convert.ToInt32(para.AppOrderAmount),
                        AppOrderDesc = para.AppOrderDesc,
                        AppOrderID = para.AppOrderID,
                        AppOrderTime = para.GetDateTime(),
                        Currency = 1,
                        CreateBy = pRequest.UserID,
                        PayChannelID = para.PayChannelID,
                        LastUpdateBy = pRequest.UserID,
                        OpenId = para.OpenId,
                        ClientIP = para.ClientIP
                    };
                    bll.Create(entity, tran);//并且生成了一个自动增长的订单标识orderid
                    Loggers.Debug(new DebugLogInfo() { Message = "创建支付中心订单并保存数据库:" + entity.ToJSON() });
                    #endregion

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

            }
            #endregion

            #region 获取Channel
            PayChannelBLL channelBll = new PayChannelBLL(userInfo);
            var channel = channelBll.GetByID(para.PayChannelID);//PayChannelID是不同商户的支付方式的标识
            if (channel == null)
                throw new Exception("无此ChannelID的Channel信息");
            #endregion

            #region 测试Channel订单价格设置为1分钱***
            entity.AppOrderAmount = channel.IsTest.Value ? 1 : entity.AppOrderAmount;
            #endregion

            string url = string.Empty;
            if (para.AppOrderDesc != "ScanWxPayOrder")
            {
                if (para.PaymentMode == 0)
                {
                    channel.PayType = 9; //新版支付宝扫码支付
                }
                url = CreatePayRecord(response, para, bll, entity, channel);
            }

            response.PayUrl = url;
            response.OrderID = entity.OrderID;
            return response;
        }
        #endregion

        #region 记录请求支付日志信息
        /// <summary>
        /// 记录请求支付日志信息
        /// </summary>
        /// <param name="response"></param>
        /// <param name="para"></param>
        /// <param name="bll"></param>
        /// <param name="entity"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        private static string CreatePayRecord(CreateOrderResponse response, CreateOrderParameters para, AppOrderBLL bll, AppOrderEntity entity, PayChannelEntity channel)
        {
            string url = string.Empty;


            //用于记录支付平台的请求和响应
            string requestJson = string.Empty;
            string responseJson = string.Empty;

            var recordBll = new PayRequestRecordBLL(new Utility.BasicUserInfo());
            var recordEntity = new PayRequestRecordEntity()
            {
                ChannelID = channel.ChannelID,
                ClientID = entity.AppClientID,
                UserID = entity.AppUserID
            };

            #region 根据Channel类型创建支付订单
            try
            {
                switch (channel.PayType)
                {
                    case 1:
                        recordEntity.Platform = 1;
                        #region 银联Wap支付
                        UnionPayChannel unionWapPaychannel = channel.ChannelParameters.DeserializeJSONTo<UnionPayChannel>();
                        PreOrderRequest Wapreq = new PreOrderRequest()
                        {
                            BackUrl = ConfigurationManager.AppSettings["UnionPayWapNotifyUrl"].Trim('?') + string.Format("?ChannelID={0}", channel.ChannelID),
                            FrontUrl = string.IsNullOrEmpty(para.ReturnUrl) ? ConfigurationManager.AppSettings["UnionPayCallBackUrl"] : para.ReturnUrl,
                            MerchantID = unionWapPaychannel.MerchantID,
                            SendTime = DateTime.Now,
                            MerchantOrderCurrency = Currencys.RMB,
                            MerchantOrderDesc = entity.AppOrderDesc,
                            MerchantOrderAmt = entity.AppOrderAmount,
                            MerchantOrderID = entity.OrderID.ToString(),
                            SendSeqID = Guid.NewGuid().ToString("N"),
                            MerchantOrderTime = entity.AppOrderTime
                        };
                        requestJson = Wapreq.ToJSON();
                        var unionWapResponse = JIT.Utility.Pay.UnionPay.Interface.Wap.WapGateway.PreOrder(unionWapPaychannel, Wapreq);
                        responseJson = unionWapResponse.ToJSON();
                        if (unionWapResponse.IsSuccess)
                        {
                            entity.PayUrl = unionWapResponse.RedirectURL;
                            entity.Status = 1;
                            bll.Update(entity);
                            url = unionWapResponse.RedirectURL;
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("银联Wap创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, Wapreq.ToJSON(), unionWapResponse.ToJSON()) });
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("银联Wap创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, Wapreq.ToJSON(), unionWapResponse.ToJSON()) });
                            response.ResultCode = 100;
                            response.Message = unionWapResponse.Description;
                        }
                        #endregion
                        break;
                    case 2:
                        recordEntity.Platform = 1;
                        #region 银联语音支付
                        UnionPayChannel unionIVRPaychannel = channel.ChannelParameters.DeserializeJSONTo<UnionPayChannel>();
                        JIT.Utility.Pay.UnionPay.Interface.IVR.Request.PreOrderRequest IVRreq = new Utility.Pay.UnionPay.Interface.IVR.Request.PreOrderRequest()
                        {
                            SendTime = DateTime.Now,
                            SendSeqID = Guid.NewGuid().ToString("N"),
                            FrontUrl = string.IsNullOrEmpty(para.ReturnUrl) ? ConfigurationManager.AppSettings["UnionPayCallBackUrl"] : para.ReturnUrl,
                            BackUrl = ConfigurationManager.AppSettings["UnionPayIVRNotifyUrl"].Trim('?') + string.Format("?ChannelID={0}", channel.ChannelID),
                            MerchantOrderDesc = entity.AppOrderDesc,
                            Mode = IVRModes.Callback,
                            TransTimeout = entity.AppOrderTime,
                            MerchantOrderCurrency = Currencys.RMB,
                            MerchantOrderAmt = entity.AppOrderAmount,
                            MerchantID = unionIVRPaychannel.MerchantID,
                            MerchantOrderTime = entity.AppOrderTime,
                            MerchantOrderID = entity.OrderID.ToString(),
                            MobileNum = entity.MobileNO
                        };
                        requestJson = IVRreq.ToJSON();
                        var IvrResponse = IVRGateway.PreOrder(unionIVRPaychannel, IVRreq);
                        responseJson = IvrResponse.ToJSON();
                        if (IvrResponse.IsSuccess)
                        {
                            entity.Status = 1;
                            bll.Update(entity);
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("银联IVR创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, IVRreq.ToJSON(), IvrResponse.ToJSON()) });
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("银联IVR创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, IVRreq.ToJSON(), IvrResponse.ToJSON()) });
                            response.ResultCode = 200;
                            response.Message = IvrResponse.Description;
                        }
                        #endregion
                        break;
                    case 3:
                        recordEntity.Platform = 2;
                        #region 阿里Wap支付
                        AliPayChannel aliPayWapChannel = channel.ChannelParameters.DeserializeJSONTo<AliPayChannel>();
                        AliPayWapTokenRequest tokenRequest = new AliPayWapTokenRequest(aliPayWapChannel)
                        {
                            CallBackUrl = string.IsNullOrEmpty(para.ReturnUrl) ? ConfigurationManager.AppSettings["AliPayCallBackUrl"] : para.ReturnUrl,
                            NotifyUrl = ConfigurationManager.AppSettings["AlipayWapNotify"].Trim('?') + string.Format("?ChannelID={0}", channel.ChannelID),
                            OutTradeNo = entity.OrderID.ToString(),
                            Partner = aliPayWapChannel.Partner,
                            SellerAccountName = aliPayWapChannel.SellerAccountName,
                            Subject = entity.AppOrderDesc,
                            TotalFee = Math.Round((Convert.ToDecimal(entity.AppOrderAmount) / 100), 2).ToString(),
                            ReqID = Guid.NewGuid().ToString().Replace("-", "")
                        };
                        requestJson = tokenRequest.ToJSON();
                        var aliPayWapResponse = AliPayWapGeteway.GetQueryTradeResponse(tokenRequest, aliPayWapChannel);
                        responseJson = aliPayWapResponse.ToJSON();
                        if (aliPayWapResponse.IsSucess)
                        {
                            entity.PayUrl = aliPayWapResponse.RedirectURL;
                            entity.Status = 1;
                            bll.Update(entity);
                            url = aliPayWapResponse.RedirectURL;
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayWap创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, tokenRequest.ToJSON(), aliPayWapResponse.ToJSON()) });
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayWap创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, tokenRequest.ToJSON(), aliPayWapResponse.ToJSON()) });
                            response.ResultCode = 300;
                            response.Message = aliPayWapResponse.Message;
                        }
                        #endregion
                        break;
                    case 4:
                        recordEntity.Platform = 2;
                        #region 阿里OffLine支付
                        AliPayChannel aliPayChannel = channel.ChannelParameters.DeserializeJSONTo<AliPayChannel>();
                        //根据DynamicID判断是否预定单支付,DynamicID为空或者Null时调用OffLine预订单接口
                        if (string.IsNullOrWhiteSpace(para.DynamicID))
                        {
                            OfflineQRCodePreRequest qrRequest = new OfflineQRCodePreRequest(aliPayChannel)
                            {
                                OutTradeNo = entity.OrderID.ToString(),
                                NotifyUrl = ConfigurationManager.AppSettings["AlipayOfflineNotify"].Trim('?') + string.Format("?ChannelID={0}", channel.ChannelID),
                                Subject = entity.AppOrderDesc,
                                TotalFee = Math.Round((Convert.ToDecimal(entity.AppOrderAmount) / 100), 2).ToString(),
                                //下面是测试数据,正式须更改
                                ExtendParams = new
                                {
                                    MACHINE_ID = "BJ_001",//?
                                    AGENT_ID = aliPayChannel.AgentID,
                                    STORE_TYPE = "0",//?
                                    STORE_ID = "12314",//?
                                    TERMINAL_ID = "111",//?
                                    SHOP_ID = "only"//?
                                }.ToJSON()
                            };
                            requestJson = qrRequest.ToJSON();
                            var offlineQrResponse = AliPayOffLineGeteway.OfflineQRPay(qrRequest);
                            responseJson = offlineQrResponse.ToJSON();
                            if (offlineQrResponse.IsSucess)
                            {
                                entity.Status = 1;
                                entity.PayUrl = offlineQrResponse.PicUrl;
                                bll.Update(entity);
                                response.QrCodeUrl = offlineQrResponse.PicUrl;
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline二维码支付创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, qrRequest.ToJSON(), offlineQrResponse.ToJSON()) });
                            }
                            else
                            {
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline二维码支付创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, qrRequest.ToJSON(), offlineQrResponse.ToJSON()) });
                                response.ResultCode = 400;
                                response.Message = offlineQrResponse.DetailErrorCode + ":" + offlineQrResponse.DetailErrorDes;
                            }
                        }
                        else
                        {
                            CreateAndPayRequest createAndPayrequest = new CreateAndPayRequest(aliPayChannel)
                            {
                                Subject = entity.AppOrderDesc,
                                TotalFee = Math.Round((Convert.ToDecimal(entity.AppOrderAmount) / 100), 2).ToString(),
                                NotifyUrl = ConfigurationManager.AppSettings["AlipayOfflineNotify"].Trim('?') + string.Format("?ChannelID={0}", channel.ChannelID),
                                OutTradeNo = entity.OrderID.ToString(),
                                DynamicIDType = para.DynamicIDType,
                                DynamicID = para.DynamicID,
                            };
                            if (!string.IsNullOrEmpty(aliPayChannel.AgentID))
                            {
                                createAndPayrequest.ExtendParams = (new
                                {
                                    AGENT_ID = aliPayChannel.AgentID,
                                    MACHINE_ID = "BJ_001",
                                    STORE_TYPE = "0",
                                    STORE_ID = "BJ_ZZ_001",
                                    TERMINAL_ID = "A80001",
                                    SHOP_ID = "only"
                                }).ToJSON();
                            }
                            requestJson = createAndPayrequest.ToJSON();
                            var offlineCreateAndPayResponse = AliPayOffLineGeteway.OfflineCreateAndPay(createAndPayrequest);
                            responseJson = offlineCreateAndPayResponse.ToJSON();
                            if (offlineCreateAndPayResponse.IsSuccess)
                            {
                                entity.Status = 2;
                                bll.Update(entity);
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline即支付创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, createAndPayrequest.ToJSON(), offlineCreateAndPayResponse.ToJSON()) });
                            }
                            else if (offlineCreateAndPayResponse.ResultCode == ResultCodes.ORDER_SUCCESS_PAY_FAIL.ToString())
                            {
                                entity.Status = 1;
                                bll.Update(entity);
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline即支付创建订单成功{0},支付失败【请求】:{1}{0}【响应】:{2}", Environment.NewLine, createAndPayrequest.ToJSON(), offlineCreateAndPayResponse.ToJSON()) });
                            }
                            else if (offlineCreateAndPayResponse.ResultCode == ResultCodes.ORDER_SUCCESS_PAY_INPROCESS.ToString())
                            {
                                entity.Status = 1;
                                bll.Update(entity);
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline即支付创建订单成功{0},支付处理中【请求】:{1}{0}【响应】:{2}", Environment.NewLine, createAndPayrequest.ToJSON(), offlineCreateAndPayResponse.ToJSON()) });
                            }
                            else
                            {
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline即支付创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, createAndPayrequest.ToJSON(), offlineCreateAndPayResponse.ToJSON()) });
                                response.ResultCode = 400;
                                response.Message = offlineCreateAndPayResponse.DetailErrorCode + ":" + offlineCreateAndPayResponse.DetailErrorDes;
                            }
                        }
                        #endregion
                        break;
                    case 5://Native
                    case 6://微信JS      
                    case 7://微信App
                        recordEntity.Platform = 3;
                        #region 微信Native支付,JS支付
                        //把channel里的参数传了过去
                        WeiXinPayHelper helper = new WeiXinPayHelper(channel.ChannelParameters.DeserializeJSONTo<WeiXinPayHelper.Channel>());
                        entity.PayUrl = ConfigurationManager.AppSettings["WeiXinPrePay"];
                        entity.NotifyUrl = ConfigurationManager.AppSettings["WeiXinPayNotify"];
                        WeiXinPayHelper.PrePayResult result = null;

                        if (para.PaymentMode == 1)//PaymentMode=1标示微信扫码支付进入
                        {
                            helper.channel.trade_type = "NATIVE";
                            channel.PayType = 5; //走扫码回调返回参数
                        }

                        Loggers.Debug(new DebugLogInfo() { Message = "isSpPay:" + helper.channel.isSpPay });

                        if (helper.channel.isSpPay == "1") //isSpPay=1 服务商支付
                        {
                            result = helper.serPrePay(entity);//统一下单，服务商支付
                        }
                        else
                        {
                            result = helper.prePay(entity);//统一下单，获取微信网页支付的预支付信息，app支付和js支付是一样的
                        }

                        Loggers.Debug(new DebugLogInfo() { Message = "微信支付_预支付返回结果:" + result.ToJSON() });
                        requestJson = helper.prePayRequest;
                        responseJson = helper.prePayResponse;
                        if (result.return_code == "SUCCESS" && result.result_code == "SUCCESS")
                        {
                            if (channel.PayType == 6)
                            {
                                response.WXPackage = helper.getJsParamater(result);
                            }
                            else if (channel.PayType == 7)//后面传的参数和js支付的就不一样了
                            {
                                response.WXPackage = helper.getAppParamater(result);
                                response.OrderID = entity.OrderID;
                            }
                            else//Native
                            {
                                response.WXPackage = result.ToJSON();
                                response.QrCodeUrl = result.code_url;
                            }
                            Loggers.Debug(new DebugLogInfo() { Message = "WXPackage:" + response.WXPackage });
                        }
                        else
                        {
                            response.ResultCode = 101;//支付失败
                            if (!string.IsNullOrEmpty(result.return_msg))//不同错误，错误信息位置不同
                                response.Message = result.return_msg;
                            else
                                response.Message = result.err_code_des;
                        }

                        #endregion
                        break;
                    case 8://旺财支付
                        return string.Empty;
                    case 9://新版支付宝扫码支付
                        #region
                        AliPayChannel aliPayScanChannel = channel.ChannelParameters.DeserializeJSONTo<AliPayChannel>();
                        RequestScanEntity scRequest = new RequestScanEntity(aliPayScanChannel);
                        scRequest.notify_url = "" + ConfigurationManager.AppSettings["AlipayOfflineNotify"];

                        RequstScanDetail scanDetail = new RequstScanDetail();
                        scanDetail.out_trade_no = "" + entity.OrderID;
                        scanDetail.total_amount = "" + Math.Round((Convert.ToDecimal(entity.AppOrderAmount) / 100), 2);
                        scanDetail.seller_id = aliPayScanChannel.Partner;
                        scanDetail.subject = entity.AppOrderDesc;

                        var scResponseJson = AliPayScanGeteway.GetResponseStr(scRequest, scanDetail.ToJSON(), aliPayScanChannel.RSA_PrivateKey);
                        var scResponse = scResponseJson.DeserializeJSONTo<ResponsetScanEntity>();

                        if (scResponse.alipay_trade_precreate_response.code == "10000")
                        {
                            response.QrCodeUrl = scResponse.alipay_trade_precreate_response.qr_code;
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("aliPayScanChannel二维码支付创建订单成功{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, requestJson, scResponseJson) });
                        }
                        else
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("AliPayOffline二维码支付创建订单失败{0}【请求】:{1}{0}【响应】:{2}", Environment.NewLine, requestJson, scResponseJson) });
                            response.ResultCode = 400;
                            response.Message = scResponse.alipay_trade_precreate_response.code + ":" + scResponse.alipay_trade_precreate_response.msg;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                recordEntity.RequestJson = requestJson;
                recordEntity.ResponseJson = responseJson;
                recordBll.Create(recordEntity);
            }
            catch (Exception ex)
            {
                recordEntity.RequestJson = requestJson;
                recordEntity.ResponseJson = responseJson;
                recordBll.Create(recordEntity);
                throw ex;
            }
            #endregion
            return url;
        }
        #endregion

        #region 查询订单信息
        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public static object QueryOrder(TradeRequest pRequest)
        {
            QueryOrderResponse response = new QueryOrderResponse();
            var user = pRequest.GetUserInfo();
            AppOrderBLL bll = new AppOrderBLL(user);
            var para = pRequest.GetParameter<QueryOrderParameters>();
            var entity = bll.GetByID(para.OrderID);
            if (entity != null)
            {
                response.Status = entity.Status;
            }
            else
            {
                response.Message = "无此订单信息";
            }
            return response;
        }
        #endregion

        #region 验证订单是否支付
        /// <summary>
        /// 验证订单是否支付
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        internal static bool IsOrderPaid(TradeRequest pRequest)
        {
            var para = pRequest.GetParameter<QueryOrderByAppInfoParameters>();
            var bll = new AppOrderBLL(pRequest.GetUserInfo());
            var entity = bll.GetByAppInfo(pRequest.AppID.Value, para.AppOrderID, pRequest.ClientID);
            return entity.Status == 2;
        }
        #endregion

        #region 创建微信支付NativePay连接
        /// <summary>
        /// 创建微信支付NativePay连接
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static object CreateWXNativePayUrl(TradeRequest request)
        {
            CreateWXNativePayUrlReqPara para = request.GetParameter<CreateWXNativePayUrlReqPara>();
            var channelBll = new PayChannelBLL(new Utility.BasicUserInfo());
            var channel = channelBll.GetByID(para.PayChannelID);
            var WXChannel = channel.ChannelParameters.DeserializeJSONTo<WeiXinPayChannel>();
            NativePayHelper req = new NativePayHelper(WXChannel);
            return new { NativePayUrl = req.GenerateNativeUrl(para.ProductID) };
        }
        #endregion

        #region 获取微信签名信息
        /// <summary>
        /// 获取微信签名信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static object WXGetSign(TradeRequest request)
        {
            WXGetSignReqPara para = request.GetParameter<WXGetSignReqPara>();
            var channelBll = new PayChannelBLL(new Utility.BasicUserInfo());
            var channel = channelBll.GetByID(para.PayChannelID);
            if (channel.PayType != 5 || channel.PayType != 6)
            {
                throw new Exception("非微信支付通道,不能调用些接口");
            }
            var WXChannel = channel.ChannelParameters.DeserializeJSONTo<WeiXinPayChannel>();
            var newSign = CommonUtil.CreateSign(para.NoSignDic, WXChannel);
            return new { Sign = newSign };
        }
        #endregion

        #region 更新微信反馈
        /// <summary>
        /// 更新微信反馈
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static object WXGetUpdateFeedBackUrl(TradeRequest request)
        {
            WXGetUpdateFeedBackReqPara para = request.GetParameter<WXGetUpdateFeedBackReqPara>();
            var channelBll = new PayChannelBLL(new Utility.BasicUserInfo());
            var channel = channelBll.GetByID(para.PayChannelID);
            var WXChannel = channel.ChannelParameters.DeserializeJSONTo<WeiXinPayChannel>();
            string url = WeiXinPayGateway.GetUpdateFeedBackUrl(WXChannel, para.FeedBackID, para.OpenID);
            return new { Url = url };
        }
        #endregion

        #region 设置支付渠道
        /// <summary>
        /// 设置支付渠道
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static object SetPayChannel(TradeRequest request)
        {
            var paras = request.GetParameter<SetPayChannel>();

            var payChannelResponsList = new PayChannelResponsList();
            var payChannelList = new PayChannelResponse();
            payChannelResponsList.PayChannelIdList = new List<PayChannelResponse>();
            foreach (var para in paras.AddPayChannelData)
            {
                var payType = para.PayType;
                var notifyUrl = para.NotifyUrl;
                var payChannelBll = new PayChannelBLL(new Utility.BasicUserInfo());
                var payChannelEntity = new PayChannelEntity();

                var channelId = para.ChannelId;

                if (payType == 0)
                {
                    throw new Exception("支付类型不能为空");
                }

                #region 银联支付
                if (payType == 1 || payType == 2)
                {
                    var unionPayData = para.UnionPayData;

                    if (unionPayData.MerchantID == null || string.IsNullOrEmpty(unionPayData.MerchantID))
                    {
                        throw new Exception("缺少参数【MerchantID】");
                    }
                    if (unionPayData.DecryptCertificateFilePath == null || string.IsNullOrEmpty(unionPayData.DecryptCertificateFilePath))
                    {
                        throw new Exception("缺少参数【DecryptCertificateFilePath】");
                    }
                    if (unionPayData.PacketEncryptKey == null || string.IsNullOrEmpty(unionPayData.PacketEncryptKey))
                    {
                        throw new Exception("缺少参数【PacketEncryptKey】");
                    }
                    if (unionPayData.CertificateFilePath == null || string.IsNullOrEmpty(unionPayData.CertificateFilePath))
                    {
                        throw new Exception("缺少参数【CertificateFilePath】");
                    }
                    if (unionPayData.CertificateFilePassword == null || string.IsNullOrEmpty(unionPayData.CertificateFilePassword))
                    {
                        throw new Exception("缺少参数【CertificateFilePassword】");
                    }

                    var unionpay = new UnionPayInfo();

                    #region 解密
                    var decFilePath = unionPayData.DecryptCertificateFilePath;

                    var decFileName = decFilePath.Substring(decFilePath.LastIndexOf('/') + 1, decFilePath.Length - decFilePath.LastIndexOf('/') - 1);

                    var uploadFilePath = HttpContext.Current.Server.MapPath("/PayCenter/");

                    FileSystem.CreateDirectoryIfNotExists(uploadFilePath);

                    SaveFiles(decFilePath, uploadFilePath + decFileName);
                    #endregion

                    #region 加密

                    var creFilePath = unionPayData.CertificateFilePath;

                    var creFileName = creFilePath.Substring(creFilePath.LastIndexOf('/') + 1, creFilePath.Length - creFilePath.LastIndexOf('/') - 1);

                    FileSystem.CreateDirectoryIfNotExists(uploadFilePath);

                    SaveFiles(creFilePath, uploadFilePath + creFileName);

                    #endregion


                    unionpay.MerchantID = unionPayData.MerchantID;
                    unionpay.PacketEncryptKey = unionPayData.PacketEncryptKey;
                    unionpay.DecryptCertificateFilePath = uploadFilePath + decFileName;
                    unionpay.CertificateFilePath = uploadFilePath + creFileName;
                    unionpay.CertificateFilePassword = unionPayData.CertificateFilePassword;

                    payChannelEntity.ChannelParameters = unionpay.ToJSON();

                }
                #endregion

                #region 支付宝

                if (payType == 3)
                {
                    var wapData = para.WapData;

                    if (wapData.Partner == null || string.IsNullOrEmpty(wapData.Partner))
                    {
                        throw new Exception("缺少参数【Partner】");
                    }
                    if (wapData.SellerAccountName == null || string.IsNullOrEmpty(wapData.SellerAccountName))
                    {
                        throw new Exception("缺少参数【SellerAccountName】");
                    }
                    //if (wapData.MD5Key == null || string.IsNullOrEmpty(wapData.MD5Key))
                    //{
                    //    throw new Exception("缺少参数【MD5Key】");
                    //}
                    if (wapData.RSA_PublicKey == null || string.IsNullOrEmpty(wapData.RSA_PublicKey))
                    {
                        throw new Exception("缺少参数【RSA_PublicKey】");
                    }
                    if (wapData.RSA_PrivateKey == null || string.IsNullOrEmpty(wapData.RSA_PrivateKey))
                    {
                        throw new Exception("缺少参数【RSA_PrivateKey】");
                    }
                    payChannelEntity.ChannelParameters = wapData.ToJSON();
                }
                if (payType == 4)
                {
                    var wapData = para.WapData;

                    if (wapData.Partner == null || string.IsNullOrEmpty(wapData.Partner))
                    {
                        throw new Exception("缺少参数【Partner】");
                    }
                    if (wapData.MD5Key == null || string.IsNullOrEmpty(wapData.MD5Key))
                    {
                        throw new Exception("缺少参数【MD5Key】");
                    }

                    wapData.AgentID = "8582928j2";
                    payChannelEntity.ChannelParameters = wapData.ToJSON();


                }
                #endregion

                #region 微信
                if (payType == 5 || payType == 6)
                {
                    var wxPayData = para.WxPayData;
                    if (wxPayData.AppID == null || string.IsNullOrEmpty(wxPayData.AppID))
                    {
                        throw new Exception("缺少参数【AppID】");
                    }
                    //if (wxPayData.AppSecret == null || string.IsNullOrEmpty(wxPayData.AppSecret))
                    //{
                    //    throw new Exception("缺少参数【AppSecret】");
                    //}
                    //if (wxPayData.ParnterID == null || string.IsNullOrEmpty(wxPayData.ParnterID))
                    //{
                    //    throw new Exception("缺少参数【ParnterID】");
                    //}
                    //if (wxPayData.ParnterKey == null || string.IsNullOrEmpty(wxPayData.ParnterKey))
                    //{
                    //    throw new Exception("缺少参数【ParnterKey】");
                    //}
                    //if (wxPayData.PaySignKey == null || string.IsNullOrEmpty(wxPayData.PaySignKey))
                    //{
                    //    throw new Exception("缺少参数【PaySignKey】");
                    //}
                    if (wxPayData.Mch_ID == null || string.IsNullOrEmpty(wxPayData.Mch_ID))
                    {
                        throw new Exception("缺少参数【Mch_ID】");
                    }
                    if (wxPayData.SignKey == null || string.IsNullOrEmpty(wxPayData.SignKey))
                    {
                        throw new Exception("缺少参数【SignKey】");
                    }
                    if (wxPayData.Trade_Type == null || string.IsNullOrEmpty(wxPayData.Trade_Type))
                    {
                        throw new Exception("缺少参数【Trade_Type】");
                    }
                    payChannelEntity.ChannelParameters = wxPayData.ToJSON();

                }
                #endregion

                channelId = payChannelBll.GetMaxChannelId();
                payChannelEntity.ChannelID = channelId;
                payChannelEntity.IsTest = false;
                payChannelEntity.NotifyUrl = notifyUrl;
                payChannelEntity.PayType = payType;
                payChannelBll.Create(payChannelEntity);

                payChannelList.ChannelId = channelId;

                payChannelResponsList.PayChannelIdList.Add(payChannelList);

            }
            return payChannelResponsList;
        }
        #endregion

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        internal static void SaveFiles(string url, string savePath)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Referer = "http://";
            var rs = (HttpWebResponse)req.GetResponse();
            var st = rs.GetResponseStream();

            var fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
            if (st != null)
            {
                st.Flush();

                var sr = new StreamReader(st);
                string buffer = sr.ReadToEnd();
                byte[] bt = new System.Text.UTF8Encoding(true).GetBytes(buffer);
                fs.Write(bt, 0, bt.Length);
            }
            fs.Close();
        }
        #endregion

        #region 使用汇付储值卡支付订单
        /// <summary>
        /// 使用汇付储值卡支付订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static PrePaidCardPayRD PrePaidCardPay(TradeRequest request)
        {
            PrePaidCardPayRD rd = new PrePaidCardPayRD();
            var rp = request.GetParameter<PrePaidCardPayRP>();
            var userInfo = request.GetUserInfo();

            if (rp == null || string.IsNullOrEmpty(rp.OrderId) ||
                string.IsNullOrEmpty(rp.CardNo) || string.IsNullOrEmpty(rp.Password))
            {
                rd.errcode = 40000;
                rd.errmsg = "参数错误";
                return rd;
            }

            try
            {
                var appBll = new AppOrderBLL(userInfo);
                var appOrder = appBll.GetAppOrderByAppOrderId(rp.OrderId);

                if (appOrder == null)
                {
                    rd.errcode = 40001;
                    rd.errmsg = "未找到支付订单，请重试！";
                    return rd;
                }

                if (CacheOrder.Contains(rp.OrderId))
                {
                    rd.errcode = 40003;
                    rd.errmsg = "订单支付中，请稍后重试";
                    return rd;
                }
                CacheOrder.Add(rp.OrderId); // 限制重复提交支付，只针对单服务器程序，多服务请走第三方缓存

                if (appOrder.Status == 2)
                {
                    rd.errcode = 0;
                    rd.errmsg = "该订单已支付";
                    return rd;
                }
                string msg = string.Empty;
                var result = Consumption(rp, appOrder, out msg);

                if (result.rspCd == "0000")
                {
                    rd.errcode = 200;
                    rd.errmsg = "success";

                    AppOrderBLL bll = new AppOrderBLL(new Utility.BasicUserInfo());
                    if (!(appOrder.IsNotified ?? false))
                    {
                        Task.Factory.StartNew(() =>
                        {//起一个新线程通知业务系统处理订单
                            try
                            {
                                string errmsg;
                                if (NotifyHandler.Notify(appOrder, out errmsg))
                                {
                                    appOrder.IsNotified = true;
                                }
                                else
                                {
                                    appOrder.NextNotifyTime = DateTime.Now.AddMinutes(1);
                                }
                                //通知完成,通知次数+1

                                appOrder.NotifyCount = (appOrder.NotifyCount ?? 0) + 1;
                                bll.Update(appOrder);
                            }
                            catch (Exception ex)
                            {
                                Loggers.Exception(new ExceptionLogInfo(ex));
                            }
                        });
                    }
                }
                else
                {
                    rd.errcode = 40002;
                    rd.errmsg = result.rspDesc;
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
            }
            finally
            {
                CacheOrder.Remove(rp.OrderId);
            }
            return rd;
        }
        #endregion

        #region 调用汇付支付接口
        /// <summary>
        /// 调用汇付支付接口
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="appOrder"></param>
        /// <returns></returns>
        private static BaseRsponse Consumption(PrePaidCardPayRP rp, AppOrderEntity appOrder, out string errMsg)
        {
            errMsg = string.Empty;
            string url = PrePaidCardUtil.GetTonyRechargeUrl();
            string desKey = PrePaidCardUtil.GetEncodingKey();
            string merchantCode = PrePaidCardUtil.GetMerchantCode();
            BaseRequest<RechargeReqBody> req = new BaseRequest<RechargeReqBody>();
            var recordBll = new PayRequestRecordBLL(new Utility.BasicUserInfo());
            int amount = appOrder.AppOrderAmount.Value;
            var recordEntity = new PayRequestRecordEntity()
            {
                ChannelID = appOrder.PayChannelID,
                ClientID = appOrder.AppClientID,
                UserID = appOrder.AppUserID,
                Platform = 8
            };
            req.bizCd = HuiFuInterfaceEnum.consumption.ToString();
            req.reqSeq = DateTime.Now.ToString("yyMMddHHmmss");
            req.reqBody = new RechargeReqBody();
            req.reqBody.orderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            req.reqBody.transDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            req.reqBody.transAmt = (amount * 100).ToString();
            // req.reqBody.cardNo = "9300100203000020002";
            // req.reqBody.transAmt = "10";    // 交易金额以分为单位            
            req.reqBody.cardNo = rp.CardNo;
            req.reqBody.passWord = rp.Password;

            string reqJson = JsonMapper.ToJson(req).Replace("\\\"", "'");
            string strReq = CommonUtil.EncryptDES(reqJson, desKey);
            url = string.Format("{0}{1}&merchantCode={2}", url, HttpContext.Current.Server.UrlEncode(strReq), merchantCode);
            string result = HttpService.Get(url);

            string strRsp = CommonUtil.DecryptDES(result);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("多利【{0}】充值结果：", appOrder.AppOrderID) + strRsp,
            });
            recordEntity.RequestJson = reqJson;
            recordEntity.ResponseJson = strRsp;
            recordBll.Create(recordEntity);

            var rspEntity = JsonMapper.ToObject<BaseRsponse>(strRsp);
            // var rsp = rspEntity.rspCd == "0000" ? "SUCCESS" : "FAIL";
            return rspEntity;
        }
        #endregion
    }
}