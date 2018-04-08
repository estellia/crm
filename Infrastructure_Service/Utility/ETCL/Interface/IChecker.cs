using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;

namespace JIT.Utility.ETCL.Interface
{
    /// <summary>
    /// 检查器，用于检查数据的合法性。
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// 执行检查
        /// </summary>
        /// <param name="pPropertyValue">待检查的数据值</param>
        /// <param name="pPropertyText">此数据的属性名称（列描述）</param>
        /// <param name="pRowData">当前数据项的信息</param>
        /// <param name="oResult">检查结果</param>
        /// <returns>TRUE：通过检查，FALSE：未通过检查。</returns>
        bool Process(object pPropertyValue, string pPropertyText, IETCLDataItem pRowData, ref IETCLResultItem oResult);
    }
}
