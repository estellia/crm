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
    [DataContract(Namespace = Common.Config.NS, Name = "package_file")]
    public class PackageFile
    {
        [DataMember(Name = "file_id")]
        public string file_id { get; set; }

        [DataMember(Name = "package_id")]
        public string package_id { get; set; }

        [DataMember(Name = "file_name")]
        public string file_name { get; set; }

        [DataMember(Name = "file_seq")]
        public string file_seq { get; set; }

        [DataMember(Name = "file_path")]
        public string file_path { get; set; }

        [DataMember(Name = "file_status")]
        public string file_status { get; set; }

        [DataMember(Name = "ftp_ip")]
        public string ftp_ip { get; set; }

        [DataMember(Name = "ftp_account_name")]
        public string ftp_account_name { get; set; }

        [DataMember(Name = "ftp_account_pwd")]
        public string ftp_account_pwd { get; set; }
    }
}