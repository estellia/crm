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
    [DataContract(Namespace = Common.Config.NS, Name = "unit_prop")]
    public class UnitProp
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "property_code_group_id")]
        public string property_code_group_id { get; set; }

        [DataMember(Name = "property_code_id")]
        public string property_code_id { get; set; }

        [DataMember(Name = "property_detail_id")]
        public string property_detail_id { get; set; }

        [DataMember(Name = "property_detail_code")]
        public string property_detail_code { get; set; }

        [DataMember(Name = "property_detail_name")]
        public string property_detail_name { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "property_code_group_name")]
        public string property_code_group_name { get; set; }

        [DataMember(Name = "property_code_group_code")]
        public string property_code_group_code { get; set; }

        [DataMember(Name = "property_code_name")]
        public string property_code_name { get; set; }

        [DataMember(Name = "property_code_code")]
        public string property_code_code { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }
    }
}