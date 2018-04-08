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
    [DataContract(Namespace = Common.Config.NS, Name = "package")]
    public class Package
    {
        [DataMember(Name = "package_id")]
        public string package_id { get; set; }

        [DataMember(Name = "package_type_id")]
        public string package_type_id { get; set; }

        [DataMember(Name = "package_type_code")]
        public string package_type_code { get; set; }

        [DataMember(Name = "package_gen_type_id")]
        public string package_gen_type_id { get; set; }

        [DataMember(Name = "package_gen_type_code")]
        public string package_gen_type_code { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "package_name")]
        public string package_name { get; set; }

        [DataMember(Name = "package_desc")]
        public string package_desc { get; set; }

        [DataMember(Name = "package_seq")]
        public string package_seq { get; set; }

        [DataMember(Name = "package_status")]
        public string package_status { get; set; }

        [DataMember(Name = "package_files_count")]
        public int package_files_count { get; set; }

        [DataMember(Name = "package_files")]
        public IList<PackageFile> package_files { get; set; }
    }
}