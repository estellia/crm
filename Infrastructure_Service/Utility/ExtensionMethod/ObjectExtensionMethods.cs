/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/11 10:15:02
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
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Object的扩展方法
    /// </summary>
    public static class ObjectExtensionMethods
    {
        #region 扩展方法：将对象序列化为JSON
        /// <summary>
        /// 扩展方法：将对象序列化为JSON
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns>对象序列化为JSON的字符串</returns>
        public static string ToJSON(this object pCaller)
        {
            if (pCaller == null) { return "null"; }

            if (pCaller.GetType().Name == "DataTable" || pCaller.GetType().Name == "DataSet")
            {
                //DataTable序列化
                return JsonConvert.SerializeObject(pCaller, Formatting.Indented);
            }
            else
            {
                //其它直接序列化
                return JsonConvert.SerializeObject(pCaller);
            }
        }

        #endregion

        /// <summary>
        /// 对Object对象进行类型转换
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pTargetType">要转换成的类型</param>
        /// <returns></returns>
        public static object ChangeTypeTo(this object pCaller, Type pTargetType)
        {
            if (pCaller == null)
                return null;
            if (pCaller.GetType() == typeof(string))
                return pCaller.ToString().ChangeTypeTo(pTargetType);
            if (pCaller.GetType() == typeof(DBNull))
                return null;
            if (pTargetType.IsNullableCRTValueType())
            {
                return Convert.ChangeType(pCaller, pTargetType.GetNullableUnderlyingType());
            }
            else
            {
                return Convert.ChangeType(pCaller, pTargetType);
            }
        }

        /// <summary>
        /// 获取可序列化对像的二进制数据
        /// </summary>
        /// <param name="pValue">可序列化对象</param>
        /// <returns></returns>
        public static byte[] GetBytes(this object pValue)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, pValue);
                return ms.ToArray();
            }
        }
    }
}
