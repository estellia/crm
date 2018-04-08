using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class AddMessageModel
    {
        /// <summary>
        /// 消息对象
        /// </summary>
        public OMSG OMSG { get; set; }

        /// <summary>
        /// 原始消息对象
        /// </summary>
        public MSG1 MSG1 { get; set; }

    }
}