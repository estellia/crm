/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:48:44
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

using JIT.Utility.Entity;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 分页查询结果 
    /// </summary>
    [Serializable]
    public class PagedQueryObjectResult<T> //where T:Object
    {
        /// <summary>
        /// 结果数组
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount { get; set; }
    }
}
