using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.AppVersion.Entity.BusinessEntity
{
    public class BusinessZoneInfo
    {
        public int BusinessZoneID { get; set; }
        public string BusinessZoneCode { get; set; }
        public string BusinessZoneName { get; set; }
        public string ServiceURL { get; set; }
        public AppVersionInfo VersionInfo { get; set; }
    }
}
