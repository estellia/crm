using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.Wap;
using WAPRequest=JIT.Utility.Pay.UnionPay.Interface.Wap.Request;
using WAPResponse=JIT.Utility.Pay.UnionPay.Interface.Wap.Response;
using WAPValueObject=JIT.Utility.Pay.UnionPay.Interface.Wap.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.IVR;
using IVRRequest=JIT.Utility.Pay.UnionPay.Interface.IVR.Request;
using IVRResponse=JIT.Utility.Pay.UnionPay.Interface.IVR.Response;
using IVRValueObject=JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;


namespace JIT.TestUtility.TestPay.Web.UnionPay
{
    /// <summary>
    /// ALD的支付入口
    /// </summary>
    public class ALDGateway : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strRequest = context.Request.Params["ReqContent"];
                //if (string.IsNullOrEmpty(strRequest))
                //{
                //    strRequest = "{\"Token\":\"\",\"CityID\":21,\"UserID\":\"FBEE2CFA-500C-4756-BFAA-000014308165\",\"Locale\":1,\"Parameters\":{\"OrderAmount\":1,\"OrderDesc\":\"测试支付\",\"OrderTime\":\"2013-12-30 16:24:33 367\",\"OrderID\":\"57F2DB3A51EB472AED32E413AA986854\",\"MobileNO\":\"13817218367\",\"PayType\":1}}";
                //}
                var request = strRequest.DeserializeJSONTo<ALDRequest<PayOrderRequest>>();
                switch (request.Parameters.PayType.Value)
                {
                    case 1:
                        #region WAP 下单
                        {
                            //创建支付通道
                            UnionPayChannel channel = new UnionPayChannel()
                            {
                                CertificateFilePassword = ConfigurationManager.AppSettings["WAPEncryptCertificateFilePassword"]
                                ,
                                CertificateFilePath = ConfigurationManager.AppSettings["WAPEncryptCertificateFilePath"]
                                ,
                                MerchantID = ConfigurationManager.AppSettings["WAPMerchantID"]
                                ,
                                PacketEncryptKey = "654321"
                            };
                            //下订单
                            WAPRequest.PreOrderRequest req = new WAPRequest.PreOrderRequest();
                            req.SendTime = DateTime.Now;
                            req.SendSeqID = Guid.NewGuid().ToString("N");
                            req.FrontUrl = ConfigurationManager.AppSettings["WAPFrontUrl"];    //商户平台的页面,用户支付完毕后跳转的页面
                            req.MerchantOrderDesc = request.Parameters.OrderDesc;
                            req.Misc = string.Empty;
                            req.TransTimeout = DateTime.Now.AddHours(1);
                            req.BackUrl = ConfigurationManager.AppSettings["WAPBackUrl"];      //当用户支付完成后,支付平台回调的交易通知的页面
                            req.MerchantOrderCurrency = Currencys.RMB;
                            req.MerchantOrderAmt = request.Parameters.OrderAmount;
                            req.MerchantID = channel.MerchantID;
                            req.MerchantOrderTime = DateTime.ParseExact(request.Parameters.OrderTime, "yyyy-MM-dd HH:mm:ss fff", null);
                            req.MerchantOrderID = request.Parameters.OrderID;
                            req.MerchantUserID = string.Empty;
                            req.MobileNum = request.Parameters.MobileNO;      //消费者的手机号,支付平台会将验证码发送到该手机
                            req.CarNum = string.Empty;
                            //记录支付请求日志
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[Wap]PreOrder Request={0}", req.GetContent()) });
                            //支付平台成功接收
                            var rsp = WapGateway.PreOrder(channel, req);
                            if (rsp.IsSuccess)
                            {
                                context.Response.Write(rsp.RedirectURL);
                            }
                        }
                        #endregion
                        break;
                    case 2:
                        #region 语音下单
                        {
                            //创建支付通道
                            UnionPayChannel channel = new UnionPayChannel()
                            {
                                CertificateFilePassword = ConfigurationManager.AppSettings["IVREncryptCertificateFilePassword"]
                                ,
                                CertificateFilePath = ConfigurationManager.AppSettings["IVREncryptCertificateFilePath"]
                                ,
                                MerchantID = ConfigurationManager.AppSettings["IVRMerchantID"]
                                ,
                                PacketEncryptKey = "654321"
                            };
                            //下订单
                            IVRRequest.PreOrderRequest req = new IVRRequest.PreOrderRequest();
                            req.SendTime = DateTime.Now;
                            req.SendSeqID = Guid.NewGuid().ToString("N");
                            req.FrontUrl = ConfigurationManager.AppSettings["IVRFrontUrl"];    //商户平台的页面,用户支付完毕后跳转的页面
                            req.MerchantOrderDesc = request.Parameters.OrderDesc;
                            req.Misc = string.Empty;
                            req.Mode = IVRValueObject.IVRModes.Callback;
                            req.TransTimeout = DateTime.Now.AddHours(1);
                            req.BackUrl = ConfigurationManager.AppSettings["IVRBackUrl"];      //当用户支付完成后,支付平台回调的交易通知的页面
                            req.MerchantOrderCurrency = Currencys.RMB;
                            req.MerchantOrderAmt = request.Parameters.OrderAmount;
                            req.MerchantID = channel.MerchantID;
                            req.MerchantOrderTime = DateTime.ParseExact(request.Parameters.OrderTime, "yyyy-MM-dd HH:mm:ss fff", null);
                            req.MerchantOrderID = request.Parameters.OrderID;
                            req.MerchantUserID = string.Empty;
                            req.MobileNum = request.Parameters.MobileNO;      //消费者的手机号,支付前置会将验证码发送到该手机
                            req.CarNum = string.Empty;
                            //记录支付请求日志
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]PreOrder Request={0}", req.GetContent()) });
                            //支付平台成功接收
                            var rsp = IVRGateway.PreOrder(channel, req);
                            //
                            if (rsp.IsSuccess)
                            {
                                context.Response.Write("OK");
                            }
                            else
                            {
                                context.Response.Write("Failed");
                            }
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                JIT.Utility.Log.Loggers.Exception(new ExceptionLogInfo(ex));
                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class ALDRequest<T> where T:class
        {
            public int? Locale { get; set; }
            public Guid? UserID { get; set; }
            public int? BusinessZoneID { get; set; }
            public string Token { get; set; }
            public T Parameters { get; set; }
        }

        class PayOrderRequest
        {
            public int? PayType { get; set; }
            public string MobileNO { get; set; }
            public string OrderID { get; set; }
            public int? OrderAmount { get; set; }
            public string OrderTime { get; set; }
            public string OrderDesc { get; set; }
        }

        class ALDResponse<T> where T : class
        {
            public int? ResultCode { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }

        }
        //class PayOrderResponse
        //{
        //    public string 
        //}
    }
}