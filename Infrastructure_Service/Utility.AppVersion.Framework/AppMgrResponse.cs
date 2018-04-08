using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.AppVersion.Framework
{
    public class AppMgrResponse
    {
        /// <summary>
        /// 返回码0-99:成功,其它失败
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message{get;set;}
        /// <summary>
        /// 业务数据
        /// </summary>
        public object Data { get; set; }
    }
}
