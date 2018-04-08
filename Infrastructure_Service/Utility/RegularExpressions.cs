using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// 常用的正则表达式 
    /// </summary>
    public static class RegularExpressions
    {
        /// <summary>
        /// 用于验证邮件格式合法性的正则表达式
        /// </summary>
        public const string EMail = @"^[_A-Za-z0-9]+(\.[_A-Za-z0-9]+)*@[_A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$";

        /// <summary>
        /// 验证手机号格式的正则表达式
        /// </summary>
        public const string MobilePhone =@"^(13|14|15|18)\d{9}$";

        /// <summary>
        /// 宽泛的验证身份证号的正则表达式
        /// </summary>
        public const string IDCardLoosely = @"^[A-Za-z0-9]{15}$|^[A-Za-z0-9]{18}$";

        /// <summary>
        /// 正整数的正则表达式
        /// </summary>
        public const string UInt =@"^[0-9]+$";
    }
}
