using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Boolean的扩展方法
    /// </summary>
    public static class BooleanExtensionMethods
    {
        /// <summary>
        /// 扩展方法:将布尔类型的对象转换成Int32
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static int ToInt32(this bool pCaller)
        {
            return pCaller ? 1 : 0;
        }
    }
}
