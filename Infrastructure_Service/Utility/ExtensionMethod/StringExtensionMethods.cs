using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Linq;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// String的扩展方法
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// 扩展方法：将16进制格式的字符串转换成颜色对象
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static Color? HexToColor(this string pCaller)
        {
            if (!string.IsNullOrEmpty(pCaller))
            {
                pCaller = pCaller.Replace("#", string.Empty);
                pCaller = pCaller.Replace("0x", string.Empty);

                int position = 0;
                byte alpha = 255;

                //如果字符串的长度超过8位,则默认前两位为透明度的值
                if (pCaller.Length > 8)
                {
                    alpha = Convert.ToByte(pCaller.Substring(position, 2), 16);
                    position += 2;
                }
                //获取红色值
                byte red = Convert.ToByte(pCaller.Substring(position, 2), 16);
                position += 2;

                //获取绿色值
                byte green = Convert.ToByte(pCaller.Substring(position, 2), 16);
                position += 2;

                //获取蓝色值
                byte blue = Convert.ToByte(pCaller.Substring(position, 2), 16);

                //返回颜色
                return Color.FromArgb(alpha, red, green, blue);
            }
            return null;
        }

        /// <summary>
        /// 扩展方法：使用替换字符逐个字符地替换输入字符串
        /// </summary>
        /// <param name="pCaller">调用者(输入字符串)</param>
        /// <param name="pReplacement">替换字符</param>
        /// <returns></returns>
        public static string ReplaceOneByOne(this string pCaller, char pReplacement)
        {
            if (pCaller == null)
                throw new ArgumentNullException();
            StringBuilder result = new StringBuilder();
            foreach (var item in pCaller)
            {
                result.Append(pReplacement);
            }

            return result.ToString();
        }

        /// <summary>
        /// 扩展方法：将字符串重复拼接多次
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pRepeatCount">重复拼接的次数</param>
        /// <returns></returns>
        public static string ConcatRepeatly(this string pCaller, int pRepeatCount)
        {
            //特殊处理
            if (pCaller == null)
                throw new ArgumentNullException();
            if (pRepeatCount < 0)
                throw new ArgumentOutOfRangeException();
            if (pCaller == string.Empty)
                return pCaller;
            if (pRepeatCount == 0)
                return string.Empty;
            if (pRepeatCount == 1)
                return pCaller;
            //初始化
            int remainder = 0;
            int divisor = pRepeatCount;
            StringBuilder strDivisor = new StringBuilder(pCaller);
            StringBuilder strRemainer = new StringBuilder();
            //将加法转换成乘法
            do
            {
                divisor = Math.DivRem(divisor, 2, out remainder);
                var temp = strDivisor.ToString();
                if (remainder == 1)
                    strRemainer.Append(temp);
                strDivisor.Append(temp);
            } while (divisor >= 2);
            //返回结果
            return strDivisor.Append(strRemainer.ToString()).ToString();
        }

        /// <summary>
        /// 扩展方法：根据JSON字符串反序列化成对象
        /// </summary>
        /// <param name="pObj">序列化字符串</param>
        /// <returns>解析后的对象</returns>
        public static T DeserializeJSONTo<T>(this string pObj)
        {
            if (string.IsNullOrEmpty(pObj))
            {
                return default(T);
            }
            return (T)JsonConvert.DeserializeObject(pObj, typeof(T));
        }

        /// <summary>
        /// 扩展方法：将字符串转换成指定类型的对象
        /// <remarks>
        /// <para>支持的类型有：</para>
        /// <para>1.string</para>
        /// <para>2.值类型(公共语言运行时类型+GUID+Enum)</para>
        /// <para>3.可空值类型</para>
        /// </remarks>
        /// </summary>
        /// <param name="pStringValue">调用者</param>
        /// <param name="pTargetType">转换后的类型</param>
        /// <returns>转换后的对象</returns>
        public static object ChangeTypeTo(this string pCaller, Type pTargetType)
        {
            //
            if (pCaller == null)
                return null;
            if (pTargetType == null)
                throw new ArgumentNullException("pTargetType");
            //
            if (pTargetType == typeof(string))
            {
                return pCaller;
            }
            else if (pTargetType.IsCRT())   //除了string以外的公共语言运行时类型
            {
                return Convert.ChangeType(pCaller, pTargetType);
            }
            else if (pTargetType == typeof(Guid))
            {
                return new Guid(pCaller);
            }
            else if (pTargetType.IsEnum)    //枚举类型
            {
                return Enum.Parse(pTargetType, pCaller);
            }
            else if (pTargetType.IsNullableCRTValueType())//基础类型为公共语言运行时类型的可空值类型
            {
                if (string.IsNullOrEmpty(pCaller))
                    return null;
                else
                {
                    return Convert.ChangeType(pCaller, pTargetType.GetNullableUnderlyingType());
                }
            }
            else if (pTargetType == typeof(Guid?)) //可空GUID
            {
                if (string.IsNullOrEmpty(pCaller))
                    return null;
                else
                    return new Guid(pCaller);
            }
            else if (pTargetType.IsNullableValueType() && pTargetType.GetNullableUnderlyingType().IsEnum)  //可空枚举
            {
                if (string.IsNullOrEmpty(pCaller))
                    return null;
                else
                    return Enum.Parse(pTargetType.GetNullableUnderlyingType(), pCaller);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// 将指定字符串进行MD5加密
        /// </summary>
        /// <param name="pOriginalString">原始字符串</param>
        /// <returns>MD5值</returns>
        public static string ToMD5String(this string pOriginalString)
        {
            //将输入转换为ASCII 字符编码
            ASCIIEncoding enc = new ASCIIEncoding();
            //将字符串转换为字节数组
            byte[] buffer = enc.GetBytes(pOriginalString);
            //创建MD5实例
            MD5 md5Provider = new MD5CryptoServiceProvider();
            //进行MD5加密
            byte[] hash = md5Provider.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            //拼装加密后的字符
            for (int i = 0; i < hash.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hash[i]);
            }
            //输出加密后的字符串
            return sb.ToString().ToUpper();
        }

        public static Guid ToGuidBy24Chars(this string pCaller)
        {
            var chars = pCaller.ToCharArray();
            #region 验证
            if (chars.Length != 24)
                throw new Exception("长度不是24位字符，不能转换成GUID");
            if (!chars.All(t => t < 123))
                throw new Exception("内容不是英文字母或者数字，不能转换成GUID");
            #endregion
            var temp = Convert.FromBase64String(pCaller);
            return new Guid(temp.ToArray());
        }
    }
}
