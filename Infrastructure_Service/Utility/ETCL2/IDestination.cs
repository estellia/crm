/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/13 11:16:17
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

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// 数据经过ETCL过程到达的终结点的接口
    /// </summary>
    public interface IDestination
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <param name="pDataItems">需要保存的数据条目</param>
        /// <returns>保存成功的记录数</returns>
        int Save(BasicUserInfo pUserInfo,IETCLDataItem[] pDataItems);
    }
}
