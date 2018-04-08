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
    public class MobileBasicContract : BaseContract
    {
        [DataMember(Name = "customers")]
        public IList<Customer> customers { get; set; }

        [DataMember(Name = "units")]
        public IList<Unit> units { get; set; }

        [DataMember(Name = "skus")]
        public IList<Sku> skus { get; set; }
    }
}