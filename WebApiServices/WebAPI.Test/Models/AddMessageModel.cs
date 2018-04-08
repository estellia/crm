using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Test.Models
{
    public class AddMessageModel
    {
        /// <summary>
        /// 消息对象
        /// </summary>
        public OMSG OmsgModel { get; set; }

        /// <summary>
        /// 原始消息对象
        /// </summary>
        public MSG1 Omsg1Model { get; set; }

    }
}