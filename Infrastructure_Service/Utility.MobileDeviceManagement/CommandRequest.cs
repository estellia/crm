using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MobileDeviceManagement
{
    public class CommandRequest
    {
        public string ClientID { get; set; }
        public string UserID { get; set; }
        public string AppCode { get; set; }
        public string AppVersion { get; set; }
    }
}
