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
    public class ItemCategorysContract : BaseContract
    {
        [DataMember(Name = "item_categorys")]
        public IList<ItemCategory> item_categorys { get; set; }
    }
}