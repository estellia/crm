using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.MessageService.Entity
{
    class MultixMTRequeest : Base.BaseRequest
    {
        public MultixMTRequeest(SmsCommandType cmd)
            : base(cmd)
        {
            Dasm = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Dasm { get; set; }

        protected override string GetStr()
        {
            string str = "{0}={1}".Fmt("dasm",Dasm.Aggregate("",(i,j)=>
                {
                    i = i + j.Key.EncodeHexStr(this.DC) + "/" + j.Value.EncodeHexStr(this.DC) + ",";
                    return i;
                }).Trim(','));
            return str;
        }
    }
}
