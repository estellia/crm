using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class TransData
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }
    }
}