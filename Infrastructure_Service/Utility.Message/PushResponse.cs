using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.Utility.Message
{
    public class PushResponse
    {
        /// <summary>
        /// 小于100为成功,大于100为失败
        /// </summary>
        public int ResultCode { get; set; }
        public string Message { get; set; }
    }
}