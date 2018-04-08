using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class DownloadOrdersContract : BaseContract
    {
        [DataMember(Name = "orders")]
        public IList<OrderContract> orders { get; set; }
    }
}