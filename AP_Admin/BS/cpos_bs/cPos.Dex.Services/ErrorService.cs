using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Common;

namespace cPos.Dex.Services
{
    public class ErrorService
    {
        #region Throw
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="writeLog"></param>
        public static void Throw(string key, string message, bool writeLog)
        {
            // Log
            //LogService.WriteError(Utils.NewGuid(), key, message);
            throw new Exception(message);
        }

        public static void Throw(string key, string message)
        {
            Throw(key, message, true);
        }

        public static void Throw(string message)
        {
            Throw(string.Empty, message, true);
        }
        #endregion

        #region OutputError
        /// <summary>
        /// 输出错误
        /// </summary>
        public static Hashtable OutputError(ErrorCode errorCode, string errorDesc, bool enableString, bool writeLog)
        {
            Hashtable ht = new Hashtable();
            object status = false;
            string errorCodeStr = errorCode.ToString();
            if (enableString)
            {
                status = Utils.GetStatus(false);
            }
            ht.Add("status", status);
            ht.Add("error_code", errorCodeStr);
            ht.Add("error_desc", errorDesc);
            return ht;
        }

        public static Hashtable OutputError(ErrorCode errorCode, string errorDesc, bool enableString)
        {
            return OutputError(errorCode, errorDesc, enableString, true);
        }
        #endregion

        #region CheckLength
        /// <summary>
        /// 检查长度范围
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="enableString"></param>
        /// <param name="allowNull"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Hashtable CheckLength(string key, object value, int minLength, int maxLength,
            bool enableString, bool allowNull, ref bool status)
        {
            Hashtable ht = null;
            if (value == null && allowNull && minLength == 0)
            {
                status = true;
                ht = new Hashtable();
                ht.Add("status", Utils.GetStatus(status));
                return ht;
            }
            if (value == null || value.ToString().Length < minLength || value.ToString().Length > maxLength)
            {
                status = false;
                //ht = OutputError(ErrorCode.A004,
                //    string.Format("'{0}'超出长度限制({1}~{2})", key, minLength, maxLength), enableString);
                ht = OutputError(ErrorCode.A004,
                    string.Format("'{0}'长度不合法", key), enableString);
                return ht;
            }
            else
            {
                status = true;
                ht = new Hashtable();
                ht.Add("status", Utils.GetStatus(status));
            }
            return ht;
        }
        #endregion

        #region CheckNumArrange
        /// <summary>
        /// 检查数值范围
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="enableString"></param>
        /// <param name="allowNull"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Hashtable CheckNumArrange(string key, object value, double min, double max,
            bool enableString, bool allowNull, ref bool status)
        {
            Hashtable ht = null;
            if (value == null && allowNull)
            {
                status = true;
                ht = new Hashtable();
                ht.Add("status", Utils.GetStatus(status));
                return ht;
            }
            if (value == null || Convert.ToDouble(value) < min || Convert.ToDouble(value) > max)
            {
                status = false;
                ht = OutputError(ErrorCode.A017,
                    string.Format("'{0}'超出数值范围", key), enableString);
                return ht;
            }
            else
            {
                status = true;
                ht = new Hashtable();
                ht.Add("status", Utils.GetStatus(status));
            }
            return ht;
        }
        #endregion

    }
}
