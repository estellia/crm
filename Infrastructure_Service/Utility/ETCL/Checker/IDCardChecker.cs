using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 身份证号检查器(Gof23 of Decorator) 
    /// </summary>
    public class IDCardChecker : RegexChecker
    {
        /// <summary>
        /// 构造器（指定名称及检查语法）
        /// </summary>
        public IDCardChecker()
            : base("身份证号")
        {
            this._reg = new System.Text.RegularExpressions.Regex(RegularExrepssions.IDCard);
        }
    }
}
