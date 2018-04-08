using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "order")]
    public class OrderContract
    {
        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "order_type_id")]
        public string order_type_id { get; set; }

        [DataMember(Name = "red_flag")]
        public string red_flag { get; set; }

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "ref_order_no")]
        public string ref_order_no { get; set; }

        [DataMember(Name = "ref_order_type_id")]
        public string ref_order_type_id { get; set; }

        [DataMember(Name = "ref_order_id")]
        public string ref_order_id { get; set; }

        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "order_date")]
        public string order_date { get; set; }

        [DataMember(Name = "request_date")]
        public string request_date { get; set; }

        [DataMember(Name = "promise_date")]
        public string promise_date { get; set; }

        [DataMember(Name = "complete_date")]
        public string complete_date { get; set; }

        [DataMember(Name = "create_unit_id")]
        public string create_unit_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "related_unit_id")]
        public string related_unit_id { get; set; }

        [DataMember(Name = "pos_id")]
        public string pos_id { get; set; }

        [DataMember(Name = "total_amount")]
        public string total_amount { get; set; }

        [DataMember(Name = "discount_rate")]
        public string discount_rate { get; set; }

        [DataMember(Name = "actual_amount")]
        public string actual_amount { get; set; }

        [DataMember(Name = "receive_points")]
        public string receive_points { get; set; }

        [DataMember(Name = "pay_points")]
        public string pay_points { get; set; }

        [DataMember(Name = "address_1")]
        public string address_1 { get; set; }

        [DataMember(Name = "address_2")]
        public string address_2 { get; set; }

        [DataMember(Name = "zip")]
        public string zip { get; set; }

        [DataMember(Name = "phone")]
        public string phone { get; set; }

        [DataMember(Name = "fax")]
        public string fax { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "carrier_id")]
        public string carrier_id { get; set; }

        [DataMember(Name = "order_status")]
        public string order_status { get; set; }

        [DataMember(Name = "print_times")]
        public string print_times { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "send_user_id")]
        public string send_user_id { get; set; }

        [DataMember(Name = "send_time")]
        public string send_time { get; set; }

        [DataMember(Name = "accept_user_id")]
        public string accept_user_id { get; set; }

        [DataMember(Name = "accept_time")]
        public string accept_time { get; set; }

        [DataMember(Name = "approve_user_id")]
        public string approve_user_id { get; set; }

        [DataMember(Name = "approve_time")]
        public string approve_time { get; set; }

        [DataMember(Name = "details")]
        public IList<OrderDetailContract> details { get; set; }
    }
}