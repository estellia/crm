using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MobileDeviceManagement
{
    public class MailUser
    {
        public string Address { get; set; }
        /// <summary>
        /// <remarks>
        /// 0为超级用户,1普通用户
        /// </remarks>
        /// </summary>
        public int Type { get; set; }

        public string AppCode { get; set; }

        public string ClientID { get; set; }
    }
}
