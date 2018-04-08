using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "inout_order")]
    public class InoutOrderContract
    {
        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "order_type_id")]
        public string order_type_id { get; set; }

        [DataMember(Name = "order_reason_id")]
        public string order_reason_id { get; set; }

        [DataMember(Name = "red_flag")]
        public string red_flag { get; set; }

        [DataMember(Name = "ref_order_id")]
        public string ref_order_id { get; set; }

        [DataMember(Name = "ref_order_no")]
        public string ref_order_no { get; set; }

        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "order_date")]
        public string order_date { get; set; }

        [DataMember(Name = "request_date")]
        public string request_date { get; set; }

        [DataMember(Name = "complete_date")]
        public string complete_date { get; set; }

        [DataMember(Name = "create_unit_id")]
        public string create_unit_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "related_unit_id")]
        public string related_unit_id { get; set; }

        [DataMember(Name = "related_unit_code")]
        public string related_unit_code { get; set; }

        [DataMember(Name = "pos_id")]
        public string pos_id { get; set; }

        [DataMember(Name = "shift_id")]
        public string shift_id { get; set; }

        [DataMember(Name = "sales_user")]
        public string sales_user { get; set; }

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

        [DataMember(Name = "pay_id")]
        public string pay_id { get; set; }

        [DataMember(Name = "print_times")]
        public string print_times { get; set; }

        [DataMember(Name = "carrier_id")]
        public string carrier_id { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "approve_time")]
        public string approve_time { get; set; }

        [DataMember(Name = "approve_user_id")]
        public string approve_user_id { get; set; }

        [DataMember(Name = "send_user_id")]
        public string send_user_id { get; set; }

        [DataMember(Name = "send_time")]
        public string send_time { get; set; }

        [DataMember(Name = "accpect_user_id")]
        public string accpect_user_id { get; set; }

        [DataMember(Name = "accpect_time")]
        public string accpect_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "total_qty")]
        public string total_qty { get; set; }

        [DataMember(Name = "total_retail")]
        public string total_retail { get; set; }

        [DataMember(Name = "keep_the_change")]
        public string keep_the_change { get; set; }

        [DataMember(Name = "wiping_zero")]
        public string wiping_zero { get; set; }

        [DataMember(Name = "vip_no")]
        public string vip_no { get; set; }

        [DataMember(Name = "data_from_id")]
        public string data_from_id { get; set; }

        [DataMember(Name = "sales_unit_id")]
        public string sales_unit_id { get; set; }

        [DataMember(Name = "purchase_unit_id")]
        public string purchase_unit_id { get; set; }

        [DataMember(Name = "if_flag")]
        public string if_flag { get; set; }

        [DataMember(Name = "details")]
        public IList<InoutOrderDetailContract> details { get; set; }

        [DataMember(Name = "field1")]
        public string field1 { get; set; }

        [DataMember(Name = "field2")]
        public string field2 { get; set; }

        [DataMember(Name = "field3")]
        public string field3 { get; set; }

        [DataMember(Name = "field4")]
        public string field4 { get; set; }

        [DataMember(Name = "field5")]
        public string field5 { get; set; }

        [DataMember(Name = "field6")]
        public string field6 { get; set; }

        [DataMember(Name = "field7")]
        public string field7 { get; set; }

        [DataMember(Name = "field8")]
        public string field8 { get; set; }

        [DataMember(Name = "field9")]
        public string field9 { get; set; }

        [DataMember(Name = "field10")]
        public string field10 { get; set; }

        [DataMember(Name = "field11")]
        public string field11 { get; set; }

        [DataMember(Name = "field12")]
        public string field12 { get; set; }

        [DataMember(Name = "field13")]
        public string field13 { get; set; }

        [DataMember(Name = "field14")]
        public string field14 { get; set; }

        [DataMember(Name = "field15")]
        public string field15 { get; set; }

        [DataMember(Name = "field16")]
        public string field16 { get; set; }

        [DataMember(Name = "field17")]
        public string field17 { get; set; }

        [DataMember(Name = "field18")]
        public string field18 { get; set; }

        [DataMember(Name = "field19")]
        public string field19 { get; set; }

        [DataMember(Name = "field20")]
        public string field20 { get; set; }
    }
}