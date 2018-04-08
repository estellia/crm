/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 15:19:25
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
    /// ETCL过程中从源数据抽取出来的数据条目接口 
    /// </summary>
    public interface IETCLDataItem
    {
        /// <summary>
        /// 数据条目是数据集中的索引
        /// <remarks>
        /// <para>索引从0开始</para>
        /// </remarks>
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// 数据条目数据
        /// </summary>
        object OriginalRow { get; set; }
    }
}
