using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Char的扩展方法
    /// </summary>
    public static class CharExtensionMethods
    {
        /// <summary>
        /// 扩展方法：将字符串重复拼接多次
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pRepeatCount">重复拼接的次数</param>
        /// <returns></returns>
        public static string ConcatRepeatly(this char pCaller, int pRepeatCount)
        {
            return new string(pCaller, pRepeatCount);
        }
    }
}
