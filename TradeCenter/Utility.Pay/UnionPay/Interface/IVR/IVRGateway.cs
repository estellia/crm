/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 13:35:46
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using JIT.Utility.Pay.UnionPay.Interface.IVR.Request;
using JIT.Utility.Pay.UnionPay.Interface.IVR.Response;
using JIT.Utility.Pay.UnionPay.Util;

namespace JIT.Utility.Pay.UnionPay.Interface.IVR
{
    /// <summary>
    /// IVR支付接口的门户
    /// </summary>
    public static class IVRGateway
    {
        /// <summary>
        /// 支付接口的地址
        /// </summary>
        static string API_URL = "http://210.51.61.171:10001/ivrPlatform/ivrMerReq!ivrMerReq.ac";
        //static string API_URL = "http://58.246.136.11:8006/wapDetect/gateWay!gate.ac";

        #region 工具方法
        /// <summary>
        /// 调用API
        /// </summary>
        /// <typeparam name="TRequest">向支付接口发送的请求的类型</typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="pChannel"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private static TResponse CallAPI<TRequest, TResponse>(UnionPayChannel pChannel, TRequest pRequest)
            where TRequest : BaseAPIRequest
            where TResponse : BaseAPIResponse, new()
        {
            //参数校验
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (string.IsNullOrWhiteSpace(pChannel.MerchantID))
                throw new ArgumentNullException("pChannel.MerchantID");
            if (pRequest == null)
                throw new ArgumentNullException("pRequest");
            //
            var reqContent = pRequest.GetContent();
            var merchantID = EncDecUtil.Base64Encrypt(pChannel.MerchantID);     //Base64加密商户ID
            var desKey = EncDecUtil.GetEncryptKey(pChannel.PacketEncryptKey);   //获取实际加密密钥
            //采用Base64(RSA(加密密钥))计算出报文中的加密密钥
            var encryptedDesKey = EncDecUtil.Base64Encrypt(EncDecUtil.RSAEncrypt(pChannel.CertificateFilePath, pChannel.CertificateFilePassword, desKey+"11111111"));
            //采用Base64(3DES(报文))加密报文
            var encryptedContent = EncDecUtil.Base64Encrypt(EncDecUtil.TripleDESEncrypt(desKey, reqContent));
            //
            var requestContent = merchantID + "|" + encryptedDesKey + "|" + encryptedContent;
            using (WebClient wc = new WebClient())
            {
                string strResponse = wc.UploadString(IVRGateway.API_URL, requestContent);
                string[] responseSections = strResponse.Split('|');
                string rspCode = responseSections[0];
                string rspContent = responseSections[1];
                //
                if (rspCode == "1")
                {
                    var decRspBytes = EncDecUtil.TripleDESDecrypt(desKey, EncDecUtil.Base64Decrypt(rspContent));
                    string decRspContent = Encoding.UTF8.GetString(decRspBytes);
                    decRspContent = decRspContent.Replace("\0", string.Empty);
                    TResponse rsp = new TResponse();
                    rsp.Load(decRspContent);
                    //
                    return rsp;
                }
                else
                {
                    string errorCode = rspContent;
                    string errorMsg = Encoding.UTF8.GetString(EncDecUtil.Base64Decrypt(responseSections[2]));
                    UnionPayException ex = new UnionPayException(errorMsg);
                    ex.Code = errorCode;
                    throw ex;
                }
            }
        }
        #endregion

        #region 支付接口
        /// <summary>
        /// 预订单
        /// <remarks>
        /// <para>商户平台 -> 支付平台</para>
        /// </remarks>
        /// </summary>
        /// <param name="pChannel">调用支付接口的基本设置信息</param>
        /// <param name="pRequest">预订单请求</param>
        public static PreOrderResponse PreOrder(UnionPayChannel pChannel, PreOrderRequest pRequest)
        {
            return IVRGateway.CallAPI<PreOrderRequest, PreOrderResponse>(pChannel, pRequest);
        }
        /// <summary>
        /// 查询订单
        /// <remarks>
        /// <para>商户平台 -> 支付平台</para>
        /// </remarks>
        /// </summary>
        /// <param name="pChannel">调用支付接口的基本设置信息</param>
        /// <param name="pRequest">订单查询请求</param>
        /// <returns></returns>
        public static QueryOrderResponse QueryOrder(UnionPayChannel pChannel, QueryOrderRequest pRequest)
        {
            return IVRGateway.CallAPI<QueryOrderRequest, QueryOrderResponse>(pChannel, pRequest);
        }

        /// <summary>
        /// 解析支付平台发送给商户平台的交易通知请求
        /// </summary>
        /// <param name="pCerFilePath">cer证书文件的路径</param>
        /// <param name="pEncryptedContent">支付平台发送过来的加密后的密文</param>
        /// <returns></returns>
        public static TransactionNotificationRequest ParseTransactionNotificationRequest(string pCerFilePath, string pEncryptedContent)
        {
            string decryptedContent = PacketUtil.ParseRequestPackets(pCerFilePath, pEncryptedContent);
            TransactionNotificationRequest req = new TransactionNotificationRequest();
            req.Load(decryptedContent);
            return req;
        }
        #endregion
    }
}
