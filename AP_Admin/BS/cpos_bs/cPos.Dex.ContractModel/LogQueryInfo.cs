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
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class LogQueryInfo
    {
        [DataMember(Name = "log_id")]
        public string log_id { get; set; }

        [DataMember(Name = "biz_id")]
        public string biz_id { get; set; }

        [DataMember(Name = "biz_name")]
        public string biz_name { get; set; }

        [DataMember(Name = "log_type_id")]
        public string log_type_id { get; set; }

        [DataMember(Name = "log_type_code")]
        public string log_type_code { get; set; }

        [DataMember(Name = "log_code")]
        public string log_code { get; set; }

        [DataMember(Name = "log_body")]
        public string log_body { get; set; }

        [DataMember(Name = "create_time_begin")]
        public string create_time_begin { get; set; }

        [DataMember(Name = "create_time_end")]
        public string create_time_end { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time_begin")]
        public string modify_time_begin { get; set; }

        [DataMember(Name = "modify_time_end")]
        public string modify_time_end { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "customer_code")]
        public string customer_code { get; set; }

        [DataMember(Name = "customer_id")]
        public string customer_id { get; set; }

        [DataMember(Name = "unit_code")]
        public string unit_code { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "user_code")]
        public string user_code { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "if_code")]
        public string if_code { get; set; }

        [DataMember(Name = "app_code")]
        public string app_code { get; set; }

        [DataMember(Name = "start_row")]
        public int start_row { get; set; }

        [DataMember(Name = "rows_count")]
        public int rows_count { get; set; }
    }
}