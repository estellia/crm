/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 18:07:58
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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using Newtonsoft.Json;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.ValueObject;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace JIT.Utility.Pay.WeiXinPay.Util
{
    /// <summary>
    /// 工具方法 
    /// </summary>
    public static class CommonUtil
    {
        #region 获取当前时间的时间戳
        /// <summary>
        /// 获取当前时间的时间戳
        /// <remarks>
        /// <para>注意：因为微信是Java系统，因此微信支付中获取时间戳的地方要调用本方法</para>
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentTimeStamp()
        {
            return Convert.ToInt32((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }
        #endregion

        #region 获取不重复标识字符串
        /// <summary>
        /// 获取不重复标识字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateNoncestr()
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < 16; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }
        #endregion

        #region 生成订单包裹
        /// <summary>
        /// 生成订单包裹
        /// </summary>
        /// <param name="pParameters">订单参数集</param>
        /// <returns></returns>
        public static string GenerateOrderPackage(Dictionary<string, string> pParameters, WeiXinPayChannel pChannel)
        {
            //参数检查
            if (pParameters == null)
                throw new ArgumentNullException("pParameters");
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            if (string.IsNullOrWhiteSpace(pChannel.ParnterKey))
                throw new ArgumentNullException("pChannel.ParnterKey");
            //计算订单包裹签名
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            var items = pParameters.OrderBy(item => item.Key).ToArray();
            foreach (var item in items)
            {
                sb1.AppendFormat("{0}={1}&", item.Key, item.Value);
                sb2.AppendFormat("{0}={1}&", item.Key, HttpUtility.UrlEncode(item.Value));
                //sb2.AppendFormat("{0}={1}&", item.Key, UrlEncoder.UpperCaseUrlEncode(item.Value));
            }
            sb1.AppendFormat("key={0}", pChannel.ParnterKey);
            string sign = MD5Helper.Encryption(sb1.ToString());
            sign = sign.ToUpper();
            //
            sb2.AppendFormat("sign={0}", sign);
            //
            return sb2.ToString();
        }
        #endregion

        #region 生成预订单内容
        /// <summary>
        /// 生成预订单内容
        /// </summary>
        /// <param name="pParameters">参数</param>
        /// <param name="pChannel">渠道</param>
        /// <returns></returns>
        public static InnerPreOrderRequestInfo GeneratePreOrderContent(Dictionary<string, string> pParameters, WeiXinPayChannel pChannel)
        {
            //参数检查
            if (pParameters == null)
                throw new ArgumentNullException("pParameters");
            if (pChannel == null)
                throw new ArgumentNullException("pChannel");
            //
            string packageContent = string.Empty;
            string timestamp = string.Empty;
            string noncestr = string.Empty;
            string traceid = string.Empty;
            Dictionary<string, string> ps = new Dictionary<string, string>();
            foreach (var item in pParameters)
            {
                var key = item.Key.ToLower();
                switch (key)
                {
                    case "noncestr":
                        noncestr = item.Value;
                        ps.Add(key, item.Value);
                        break;
                    case "timestamp":
                        timestamp = item.Value;
                        ps.Add(key, item.Value);
                        break;
                    case "traceid":
                        traceid = item.Value;
                        ps.Add(key, item.Value);
                        break;
                    case "package":
                        packageContent = item.Value;
                        ps.Add(key, item.Value);
                        break;
                }
            }
            ps.Add("appid", pChannel.AppID);
            ps.Add("appkey", pChannel.PaySignKey);
            //计算签名
            var items = ps.OrderBy(item => item.Key).ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("&{0}={1}", item.Key, item.Value);
            }
            sb.Remove(0, 1);
            var sign = FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "SHA1");
            sign = sign.ToLower();
            //组织预订单内容
            InnerPreOrderRequestInfo request = new InnerPreOrderRequestInfo();
            request.Sign = sign;
            request.AppID = pChannel.AppID;
            request.Package = packageContent;
            request.TimeStamp = int.Parse(timestamp);
            request.NonceStr = noncestr;
            if (!string.IsNullOrWhiteSpace(traceid))
            {
                request.UserID = traceid;
            }
            //
            return request;
        }
        #endregion

        #region 将字典按ASCII排序后转化为"key1=value1&key2=value2"和格式
        /// <summary>
        /// 将字典按ASCII排序后转化为"key1=value1&key2=value2"和格式
        /// </summary>
        /// <param name="pParameters"></param>
        /// <param name="pUrlEncode"></param>
        /// <returns></returns>
        public static string GetParametersStr(IEnumerable<KeyValuePair<string, object>> pParameters, bool pUrlEncode = false)
        {
            var temp = pParameters.OrderBy(t => t.Key).Select(t => t);
            return temp.Aggregate(new StringBuilder(), (i, j) =>
                {
                    i.AppendFormat("{0}={1}&", j.Key.ToLower(), pUrlEncode ? HttpUtility.UrlEncode(j.Value.ToString()) : j.Value);
                    return i;
                }).ToString().Trim('&');
        }
        #endregion

        #region 将字典内容生成XML字符串
        /// <summary>
        /// 将字典内容生成XML字符串
        /// </summary>
        /// <param name="pParameters"></param>
        /// <returns></returns>
        public static string ArrayToXml(IEnumerable<KeyValuePair<string, object>> pParameters)
        {
            Func<string, bool> IsNumeric = (t) =>
                {
                    int i;
                    return int.TryParse(t, out i);
                };

            String xml = "<xml>";

            foreach (KeyValuePair<string, object> pair in pParameters)
            {
                String key = pair.Key;
                String val = pair.Value.ToString();
                if (IsNumeric(val))
                {
                    xml += "<" + key + ">" + val + "</" + key + ">";

                }
                else
                    xml += "<" + key + "><![CDATA[" + val + "]]></" + key + ">";
            }

            xml += "</xml>";
            return xml;
        }
        #endregion

        #region 生成签名
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="pParameters">生成签名的参数</param>
        /// <param name="pChannel">通道</param>
        /// <returns></returns>
        public static string CreateSign(IEnumerable<KeyValuePair<string, object>> pParameters, WeiXinPayChannel pChannel)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var item in pParameters)
            {
                dic[item.Key] = item.Value;
            }
            dic["appkey"] = pChannel.PaySignKey;
            if (!dic.Keys.ToList().Exists(t => t.ToLower() == "appid"))
            {
                dic["appid"] = pChannel.AppID;
            }
            var temp = dic.OrderBy(t => t.Key);
            var nosign = GetParametersStr(temp, false);
            var sign = CommonUtil.Sha1(nosign);
            return sign.ToLower();
        }
        #endregion

        #region 计算sha
        /// <summary>
        /// 计算sha
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String Sha1(String s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
					'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
                SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                byte[] md = sha.ComputeHash(btInput);
                // 把密文转换成十六进制的字符串形式
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }
        #endregion

        #region 获取请求ip地址
        /// <summary>
        /// 获取请求ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            try
            {
                IPAddress ipAddr =
                    Dns.GetHostByName(Dns.GetHostName()).AddressList[0];//获得当前IP地址
                string ip = ipAddr.ToString();
                return ip;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region DES加密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string EncryptDES(string strContent, string strKey = "Dw&7#~d2")
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(strContent);
            provider.Key = Encoding.GetEncoding("UTF-8").GetBytes(strKey);
            provider.IV = Encoding.GetEncoding("UTF-8").GetBytes(strKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            var result = Convert.ToBase64String(stream.ToArray());
            return result;
        }
        #endregion

        #region DES解码
        /// <summary>
        /// DES解码
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string DecryptDES(string strContent, string strKey = "Dw&7#~d2")
        {
            byte[] bytes = Convert.FromBase64String(strContent);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.GetEncoding("UTF-8").GetBytes(strKey);
            provider.IV = Encoding.GetEncoding("UTF-8").GetBytes(strKey);
            byte[] resultBytes = provider.CreateDecryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            var result = Encoding.UTF8.GetString(resultBytes.ToArray());
            return result;
        }
        #endregion
    }
}
