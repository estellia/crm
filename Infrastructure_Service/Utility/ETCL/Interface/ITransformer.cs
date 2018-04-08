using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;
using System.Data;

namespace JIT.Utility.ETCL.Interface
{
    /// <summary>
    /// 转换器，用于将数据规格化成Loader所需要的数据格式(Entity)。
    /// </summary>
    public interface ITransformer
    {
        /// <summary>
        /// 执行转换
        /// </summary>
        /// <param name="pDataSource">从数据源取出的直接数据</param>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="oData">转换后的数据</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        bool Process(DataTable pDataSource, BasicUserInfo pUserInfo, out IETCLDataItem[] oData, out IETCLResultItem[] oResult);
    }
}
