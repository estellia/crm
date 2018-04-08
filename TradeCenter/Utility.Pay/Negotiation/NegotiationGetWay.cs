using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Negotiation.Interface;
using JIT.CPOS.BS.Web.ApplicationInterface.Negotiation.Util;
using System.Web;
using System.Net;
using System.Configuration;
using JIT.Utility.Pay.Negotiation.Interface.Request;
using JIT.Utility.Pay.Negotiation.Interface.Response;
using JIT.Utility.Pay.UnionPay.Interface;
using System.Xml;



namespace JIT.Utility.Pay.Negotiation
{
    public class NegotiationGetWay
    {
        static string url = "http://58.246.136.11:8016/hxb/qz/doBusiness.html";
        public static ReqResponse CallAPI<TRequest>(NegotiationPayChannel pChannel, TRequest pRequest)
            where TRequest : NeBaseAPIRequest
        {
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            var reqContent = pRequest.GetContent();
            var merchantID = JIT.CPOS.BS.Web.ApplicationInterface.Negotiation.Util.EncDecUtil.Base64Encrypt(pChannel.MerchantID);
            string CertificateFilePath = HttpContext.Current.Server.MapPath("~/" + pChannel.CertificateFilePath);

            RSAGenerator rsa = new RSAGenerator();
            string key = rsa.getPublicKey();
            var desKey = EncDecUtil.GetEncryptKey(key);   //获取实际加密密钥
            //采用Base64(RSA(加密密钥))计算出报文中的加密密钥
            var encryptedDesKey = EncDecUtil.Base64Encrypt(EncDecUtil.RSAEncrypt(CertificateFilePath, pChannel.CertificateFilePassword, desKey + "11111111"));
            //采用Base64(3DES(报文))加密报文
            var encryptedContent = EncDecUtil.Base64Encrypt(EncDecUtil.TripleDESEncrypt(desKey, reqContent));
            //使用md5生成摘要
            var md5 = EncDecUtil.CreateMD5Encod(reqContent);
            var requestContent = merchantID + "|" + encryptedDesKey + "|" + encryptedContent + "|" + md5;
            using (WebClient wc = new WebClient())
            {
                string strResponse = wc.UploadString(url, requestContent);
                string[] responseSections = strResponse.Split('|');
                string rspCode = responseSections[0];
                string rspContent = responseSections[1];
                if (rspCode == "1")
                {
                    var decRspBytes = EncDecUtil.TripleDESDecrypt(desKey, EncDecUtil.Base64Decrypt(rspContent));
                    string decRspContent = Encoding.UTF8.GetString(decRspBytes);
                    decRspContent = decRspContent.Replace("\0", string.Empty);
                    ReqResponse req = new ReqResponse();
                    req.ResultCode = rspCode;
                    req.Data = decRspContent;
                    return req;
                }
                else
                {
                    string errorCode = rspContent;
                    string errorMsg = Encoding.UTF8.GetString(EncDecUtil.Base64Decrypt(responseSections[2]));
                    UnionPayException ex = new UnionPayException(errorMsg);
                    ex.Code = errorCode;
                    Log.Loggers.Exception(new Log.ExceptionLogInfo(ex) { ErrorMessage = ex.ToString() });
                    ReqResponse req = new ReqResponse();
                    req.ResultCode = errorCode;
                    req.Message = errorMsg;
                    return req;
                }
            }

        }

        #region 待支付接口

        public static ReqResponse PayOrder(NegotiationPayChannel pChannel, PayRequest pRequest)
        {

            return NegotiationGetWay.CallAPI<PayRequest>(pChannel, pRequest);
        }

        public static ReqResponse BatchPayOrder(NegotiationPayChannel pChannel, BatchPayRequest pRequest)
        {
            return NegotiationGetWay.CallAPI<BatchPayRequest>(pChannel, pRequest);
        }
        public static TransationNotificationRequest ParseTransactionNotificationRequest(string pCerFilePath, string pEncryptedContent)
        {
            string decryptedContent = JIT.Utility.Pay.UnionPay.Util.PacketUtil.ParseRequestPackets(pCerFilePath, pEncryptedContent).ToString();
            TransationNotificationRequest req = new TransationNotificationRequest();
            string Content = decryptedContent.Replace("\0", "").Trim();

            req.Load(string.Format(Content));

            return req;
        }
        #endregion
    }
    public class ReqResponse
    {
        public string ResultCode { set; get; }
        public string Message { set; get; }
        public string Data { set; get; }
        public string MerchantSerial { set; get; }

    }
}
