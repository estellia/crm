using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "order_detail")]
    public class OrderDetailContract
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

        [DataMember(Name = "status")]
        public string status { get; set; }

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

        [DataMember(Name = "if_flag")]
        public string if_flag { get; set; }
    }
}