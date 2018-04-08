/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 18:29:39
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
    /// 消息类型 
    /// </summary>
    public enum MessageTypes
    {
        /// <summary>
        /// 消息（透传给应用的消息体）
        /// </summary>
        Message=0
        ,
        /// <summary>
        /// 通知（对应设备上的消息通知）
        /// </summary>
        Notification=1
    }
}
