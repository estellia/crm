using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.ETCL.Checker;

namespace JIT.Utility.ETCL.Interface
{
    /// <summary>
    /// 抽取器，用于从数据源取出原始数据。
    /// </summary>
    public interface IExtractor
    {
        /// <summary>
        /// 执行抽取
        /// </summary>
        /// <param name="pDataSource">数据源（XLS的文件路径）</param>
        /// <param name="oData">抽取出的直接数据（默认为DataTable）</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        bool Process(object pDataSource,out object oData,out IETCLResultItem[] oResult);
    }
}
