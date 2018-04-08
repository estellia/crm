using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MobileDeviceManagement
{
    public class CommonResponse
    {
        /// <summary>
        /// 返回码:0-99为成功  其它为失败
        /// </summary>
        public int ResultCode { get; set; }
        public string Message { get; set; }
    }
}
