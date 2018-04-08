using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Message.IOS.ValueObject
{
    public class IOSChannel
    {
        /// <summary>
        /// P12文件名
        /// </summary>
        public string P12 { get; set; }
        /// <summary>
        /// P12文件密码
        /// </summary>
        public string P12PWD { get; set; }
        /// <summary>
        /// 是否是沙盒测试
        /// </summary>
        public bool? SandBox { get; set; }
    }
}
