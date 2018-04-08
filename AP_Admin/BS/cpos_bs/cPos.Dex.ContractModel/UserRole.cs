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
    [DataContract(Namespace = Common.Config.NS, Name = "user_role")]
    public class UserRole
    {
        [DataMember(Name = "user_role_id")]
        public string user_role_id { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "role_id")]
        public string role_id { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}