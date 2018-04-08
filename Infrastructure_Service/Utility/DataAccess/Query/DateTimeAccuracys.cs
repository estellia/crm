/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:34:12
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

namespace JIT.Utility.DataAccess.Query
{
    /// <summary>
    /// 时间精度 
    /// </summary>
    public enum DateTimeAccuracys
    {
        /// <summary>
        /// 日期,丢弃了时、分、秒、毫秒
        /// <remarks>
        /// <para>例如:</para>
        /// <para>2011-11-2</para>
        /// </remarks>
        /// </summary>
        Date
        ,
        /// <summary>
        /// 时间,完整的时间格式
        /// </summary>
        DateTime
    }
}
