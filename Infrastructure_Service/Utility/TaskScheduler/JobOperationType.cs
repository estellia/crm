using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.TaskScheduler
{
    public enum JobOperationType
    {
        /// <summary>
        /// 增加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 2,
        /// <summary>
        /// 继续
        /// </summary>
        Resume = 3,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel=4
    }
}
