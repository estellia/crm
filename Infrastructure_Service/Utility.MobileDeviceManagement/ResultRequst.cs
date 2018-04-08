using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MobileDeviceManagement
{
    public class CommandResponse
    {
        /// <summary>
        /// 命令ID
        /// </summary>
        public string CommandID { get; set; }
        /// <summary>
        /// 执行结果：0-99成功，大于等于100为失败
        /// </summary>
        public int ResponseCode { get; set; }
        /// <summary>
        /// 执行消息（如错误信息）
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否需要重复接收
        /// </summary>
        public bool NeedRepeat { get; set; }

    }
}
