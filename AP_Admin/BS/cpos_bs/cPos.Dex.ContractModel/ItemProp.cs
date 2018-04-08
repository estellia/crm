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
    [DataContract(Namespace = Common.Config.NS, Name = "item_prop")]
    public class ItemProp
    {
        [DataMember(Name = "item_id")]
        public string item_id { get; set; }

        [DataMember(Name = "item_property_id")]
        public string item_property_id { get; set; }

        [DataMember(Name = "property_code_group_id")]
        public string property_code_group_id { get; set; }

        [DataMember(Name = "property_code_id")]
        public string property_code_id { get; set; }

        [DataMember(Name = "property_detail_id")]
        public string property_detail_id { get; set; }

        [DataMember(Name = "property_code_value")]
        public string property_code_value { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "property_code_group_name")]
        public string property_code_group_name { get; set; }

        [DataMember(Name = "property_code_name")]
        public string property_code_name { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }
    }
}