using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.AppVersion.Entity.BusinessEntity
{
    public class AppVersionInfo
    {
        public int AppID { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string PackageUrl { get; set; }
    }
}
