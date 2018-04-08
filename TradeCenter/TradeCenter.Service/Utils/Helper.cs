using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec.Util;

namespace JIT.TradeCenter.Service.Utils
{
    public static class Helper
    {
        /// <summary>
        /// 获取16位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetDataRandom()
        {
            string strData = string.Empty;
            strData += DateTime.Now.Year;
            strData += DateTime.Now.Month;
            strData += DateTime.Now.Day;
            strData += DateTime.Now.Hour;
            strData += DateTime.Now.Minute;
            strData += DateTime.Now.Second;
            Random r = new Random();
            strData = strData + r.Next(100);
            return strData;
        }

    }
}