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
    [DataContract(Namespace = Common.Config.NS, Name = "warehouse")]
    public class Warehouse
    {
        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "wh_code")]
        public string wh_code { get; set; }

        [DataMember(Name = "wh_name")]
        public string wh_name { get; set; }

        [DataMember(Name = "wh_name_en")]
        public string wh_name_en { get; set; }

        [DataMember(Name = "wh_address")]
        public string wh_address { get; set; }

        [DataMember(Name = "wh_contacter")]
        public string wh_contacter { get; set; }

        [DataMember(Name = "wh_tel")]
        public string wh_tel { get; set; }

        [DataMember(Name = "wh_fax")]
        public string wh_fax { get; set; }

        [DataMember(Name = "wh_status")]
        public string wh_status { get; set; }

        [DataMember(Name = "wh_remark")]
        public string wh_remark { get; set; }

        [DataMember(Name = "is_default")]
        public string is_default { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }
    }
}