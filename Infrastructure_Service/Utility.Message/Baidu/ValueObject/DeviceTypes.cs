/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 18:18:49
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
    /// 设备类型 
    /// </summary>
    public enum DeviceTypes:uint
    {
        /// <summary>
        /// 浏览器
        /// </summary>
        Browser =1
        ,
        /// <summary>
        /// PC设备
        /// </summary>
        PC
        ,
        /// <summary>
        /// Android设备
        /// </summary>
        Android
        ,
        /// <summary>
        /// IOS设备
        /// </summary>
        IOS
        ,
        /// <summary>
        /// Windows Phone设备
        /// </summary>
        WindowsPhone
    }
}
