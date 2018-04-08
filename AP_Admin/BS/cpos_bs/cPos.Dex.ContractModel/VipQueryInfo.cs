using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class VipQueryInfo
    {
        [DataMember(Name = "vip_no")]
        public string vip_no { get; set; }

        [DataMember(Name = "vip_type")]
        public string vip_type { get; set; }

        [DataMember(Name = "vip_name")]
        public string vip_name { get; set; }

        [DataMember(Name = "vip_cell")]
        public string vip_cell { get; set; }

        [DataMember(Name = "vip_identity_no")]
        public string vip_identity_no { get; set; }
    }
}