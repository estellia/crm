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
    [DataContract(Namespace = Common.Config.NS, Name = "item_prop_def")]
    public class ItemPropDef
    {
        [DataMember(Name = "prop_id")]
        public string prop_id { get; set; }

        [DataMember(Name = "prop_code")]
        public string prop_code { get; set; }

        [DataMember(Name = "prop_name")]
        public string prop_name { get; set; }

        [DataMember(Name = "display_index")]
        public string display_index { get; set; }
    }
}