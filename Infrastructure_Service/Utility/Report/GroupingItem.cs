/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 16:16:35
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

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分组项 
    /// </summary>
    /// <typeparam name="T">T为分组中元素的类型</typeparam>
    public class GroupingItem<T>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GroupingItem()
        {
        }
        #endregion

        /// <summary>
        /// 分组的键
        /// </summary>
        public List<object> Keys { get; set; }

        /// <summary>
        /// 分组项明细
        /// </summary>
        public List<T> Details { get; set; }
    }
}
