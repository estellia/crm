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
    public class CheckVersionContract : BaseContract
    {
        [DataMember(Name = "pos_version")]
        public PosVersionContract pos_version { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "pos_version")]
    public class PosVersionContract
    {
        [DataMember(Name = "version_id")]
        public string version_id { get; set; }

        [DataMember(Name = "version_no")]
        public string version_no { get; set; }

        [DataMember(Name = "version_path")]
        public string version_path { get; set; }

        [DataMember(Name = "version_url")]
        public string version_url { get; set; }

        [DataMember(Name = "file_name")]
        public string file_name { get; set; }

        //[DataMember(Name = "version_status")]
        //public string version_status { get; set; }

        //[DataMember(Name = "sort_flag")]
        //public string sort_flag { get; set; }

        [DataMember(Name = "version_size")]
        public string version_size { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }

        [DataMember(Name = "files")]
        public IList<PosVersionFileContract> files { get; set; }

        [DataMember(Name = "files_count")]
        public int files_count { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "pos_version_file")]
    public class PosVersionFileContract
    {
        [DataMember(Name = "file_id")]
        public string file_id { get; set; }

        [DataMember(Name = "version_id")]
        public string version_id { get; set; }

        [DataMember(Name = "file_url")]
        public string file_url { get; set; }

        [DataMember(Name = "file_name")]
        public string file_name { get; set; }

        [DataMember(Name = "file_size")]
        public string file_size { get; set; }

        [DataMember(Name = "remark")]
        public string remark { get; set; }
    }
}