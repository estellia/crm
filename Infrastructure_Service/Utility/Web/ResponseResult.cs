using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Web
{
    /// <summary>
    /// ajax请求响应的结果
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
