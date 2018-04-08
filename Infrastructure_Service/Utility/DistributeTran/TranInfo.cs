using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.DistributeTran
{
    public class TranInfo
    {
        public DateTime CreateTime { get; set; }
        public List<TerminalInfo> Terminals { get; set; }
    }
}
