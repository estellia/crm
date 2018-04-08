using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 手机号检查器(Gof23 of Decorator) 
    /// </summary>
    public class MobilePhoneChecker : RegexChecker
    {
        /// <summary>
        /// 构造器（指定名称及检查语法）
        /// </summary>
        public MobilePhoneChecker()
            : base("手机号")
        {
            this._reg = new System.Text.RegularExpressions.Regex(RegularExrepssions.MobilePhone);
        }
    }
}
