using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Log;
using JIT.MessageService.Base;
using JIT.MessageService.Entity;
using JIT.MessageService;

namespace JIT.MessageService
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

        public static string Fmt(this string value,params object[] objs)
        {
            return string.Format(value, objs);
        }

        public static BaseRequest GetRequest(this SMSSendEntity entity)
        {
            SmsRequest request = new SmsRequest(SmsCommandType.MT_REQUEST);
            request.Sm = entity.SMSContent;
            request.Da = entity.MobileNO.ToString();
            return request;
        }

        /// <summary>
        /// 相同内容,不同号码
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static BaseRequest GetMultiRequest(this List<SMSSendEntity> entities)
        {
            MultiSmsRequest request = new MultiSmsRequest(SmsCommandType.MULTI_MT_REQUEST);
            request.Das = entities.Aggregate(new List<string> { }, (i, j) => { i.Add(j.MobileNO); return i; });
            return request;
        }

        /// <summary>
        /// 不同内容,不同号码
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static BaseRequest GetMultiMTRequest(this List<SMSSendEntity> entities)
        {
            MultixMTRequeest request = new MultixMTRequeest(SmsCommandType.MULTIX_MT_REQUEST);
            foreach (var item in entities)
            {
                request.Dasm.Add(item.MobileNO, item.SMSContent);
            }
            return request;
        }

    }
}
