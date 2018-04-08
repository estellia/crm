using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "cc_order")]
    public class CcOrderContract
    {
        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "order_type_id")]
        public string order_type_id { get; set; }

        [DataMember(Name = "order_reason_id")]
        public string order_reason_id { get; set; }

        [DataMember(Name = "ref_order_id")]
        public string ref_order_id { get; set; }

        [DataMember(Name = "ref_order_no")]
        public string ref_order_no { get; set; }

        [DataMember(Name = "order_date")]
        public string order_date { get; set; }

        [DataMember(Name = "request_date")]
        public string request_date { get; set; }

        [DataMember(Name = "complete_date")]
        public string complete_date { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "pos_id")]
        public string pos_id { get; set; }

        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

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

        [DataMember(Name = "data_from_id")]
        public string data_from_id { get; set; }

        [DataMember(Name = "send_user_id")]
        public string send_user_id { get; set; }

        [DataMember(Name = "send_time")]
        public string send_time { get; set; }

        [DataMember(Name = "approve_user_id")]
        public string approve_user_id { get; set; }

        [DataMember(Name = "approve_time")]
        public string approve_time { get; set; }

        [DataMember(Name = "accept_user_id")]
        public string accept_user_id { get; set; }

        [DataMember(Name = "accept_time")]
        public string accept_time { get; set; }

        [DataMember(Name = "if_flag")]
        public string if_flag { get; set; }

        [DataMember(Name = "details")]
        public IList<CcOrderDetailContract> details { get; set; }
    }
}