using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Interface
{
    /// <summary>
    /// ECTL处理结果详情
    /// </summary>
    public interface IETCLResultItem
    {
        /// <summary>
        /// 检查类型
        /// </summary>
        OperationType OPType { get; set; }

        /// <summary>
        /// 严重等级
        /// </summary>
        ServerityLevel ServerityLevel { get; set; }

        /// <summary>
        /// 结果代码(0-99为成功，100以上表示失败,500以上表示异常)
        /// </summary>
        int ResultCode { get; set; }

        /// <summary>
        /// 行序号
        /// </summary>
        int RowIndex { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        string Message { get; set; }
    }
}
