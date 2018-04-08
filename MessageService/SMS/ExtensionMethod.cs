using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Log;
using System.Net;
using System.Web;

namespace JIT.Utility.SMS
{
    public static class ExtensionMethod
    {
        public static string EncodeHexStr(this string value, int dataCoding)
        {
            string strhex = "";
            try
            {
                Byte[] bytSource = null;
                if (dataCoding == 15)
                {
                    bytSource = Encoding.GetEncoding("GBK").GetBytes(value);
                }
                else if (dataCoding == 8)
                {
                    bytSource = Encoding.BigEndianUnicode.GetBytes(value);
                }
                else
                {
                    bytSource = Encoding.ASCII.GetBytes(value);
                }
                for (int i = 0; i < bytSource.Length; i++)
                {
                    strhex = strhex + bytSource[i].ToString("X2");

                }
            }
            catch (System.Exception err)
            {
                Loggers.Exception(new ExceptionLogInfo(err));
            }
            return strhex;
        }

        public static string DecodeHexStr(this string value, int dataCoding)
        {
            String strReturn = "";
            try
            {
                int len = value.Length / 2;
                byte[] bytSrc = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    string s = value.Substring(i * 2, 2);
                    bytSrc[i] = Byte.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);
                }

                if (dataCoding == 15)
                {
                    strReturn = Encoding.GetEncoding("GBK").GetString(bytSrc);
                }
                else if (dataCoding == 8)
                {
                    strReturn = Encoding.BigEndianUnicode.GetString(bytSrc);
                }
                else
                {
                    strReturn = System.Text.ASCIIEncoding.ASCII.GetString(bytSrc);
                }
            }
            catch (System.Exception err)
            {
                Loggers.Exception(new ExceptionLogInfo(err));
            }
            return strReturn;
        }

        public static string Fmt(this string value, params object[] objs)
        {
            return string.Format(value, objs);
        }

        public static byte[] GetData(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value,Encoding.GetEncoding("GBK"));
        }

        public static string UrlDecode(this string value)
        {
            return HttpUtility.UrlDecode(value);
        }

    }
}
