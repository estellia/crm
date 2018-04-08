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
    [DataContract(Namespace = Common.Config.NS, Name = "customer")]
    public class Customer
    {
        [DataMember(Name = "customer_id")]
        public string customer_id { get; set; }

        [DataMember(Name = "customer_code")]
        public string customer_code { get; set; }

        [DataMember(Name = "customer_name")]
        public string customer_name { get; set; }

        [DataMember(Name = "customer_name_en")]
        public string customer_name_en { get; set; }

        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "post_code")]
        public string post_code { get; set; }

        [DataMember(Name = "contacter")]
        public string contacter { get; set; }

        [DataMember(Name = "tel")]
        public string tel { get; set; }

        [DataMember(Name = "fax")]
        public string fax { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "cell")]
        public string cell { get; set; }

        [DataMember(Name = "memo")]
        public string memo { get; set; }

        [DataMember(Name = "start_date")]
        public string start_date { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}