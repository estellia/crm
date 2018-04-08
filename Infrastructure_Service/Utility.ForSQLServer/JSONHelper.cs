/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/5 10:07:53
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

namespace JIT.Utility.ForSQLServer
{
    /// <summary>
    /// JSON的序列化和反序列化 
    /// </summary>
    public static class JSONHelper
    {
        /// <summary>
        /// 根据JSON字符串反序列化成对象
        /// </summary>
        /// <param name="pObj">序列化字符串</param>
        /// <returns>解析后的对象</returns>
        public static T DeserializeJSONTo<T>(string pObj)
        {
            if (string.IsNullOrEmpty(pObj))
            {
                return default(T);
            }
            return (T)JsonConvert.DeserializeObject(pObj, typeof(T));
        }

        /// <summary>
        /// 扩展方法：将对象序列化为JSON
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns>对象序列化为JSON的字符串</returns>
        public static string ToJSON(object pCaller)
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
    }
}
