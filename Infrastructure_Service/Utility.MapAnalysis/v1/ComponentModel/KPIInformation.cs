using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    public class KPIInformation
    {
        public IKPIDataThreshold[] Thresholds { get; set; }
        public IKPIData[] Data { get; set; } 
    }
}