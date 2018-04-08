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
    [DataContract(Namespace = Common.Config.NS, Name = "unit")]
    public class Unit
    {
        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "customer_id")]
        public string customer_id { get; set; }

        [DataMember(Name = "type_id")]
        public string type_id { get; set; }

        [DataMember(Name = "unit_code")]
        public string unit_code { get; set; }

        [DataMember(Name = "unit_name")]
        public string unit_name { get; set; }

        [DataMember(Name = "unit_name_en")]
        public string unit_name_en { get; set; }

        [DataMember(Name = "unit_name_short")]
        public string unit_name_short { get; set; }

        [DataMember(Name = "unit_city_id")]
        public string unit_city_id { get; set; }

        [DataMember(Name = "unit_address")]
        public string unit_address { get; set; }

        [DataMember(Name = "unit_contact")]
        public string unit_contact { get; set; }

        [DataMember(Name = "unit_tel")]
        public string unit_tel { get; set; }

        [DataMember(Name = "unit_fax")]
        public string unit_fax { get; set; }

        [DataMember(Name = "unit_email")]
        public string unit_email { get; set; }

        [DataMember(Name = "unit_postcode")]
        public string unit_postcode { get; set; }

        [DataMember(Name = "unit_remark")]
        public string unit_remark { get; set; }

        [DataMember(Name = "unit_status")]
        public string unit_status { get; set; }

        [DataMember(Name = "unit_props")]
        public IList<UnitProp> unit_props { get; set; }

        [DataMember(Name = "warehouses")]
        public IList<Warehouse> warehouses { get; set; }

        [DataMember(Name = "longitude")]
        public string longitude { get; set; }

        [DataMember(Name = "dimension")]
        public string dimension { get; set; }

        [DataMember(Name = "shop_url1")]
        public string shop_url1 { get; set; }

        [DataMember(Name = "shop_url2")]
        public string shop_url2 { get; set; }

        [DataMember(Name = "imager_url")]
        public string imager_url { get; set; }
    }
}