/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 16:43:19
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

using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2.ValueObject
{
    /// <summary>
    /// 对ETCL数据进行检查的检查结果  
    /// </summary>
    public class CheckResult
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CheckResult()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 检查类型
        /// </summary>
        public CheckTypes CheckType { get; set; }

        /// <summary>
        /// 严重等级
        /// </summary>
        public ServerityLevels ServerityLevel { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 被检查的数据条目
        /// </summary>
        public IETCLDataItem CheckedDataItem { get; set; }
        #endregion
    }
}
