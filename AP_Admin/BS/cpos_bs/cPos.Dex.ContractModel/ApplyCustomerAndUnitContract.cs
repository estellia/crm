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
    public class ApplyCustomerAndUnitContract : BaseContract
    {

    }

    [DataContract(Namespace = Common.Config.NS, Name = "customer_unit_apply")]
    public class CustomerUnitApply
    {
        [DataMember(Name = "customers")]
        public IList<Customer> customers { get; set; }

        [DataMember(Name = "units")]
        public IList<Unit> units { get; set; }
    }
}