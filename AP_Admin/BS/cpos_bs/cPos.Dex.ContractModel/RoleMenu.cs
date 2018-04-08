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
    [DataContract(Namespace = Common.Config.NS, Name = "role_menu")]
    public class RoleMenu
    {
        [DataMember(Name = "role_menu_id")]
        public string role_menu_id { get; set; }

        [DataMember(Name = "role_id")]
        public string role_id { get; set; }

        [DataMember(Name = "menu_id")]
        public string menu_id { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}