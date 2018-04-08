using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 邮件格式检查器
    /// </summary>
    public class EMailChecker : RegexChecker
    {
        /// <summary>
        /// 构造器（指定名称及检查语法）
        /// </summary>
        public EMailChecker()
           : base("邮件")
        {
            this._reg = new System.Text.RegularExpressions.Regex(RegularExrepssions.EMail);
        } 
    }
}
