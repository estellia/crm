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
    [DataContract(Namespace = Common.Config.NS, Name = "role")]
    public class Role
    {
        [DataMember(Name = "role_id")]
        public string role_id { get; set; }

        [DataMember(Name = "role_name")]
        public string role_name { get; set; }

        [DataMember(Name = "role_code")]
        public string role_code { get; set; }

        [DataMember(Name = "is_sys")]
        public string is_sys { get; set; }

        [DataMember(Name = "role_eng_name")]
        public string role_eng_name { get; set; }
    }
}