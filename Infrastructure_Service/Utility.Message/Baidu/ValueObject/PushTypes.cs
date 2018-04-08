/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 17:42:00
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
    /// 推送类型 
    /// </summary>
    public enum PushTypes:uint
    {
        /// <summary>
        /// 单播
        /// </summary>
        Unicast=1
        ,
        /// <summary>
        /// 标签式多播
        /// </summary>
        MulticastTag=2
        ,
        /// <summary>
        /// 广播式
        /// </summary>
        Broadcast=3
    }
}
