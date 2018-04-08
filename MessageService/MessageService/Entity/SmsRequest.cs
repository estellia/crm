using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.MessageService.Base;

namespace JIT.MessageService.Entity
{
    class SmsRequest:BaseRequest
    {
        public SmsRequest(SmsCommandType cmd):base(cmd)
        { 
            
        }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string Da { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Sm { get; set; }

        protected override string GetStr()
        {
            string str = "{0}={1}&{2}={3}".Fmt("da", "86"+Da, "sm", Sm.EncodeHexStr(this.DC));
            return str;
        }
    }
}
