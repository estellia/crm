using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility.Sync.WCFService.WCF.API
{
    public class SyncRequest
    {
        public SyncRequest() { }
        public string ClientID { get; set; }
        public string SourceItemID { get; set; }
        public int SourceType { get; set; }
        public string MemberID { get; set; }
    }
}