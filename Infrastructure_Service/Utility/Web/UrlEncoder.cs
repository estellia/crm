/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/11 10:47:00
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
using System.Web;

namespace JIT.Utility.Web
{
    /// <summary>
    /// URL编码工具类 
    /// </summary>
    public static class UrlEncoder
    {
        /// <summary>
        /// 对URL进行编码并且编码的结果集要为大写字符
        /// </summary>
        /// <param name="pUrl"></param>
        /// <returns></returns>
        public static string UpperCaseUrlEncode(string pUrl)
        {
            char[] temp = HttpUtility.UrlEncode(pUrl).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            //
            return new string(temp);
        }
    }
}
