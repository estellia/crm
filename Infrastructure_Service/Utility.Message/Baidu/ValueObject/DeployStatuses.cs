/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 10:00:47
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

namespace JIT.Utility.Message.Baidu.ValueObject
{
    /// <summary>
    /// 部署状态。指定应用当前的部署状态 
    /// </summary>
    public enum DeployStatuses:uint
    {
        /// <summary>
        /// 开发状态
        /// </summary>
        Development=1
        ,
        /// <summary>
        /// 生产状态
        /// </summary>
        Production=2
    }
}
