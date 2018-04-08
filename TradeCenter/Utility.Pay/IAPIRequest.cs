/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/20 13:05:32
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

namespace JIT.Utility.Pay
{
    /// <summary>
    /// API请求的参数接口 
    /// </summary>
    public interface IAPIRequest
    {
        /// <summary>
        /// 获取请求参数的内容
        /// </summary>
        /// <returns></returns>
        string GetContent();
    }
}
