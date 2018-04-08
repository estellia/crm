using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 正则检查器基类
    /// </summary>
    public abstract class RegexChecker : IChecker
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        protected Regex _reg = null;
        /// <summary>
        /// 检查器名称
        /// </summary>
        private string _regexName;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pRegexName">检查器名称</param>
        public RegexChecker(string pRegexName)
        {
            _regexName = pRegexName;
        }

        /// <summary>
        /// 根据指定的正则表达式执行检查
        /// </summary>
        /// <param name="pPropertyValue">待检查的数据值</param>
        /// <param name="pPropertyText">此数据的属性名称（列描述）</param>
        /// <param name="pRowData">当前数据项的信息</param>
        /// <param name="oResult">检查结果</param>
        /// <returns>TRUE：通过检查，FALSE：未通过检查。</returns>
        public bool Process(object pPropertyValue, string pPropertyText, IETCLDataItem pRowData, ref IETCLResultItem oResult)
        {
            bool isPass = true;
            if (pPropertyValue != null && !string.IsNullOrWhiteSpace(pPropertyValue.ToString()))
            {
                if (!this._reg.IsMatch(pPropertyValue.ToString()))
                {
                    isPass = false;
                    oResult.OPType = OperationType.DataValidaty;
                    oResult.Message = string.Format("数据项:【{0}】 的数据不是一个合法的【{1}】格式.", pPropertyText, _regexName);
                }
            }
            return isPass;
        } 
    }
}
