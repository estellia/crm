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
    [DataContract(Namespace = Common.Config.NS, Name = "monitor_log")]
    public class MonitorLogContract
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "monitor_log_id")]
        public string monitor_log_id { get; set; }

        [DataMember(Name = "customer_id")]
        public string customer_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "pos_id")]
        public string pos_id { get; set; }

        [DataMember(Name = "upload_time")]
        public string upload_time { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }
    }
}