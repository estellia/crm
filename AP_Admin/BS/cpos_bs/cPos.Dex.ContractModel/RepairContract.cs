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
    [DataContract(Namespace = Common.Config.NS, Name = "repair")]
    public class RepairContract
    {
        [DataMember(Name = "repair_id")]
        public string repair_id { get; set; }

        [DataMember(Name = "repair_type_id")]
        public string repair_type_id { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "phone")]
        public string phone { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "pos_code")]
        public string pos_code { get; set; }

        [DataMember(Name = "pos_sn")]
        public string pos_sn { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "status_desc")]
        public string status_desc { get; set; }

        [DataMember(Name = "repair_time")]
        public string repair_time { get; set; }

        [DataMember(Name = "repair_user_id")]
        public string repair_user_id { get; set; }

        [DataMember(Name = "response_time")]
        public string response_time { get; set; }

        [DataMember(Name = "response_user_id")]
        public string response_user_id { get; set; }

        [DataMember(Name = "complete_time")]
        public string complete_time { get; set; }

        [DataMember(Name = "complete_user_id")]
        public string complete_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }
    }
}