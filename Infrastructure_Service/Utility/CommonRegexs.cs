using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JIT.Utility
{
    /// <summary>
    /// 常用的正则表达式的对象
    /// </summary>
    public static class CommonRegexs
    {
        /// <summary>
        /// 用于验证邮件格式合法性的正则表达式
        /// </summary>
        public readonly static Regex EMail = new Regex(RegularExpressions.EMail);

        /// <summary>
        /// 验证手机号格式的正则表达式
        /// </summary>
        public readonly static Regex MobilePhone = new Regex(RegularExpressions.MobilePhone);

        /// <summary>
        /// 宽泛的验证身份证号的正则表达式
        /// </summary>
        public readonly static Regex IDCardLoosely = new Regex(RegularExpressions.IDCardLoosely);

        /// <summary>
        /// 正整数的正则表达式
        /// </summary>
        public readonly static Regex UInt = new Regex(RegularExpressions.UInt);
    }
}
