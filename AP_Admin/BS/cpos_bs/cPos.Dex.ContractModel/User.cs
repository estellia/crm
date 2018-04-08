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
    [DataContract(Namespace = Common.Config.NS, Name = "user")]
    public class User
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "user_code")]
        public string user_code { get; set; }

        [DataMember(Name = "user_name")]
        public string user_name { get; set; }

        [DataMember(Name = "user_name_en")]
        public string user_name_en { get; set; }

        [DataMember(Name = "user_password")]
        public string user_password { get; set; }

        [DataMember(Name = "user_email")]
        public string user_email { get; set; }

        [DataMember(Name = "user_mobile")]
        public string user_mobile { get; set; }

        [DataMember(Name = "user_tel")]
        public string user_tel { get; set; }

        [DataMember(Name = "user_status")]
        public string user_status { get; set; }

        [DataMember(Name = "user_gender")]
        public string user_gender { get; set; }

        [DataMember(Name = "user_birthday")]
        public string user_birthday { get; set; }

        [DataMember(Name = "user_identity")]
        public string user_identity { get; set; }

        [DataMember(Name = "user_address")]
        public string user_address { get; set; }

        [DataMember(Name = "user_postcode")]
        public string user_postcode { get; set; }

        [DataMember(Name = "qq")]
        public string qq { get; set; }

        [DataMember(Name = "msn")]
        public string msn { get; set; }

        [DataMember(Name = "blog")]
        public string blog { get; set; }

        [DataMember(Name = "user_remark")]
        public string user_remark { get; set; }
    }
}