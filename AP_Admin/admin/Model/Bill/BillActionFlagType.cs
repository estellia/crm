using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单的动作的标志的类型
    /// </summary>
    public enum BillActionFlagType
    {
        /// <summary>
        /// 创建标志
        /// </summary>
        Create = 1,
        /// <summary>
        /// 修改标志
        /// </summary>
        Modify = 2,
        /// <summary>
        /// 删除标志
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 审核标志
        /// </summary>
        Approve = 4,
        /// <summary>
        /// 退回标志
        /// </summary>
        Reject = 5,
        /// <summary>
        /// 保留标志
        /// </summary>
        Reserve = 6
    }
}
