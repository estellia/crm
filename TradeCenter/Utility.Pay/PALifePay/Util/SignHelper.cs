using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JIT.Utility.Pay.PALifePay.ValueObject;

namespace JIT.Utility.Pay.PALifePay.Util
{
    public static class SignHelper
    {
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <typeparam name="T">请求实体类型</typeparam>
        /// <param name="pObj">请求实体</param>
        /// <param name="pIgnoreAttr">不需要拼接的属性名,多个用逗号隔开</param>
        /// <returns></returns>
        public static void GetSecuritySign<T>(this T pObj)
        {
            Type type = pObj.GetType();
            PropertyInfo[] pro = type.GetProperties();
            PropertyInfo signProp = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (PropertyInfo p in pro)
            {
                // 获取签名的字段
                if (p.GetCustomAttributes(typeof(SignatureFieldAttribute), false).Any())
                {
                    signProp = p;
                }
                // 可以采用特性来进行需要签名或者不需要签名的字段排除
                if (p.GetCustomAttributes(typeof(IgnoreSignatureAttribute), false).Any())
                {
                    continue;
                }
                if (p.GetValue(pObj, null) == null)
                {
                    dic.Add(p.Name, string.Empty);
                }
                else
                {
                    dic.Add(p.Name, p.GetValue(pObj, null).ToString());
                }
            }
            if (dic.Count == 0)
            {
                return;
            }

            // 排序
            IOrderedEnumerable<KeyValuePair<string, string>> keys = dic.OrderBy(d => d.Key);

            string signValue = string.Empty;
            foreach (KeyValuePair<string, string> d in keys)
            {
                signValue += string.Format("{0}={1}&", d.Key, d.Value);
            }
            signValue = signValue.Substring(0, signValue.Length - 1);
            // sha256加密
            byte[] sha256SignValue = HashEncryptHelper.SHA256EncryptOutByte(signValue);
            // RSA签名
            string rsaSginValue = BouncyCastleHelper.PrivateKeyEncrypt(sha256SignValue, System.Configuration.ConfigurationManager.AppSettings["PrivateKey"]);

            // 签名
            signProp.SetValue(pObj, rsaSginValue, null);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static bool CheckSecuritySign<T>(this T pObj)
        {
            bool flag = false;
            string signStr = string.Empty;

            Type type = pObj.GetType();
            PropertyInfo[] pro = type.GetProperties();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (PropertyInfo p in pro)
            {
                // 获取签名字段的签名值
                if (p.GetCustomAttributes(typeof(SignatureFieldAttribute), false).Any())
                {
                    signStr = p.GetValue(pObj, null).ToString();
                }
                
                // 可以采用特性来进行需要签名或者不需要签名的字段排除
                if (p.GetCustomAttributes(typeof(IgnoreSignatureAttribute), false).Any())
                {
                    continue;
                }
                if (p.GetValue(pObj, null) == null)
                {
                    dic.Add(p.Name, string.Empty);
                }
                else
                {
                    dic.Add(p.Name, p.GetValue(pObj, null).ToString());
                }
            }
            if (dic.Count == 0)
            {
                return flag;
            }

            // 排序
            IOrderedEnumerable<KeyValuePair<string, string>> keys = dic.OrderBy(d => d.Key);

            string signValue = string.Empty;
            foreach (KeyValuePair<string, string> d in keys)
            {
                signValue += string.Format("{0}={1}&", d.Key, d.Value);
            }
            signValue = signValue.Substring(0, signValue.Length - 1);
            // sha256加密
            string sha256SignValue = HashEncryptHelper.SHA256Encrypt(signValue);
            // RSA签名解密出来的值
            string rsaSginValue = BouncyCastleHelper.PublicKeyDecrypt(signStr, System.Configuration.ConfigurationManager.AppSettings["PAPublicKey"]);

            // 用自己SHA256加密后跟签名里面的做对比是否一样防止串改
            return sha256SignValue.Equals(rsaSginValue);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
