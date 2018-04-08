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
    [DataContract(Namespace = Common.Config.NS, Name = "shift")]
    public class ShiftContract
    {
        [DataMember(Name = "shift_id")]
        public string shift_id { get; set; }

        [DataMember(Name = "sales_user")]
        public string sales_user { get; set; }

        [DataMember(Name = "pos_id")]
        public string pos_id { get; set; }

        [DataMember(Name = "parent_shift_id")]
        public string parent_shift_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "deposit_amount")]
        public string deposit_amount { get; set; }

        [DataMember(Name = "sale_amount")]
        public string sale_amount { get; set; }

        [DataMember(Name = "return_amount")]
        public string return_amount { get; set; }

        [DataMember(Name = "pos_date")]
        public string pos_date { get; set; }

        [DataMember(Name = "sales_qty")]
        public string sales_qty { get; set; }

        [DataMember(Name = "sales_total_amount")]
        public string sales_total_amount { get; set; }

        [DataMember(Name = "open_time")]
        public string open_time { get; set; }

        [DataMember(Name = "close_time")]
        public string close_time { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "sales_total_qty")]
        public string sales_total_qty { get; set; }

        [DataMember(Name = "sales_total_total_amount")]
        public string sales_total_total_amount { get; set; }
    }
}