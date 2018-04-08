/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:41:08
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

namespace JIT.Utility.Entity
{
    /// <summary>
    /// 空接口,表明对象为一个实体对象
    /// <remarks>
    /// <para>1.实体对象应该都有一个持久化句柄.</para>
    /// </remarks>
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 持久化句柄
        /// </summary>
        PersistenceHandle PersistenceHandle { get; set; }
    }
}
