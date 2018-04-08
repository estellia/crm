using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "price_order")]
    public class PriceOrderContract
    {
        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "order_date")]
        public string order_date { get; set; }

        [DataMember(Name = "begin_date")]
        public string begin_date { get; set; }

        [DataMember(Name = "end_date")]
        public string end_date { get; set; }

        [DataMember(Name = "item_price_type_id")]
        public string item_price_type_id { get; set; }

        [DataMember(Name = "item_price_type_name")]
        public string item_price_type_name { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "status_desc")]
        public string status_desc { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "no")]
        public string no { get; set; }

        [DataMember(Name = "item_details")]
        public IList<PriceOrderDetailItemContract> item_details { get; set; }

        [DataMember(Name = "sku_details")]
        public IList<PriceOrderDetailSkuContract> sku_details { get; set; }

        [DataMember(Name = "unit_details")]
        public IList<PriceOrderDetailUnitContract> unit_details { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "price_order_detail_item")]
    public class PriceOrderDetailItemContract
    {
        [DataMember(Name = "order_detail_item_id")]
        public string order_detail_item_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "item_id")]
        public string item_id { get; set; }

        [DataMember(Name = "price")]
        public string price { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "price_order_detail_sku")]
    public class PriceOrderDetailSkuContract
    {
        [DataMember(Name = "order_detail_sku_id")]
        public string order_detail_sku_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "sku_id")]
        public string sku_id { get; set; }

        [DataMember(Name = "price")]
        public string price { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "price_order_detail_unit")]
    public class PriceOrderDetailUnitContract
    {
        [DataMember(Name = "order_detail_unit_id")]
        public string order_detail_unit_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }
    }
}