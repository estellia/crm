using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.MessageService.Base
{
    public abstract class BaseResponse
    {
        public SmsCommandType Command { get; set; }
        public string Spid { get; set; }
        public string Mtstat { get; set; }
        public string Mterrcode { get; set; }
    }
}
