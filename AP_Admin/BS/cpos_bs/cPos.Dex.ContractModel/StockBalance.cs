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
    [DataContract(Namespace = Common.Config.NS, Name = "stock_balance")]
    public class StockBalance
    {
        [DataMember(Name = "stock_balance_id")]
        public string stock_balance_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "sku_id")]
        public string sku_id { get; set; }

        [DataMember(Name = "warehouse_id")]
        public string warehouse_id { get; set; }

        [DataMember(Name = "begin_qty")]
        public string begin_qty { get; set; }

        [DataMember(Name = "in_qty")]
        public string in_qty { get; set; }

        [DataMember(Name = "out_qty")]
        public string out_qty { get; set; }

        [DataMember(Name = "adjust_in_qty")]
        public string adjust_in_qty { get; set; }

        [DataMember(Name = "adjust_out_qty")]
        public string adjust_out_qty { get; set; }

        [DataMember(Name = "reserver_qty")]
        public string reserver_qty { get; set; }

        [DataMember(Name = "on_way_qty")]
        public string on_way_qty { get; set; }

        [DataMember(Name = "end_qty")]
        public string end_qty { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "price")]
        public string price { get; set; }

        [DataMember(Name = "item_label_type_id")]
        public string item_label_type_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }
    }
}