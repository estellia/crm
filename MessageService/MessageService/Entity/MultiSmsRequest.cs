using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.MessageService.Base;

namespace JIT.MessageService.Entity
{
    class MultiSmsRequest:BaseRequest
    {
        public MultiSmsRequest(SmsCommandType cmd)
            : base(cmd)
        {
            Das = new List<string> { };
        }

        public List<string> Das { get; set; }
        public string Sm { get; set; }

        protected override string GetStr()
        {
            string str = "{0}={1}&{2}={3}".Fmt("das", Das.Aggregate("",(i,j)=>i+"86"+j+",").Trim(','), "sm", Sm.EncodeHexStr(this.DC));
            return str;
        }
    }
}
