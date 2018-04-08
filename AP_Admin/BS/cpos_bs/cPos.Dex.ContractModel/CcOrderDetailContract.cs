using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "cc_order_detail")]
    public class CcOrderDetailContract
    {
        [DataMember(Name = "order_detail_id")]
        public string order_detail_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "ref_order_detail_id")]
        public string ref_order_detail_id { get; set; }

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "sku_id")]
        public string sku_id { get; set; }

        [DataMember(Name = "end_qty")]
        public string end_qty { get; set; }

        [DataMember(Name = "order_qty")]
        public string order_qty { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

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

        [DataMember(Name = "difference_qty")]
        public string difference_qty { get; set; }

        [DataMember(Name = "item_code")]
        public string item_code { get; set; }

        [DataMember(Name = "item_name")]
        public string item_name { get; set; }

        [DataMember(Name = "barcode")]
        public string barcode { get; set; }

        [DataMember(Name = "sku_prop_1_name")]
        public string sku_prop_1_name { get; set; }

        [DataMember(Name = "sku_prop_2_name")]
        public string sku_prop_2_name { get; set; }

        [DataMember(Name = "sku_prop_3_name")]
        public string sku_prop_3_name { get; set; }

        [DataMember(Name = "sku_prop_4_name")]
        public string sku_prop_4_name { get; set; }

        [DataMember(Name = "sku_prop_5_name")]
        public string sku_prop_5_name { get; set; }

        [DataMember(Name = "enter_price")]
        public string enter_price { get; set; }

        [DataMember(Name = "sales_price")]
        public string sales_price { get; set; }

        [DataMember(Name = "brand_name")]
        public string brand_name { get; set; }

        [DataMember(Name = "item_category_id")]
        public string item_category_id { get; set; }
    }
}