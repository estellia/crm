using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Component
{
    /// <summary>
    /// 参数操作类
    /// </summary>
    public class ArgumentHelper
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="argumentName">参数名</param>
        /// <param name="argumentValue">参数的值</param>
        public static bool Validate(string argumentName, string argumentValue, bool throwException)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                if (throwException)
                {
                    throw new ArgumentNullException(argumentName);
                }
                return false;
            }

            if (argumentValue.Length > 32)
            {
                if (throwException)
                {
                    throw new ArgumentException(argumentName);
                }
                return false;
            }

            foreach (char ch in argumentValue)
            {
                if (!((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')))
                {
                    if (throwException)
                    {
                        throw new ArgumentException(argumentName);
                    }
                    return false;
                }
            }

            return true;
        }
    }
}
