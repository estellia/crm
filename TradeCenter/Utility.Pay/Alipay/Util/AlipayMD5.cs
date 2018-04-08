﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace JIT.Utility.Pay.Alipay.Util
{
    /// <summary>
    /// 类名：MD5
    /// 功能：MD5加密
    /// 版本：3.3
    /// 修改日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// </summary>
    public sealed class AlipayMD5
    {
        public AlipayMD5()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="pKey">密钥</param>
        /// <param name="pInputCharset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string pPrestr, string pKey, string pInputCharset)
        {
            StringBuilder sb = new StringBuilder(32);

            pPrestr = pPrestr + pKey;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(pInputCharset).GetBytes(pPrestr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="pPrestr">需要签名的字符串</param>
        /// <param name="pSign">签名结果</param>
        /// <param name="pKey">密钥</param>
        /// <param name="pInputCharset">编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string pPrestr, string pSign, string pKey, string pInputCharset)
        {
            string mysign = Sign(pPrestr, pKey, pInputCharset);
            if (mysign == pSign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
