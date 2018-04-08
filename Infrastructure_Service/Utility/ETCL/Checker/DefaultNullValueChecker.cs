using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 空值检查
    /// </summary>
    public class DefaultNullValueChecker : IChecker
    {
        /// <summary>
        /// 执行检查
        /// </summary>
        /// <param name="pPropertyValue">待检查的数据值</param>
        /// <param name="pPropertyText">此数据的属性名称（列描述）</param>
        /// <param name="pRowData">当前数据项的信息</param>
        /// <param name="oResult">检查结果</param>
        /// <returns>TRUE：通过检查，FALSE：未通过检查。</returns>
        public bool Process(object pPropertyValue, string pPropertyText, IETCLDataItem pRowData, ref IETCLResultItem oResult)
        {
            bool isPass = true; 
            if (pPropertyValue == null || string.IsNullOrWhiteSpace(pPropertyValue.ToString()))
            {//无效
                isPass = false;
                oResult.OPType = OperationType.DataValidaty;
                oResult.Message = string.Format("数据项:【{0}】的数据值不能为空.", pPropertyText);
            }
            return isPass;
        }
    }
}
