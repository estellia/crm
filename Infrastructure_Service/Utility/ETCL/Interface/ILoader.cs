using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;
using System.Data;

namespace JIT.Utility.ETCL.Interface
{
    /// <summary>
    /// 加载器，用于将规格化之后的数据插入到目的数据库。
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// 执行加载
        /// </summary>
        /// <param name="pDataRow">ETCL数据项</param>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        bool Process(IETCLDataItem[] pDataRow, BasicUserInfo pUserInfo, out IETCLResultItem[] oResult,IDbTransaction pTran);
    }
}
