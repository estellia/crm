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
    [DataContract(Namespace = Common.Config.NS, Name = "sku")]
    public class Sku
    {
        [DataMember(Name = "sku_id")]
        public string sku_id { get; set; }

        [DataMember(Name = "item_id")]
        public string item_id { get; set; }

        [DataMember(Name = "prop_1_detail_id")]
        public string prop_1_detail_id { get; set; }

        [DataMember(Name = "prop_1_detail_code")]
        public string prop_1_detail_code { get; set; }

        [DataMember(Name = "prop_1_detail_name")]
        public string prop_1_detail_name { get; set; }

        [DataMember(Name = "prop_2_detail_id")]
        public string prop_2_detail_id { get; set; }

        [DataMember(Name = "prop_2_detail_code")]
        public string prop_2_detail_code { get; set; }

        [DataMember(Name = "prop_2_detail_name")]
        public string prop_2_detail_name { get; set; }

        [DataMember(Name = "prop_3_detail_id")]
        public string prop_3_detail_id { get; set; }

        [DataMember(Name = "prop_3_detail_code")]
        public string prop_3_detail_code { get; set; }

        [DataMember(Name = "prop_3_detail_name")]
        public string prop_3_detail_name { get; set; }

        [DataMember(Name = "prop_4_detail_id")]
        public string prop_4_detail_id { get; set; }

        [DataMember(Name = "prop_4_detail_code")]
        public string prop_4_detail_code { get; set; }

        [DataMember(Name = "prop_4_detail_name")]
        public string prop_4_detail_name { get; set; }

        [DataMember(Name = "prop_5_detail_id")]
        public string prop_5_detail_id { get; set; }

        [DataMember(Name = "prop_5_detail_code")]
        public string prop_5_detail_code { get; set; }

        [DataMember(Name = "prop_5_detail_name")]
        public string prop_5_detail_name { get; set; }

        [DataMember(Name = "prop_1_id")]
        public string prop_1_id { get; set; }

        [DataMember(Name = "prop_1_code")]
        public string prop_1_code { get; set; }

        [DataMember(Name = "prop_1_name")]
        public string prop_1_name { get; set; }

        [DataMember(Name = "prop_2_id")]
        public string prop_2_id { get; set; }

        [DataMember(Name = "prop_2_code")]
        public string prop_2_code { get; set; }

        [DataMember(Name = "prop_2_name")]
        public string prop_2_name { get; set; }

        [DataMember(Name = "prop_3_id")]
        public string prop_3_id { get; set; }

        [DataMember(Name = "prop_3_code")]
        public string prop_3_code { get; set; }

        [DataMember(Name = "prop_3_name")]
        public string prop_3_name { get; set; }

        [DataMember(Name = "prop_4_id")]
        public string prop_4_id { get; set; }

        [DataMember(Name = "prop_4_code")]
        public string prop_4_code { get; set; }

        [DataMember(Name = "prop_4_name")]
        public string prop_4_name { get; set; }

        [DataMember(Name = "prop_5_id")]
        public string prop_5_id { get; set; }

        [DataMember(Name = "prop_5_code")]
        public string prop_5_code { get; set; }

        [DataMember(Name = "prop_5_name")]
        public string prop_5_name { get; set; }

        [DataMember(Name = "barcode")]
        public string barcode { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }
    }
}