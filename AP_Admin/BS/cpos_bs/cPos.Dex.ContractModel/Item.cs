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
    [DataContract(Namespace = Common.Config.NS, Name = "item")]
    public class Item
    {
        [DataMember(Name = "item_id")]
        public string item_id { get; set; }

        [DataMember(Name = "item_category_id")]
        public string item_category_id { get; set; }

        [DataMember(Name = "item_code")]
        public string item_code { get; set; }

        [DataMember(Name = "item_name")]
        public string item_name { get; set; }

        [DataMember(Name = "item_name_en")]
        public string item_name_en { get; set; }

        [DataMember(Name = "item_name_short")]
        public string item_name_short { get; set; }

        [DataMember(Name = "item_status")]
        public string item_status { get; set; }

        [DataMember(Name = "item_remark")]
        public string item_remark { get; set; }

        [DataMember(Name = "pyzjm")]
        public string pyzjm { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "item_props")]
        public IList<ItemProp> item_props { get; set; }

        [DataMember(Name = "if_gifts")]
        public string if_gifts { get; set; }

        [DataMember(Name = "if_often")]
        public string if_often { get; set; }

        [DataMember(Name = "if_service")]
        public string if_service { get; set; }

        [DataMember(Name = "isgb")]
        public string isgb { get; set; }

        [DataMember(Name = "data_from")]
        public string data_from { get; set; }

        [DataMember(Name = "display_index")]
        public string display_index { get; set; }

        [DataMember(Name = "image_url")]
        public string image_url { get; set; }
    }
}