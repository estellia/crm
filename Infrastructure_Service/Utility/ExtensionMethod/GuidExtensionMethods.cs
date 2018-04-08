/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 12:42:06
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
using System.Linq;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Guid的扩展方法 
    /// </summary>
    public static class GuidExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取GUID的文本值,不带连字符
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string ToText(this Guid pCaller)
        {
            return pCaller.ToString("N");
        }

        /// <summary>
        /// 将GUID压缩成16位的字符串
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string CompressTo24Chars(this Guid pCaller)
        {
            byte[] temp = pCaller.ToByteArray();
            var str = Convert.ToBase64String(temp);
            return str;
        }
    }
}
