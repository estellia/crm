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
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class GetPackagesContract : BaseContract
    {
        [DataMember(Name = "packages")]
        public IList<Package> packages { get; set; }

        [DataMember(Name = "zip_file")]
        public string zip_file { get; set; }

        [DataMember(Name = "ftp_ip")]
        public string ftp_ip { get; set; }

        [DataMember(Name = "ftp_account_name")]
        public string ftp_account_name { get; set; }

        [DataMember(Name = "ftp_account_pwd")]
        public string ftp_account_pwd { get; set; }
    }
}