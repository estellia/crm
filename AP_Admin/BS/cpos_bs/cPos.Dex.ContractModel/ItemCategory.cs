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
    [DataContract(Namespace = Common.Config.NS, Name = "item_category")]
    public class ItemCategory
    {
        [DataMember(Name = "item_category_id")]
        public string item_category_id { get; set; }

        [DataMember(Name = "item_category_code")]
        public string item_category_code { get; set; }

        [DataMember(Name = "item_category_name")]
        public string item_category_name { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "parent_id")]
        public string parent_id { get; set; }

        [DataMember(Name = "pyzjm")]
        public string pyzjm { get; set; }

        [DataMember(Name = "display_index")]
        public string display_index { get; set; }
    }
}