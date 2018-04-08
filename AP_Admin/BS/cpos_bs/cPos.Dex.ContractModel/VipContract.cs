using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "vip")]
    public class VipContract
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "no")]
        public string no { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "gender")]
        public string gender { get; set; }

        [DataMember(Name = "english_name")]
        public string english_name { get; set; }

        [DataMember(Name = "identity_no")]
        public string identity_no { get; set; }

        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "postcode")]
        public string postcode { get; set; }

        [DataMember(Name = "birthday")]
        public string birthday { get; set; }

        [DataMember(Name = "cell")]
        public string cell { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "qq")]
        public string qq { get; set; }

        [DataMember(Name = "msn")]
        public string msn { get; set; }

        [DataMember(Name = "weibo")]
        public string weibo { get; set; }

        [DataMember(Name = "points")]
        public string points { get; set; }

        [DataMember(Name = "expired_date")]
        public string expired_date { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "activate_unit_id")]
        public string activate_unit_id { get; set; }

        [DataMember(Name = "activate_time")]
        public string activate_time { get; set; }

        [DataMember(Name = "type_id")]
        public string type_id { get; set; }

        [DataMember(Name = "type_code")]
        public string type_code { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_user_name")]
        public string create_user_name { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_user_name")]
        public string modify_user_name { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "version")]
        public string version { get; set; }
    }
}