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
    [DataContract(Namespace = Common.Config.NS, Name = "item_price")]
    public class ItemPrice
    {
        [DataMember(Name = "item_price_id")]
        public string item_price_id { get; set; }

        [DataMember(Name = "item_id")]
        public string item_id { get; set; }

        [DataMember(Name = "item_price_type_id")]
        public string item_price_type_id { get; set; }

        [DataMember(Name = "item_price_type_name")]
        public string item_price_type_name { get; set; }

        [DataMember(Name = "item_price")]
        public string item_price { get; set; }

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
    }
}