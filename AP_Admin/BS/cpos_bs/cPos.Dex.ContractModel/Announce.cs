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
    [DataContract(Namespace = Common.Config.NS, Name = "announce")]
    public class Announce
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "no")]
        public string no { get; set; }

        [DataMember(Name = "title")]
        public string title { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "content")]
        public string content { get; set; }

        [DataMember(Name = "publisher")]
        public string publisher { get; set; }

        [DataMember(Name = "begin_date")]
        public string begin_date { get; set; }

        [DataMember(Name = "end_date")]
        public string end_date { get; set; }
    }
}