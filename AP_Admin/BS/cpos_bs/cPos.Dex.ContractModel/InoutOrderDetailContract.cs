using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "inout_order_detail")]
    public class InoutOrderDetailContract
    {
        [DataMember(Name = "order_detail_id")]
        public string order_detail_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "ref_order_detail_id")]
        public string ref_order_detail_id { get; set; }

        [DataMember(Name = "sku_id")]
        public string sku_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "enter_qty")]
        public string enter_qty { get; set; }

        [DataMember(Name = "order_qty")]
        public string order_qty { get; set; }

        [DataMember(Name = "enter_price")]
        public string enter_price { get; set; }

        [DataMember(Name = "std_price")]
        public string std_price { get; set; }

        [DataMember(Name = "discount_rate")]
        public string discount_rate { get; set; }

        [DataMember(Name = "retail_price")]
        public string retail_price { get; set; }

        [DataMember(Name = "retail_amount")]
        public string retail_amount { get; set; }

        [DataMember(Name = "enter_amount")]
        public string enter_amount { get; set; }

        [DataMember(Name = "receive_points")]
        public string receive_points { get; set; }

        [DataMember(Name = "pay_points")]
        public string pay_points { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "order_detail_status")]
        public string order_detail_status { get; set; }

        [DataMember(Name = "display_index")]
        public string display_index { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "ref_order_id")]
        public string ref_order_id { get; set; }

        [DataMember(Name = "if_flag")]
        public string if_flag { get; set; }

        [DataMember(Name = "pos_order_code")]
        public string pos_order_code { get; set; }

        [DataMember(Name = "plan_price")]
        public string plan_price { get; set; }

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
    }
}