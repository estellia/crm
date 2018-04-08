/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 14:55:10
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
using System.Text;

namespace JIT.Utility.Pay.UnionPay.Util
{
    /// <summary>
    /// 支付接口报文的助手
    /// <remarks>
    /// <para>解析报文和封报文</para>
    /// </remarks>
    /// </summary>
    public static class PacketUtil
    {
        /// <summary>
        /// 解析请求报文
        /// <remarks>
        /// <para>请求报文是支付前置发给商户平台的,用于通知API</para>
        /// </remarks>
        /// </summary>
        /// <param name="pCertFilePath">cer证书文件路径</param>
        /// <param name="pCertFilePassword">pfx证书文件密码</param>
        /// <param name="pRequestContent">加密后的请求内容</param>
        /// <returns></returns>
        public static string ParseRequestPackets(string pCerFilePath, string pRequestContent)
        {
            //参数检查
            if (string.IsNullOrWhiteSpace(pRequestContent))
                return null;
            var content = pRequestContent.Replace("\0", string.Empty);
            if (string.IsNullOrWhiteSpace(content))
                return null;
            //
            string[] sections = content.Split(new char[]{'|'},  StringSplitOptions.RemoveEmptyEntries);
            if (sections.Length != 3)
                throw new ArgumentException("格式非法的报文.");
            string merchantID = Encoding.UTF8.GetString(EncDecUtil.Base64Decrypt(sections[0]));
            byte[] encryptedKey = EncDecUtil.Base64Decrypt(sections[1]);
            string desKey =Encoding.UTF8.GetString(EncDecUtil.RSADecrypt(pCerFilePath, encryptedKey));
            byte[] key = Encoding.ASCII.GetBytes(desKey.Substring(0, 24));
            byte[] keyIV = Encoding.ASCII.GetBytes(desKey.Substring(24, 8));
            byte[] encryptedContent = EncDecUtil.Base64Decrypt(sections[2]);
            string decryptedContent =Encoding.UTF8.GetString(EncDecUtil.TripleDESDecrypt(key, encryptedContent));
            //
            return decryptedContent;
        }


    }
}
