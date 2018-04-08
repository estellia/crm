/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 15:25:56
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.ETCL2.ValueObject
{
    /// <summary>
    /// ETCL执行过程结果 
    /// </summary>
    public class ETCLProcessResult
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ETCLProcessResult()
        {
            this.IsSuccess = true;
        }
        #endregion

        /// <summary>
        /// 是否处理成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 从数据源中获取的记录数
        /// </summary>
        public int SourceItemCount { get; set; }

        /// <summary>
        /// 转换后的记录数
        /// </summary>
        public int TransferedItemCount { get; set; }

        /// <summary>
        /// 写入目的地的记录数
        /// </summary>
        public int DestinationItemCount { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public CheckResult[] CheckResults { get; set; }
    }
}
