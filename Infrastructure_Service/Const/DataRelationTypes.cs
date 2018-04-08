/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/24 14:26:22
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

namespace JIT.Const
{
    /// <summary>
    /// 数据关联类型 
    /// </summary>
    public enum DataRelationTypes
    {
        /// <summary>
        /// 一对一关系
        /// </summary>
        OneToOne=1
        ,
        /// <summary>
        /// 一对多关系
        /// </summary>
        OneToMany
    }
}
