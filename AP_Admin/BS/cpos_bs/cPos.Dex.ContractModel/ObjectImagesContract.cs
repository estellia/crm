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
    public class ObjectImagesContract : BaseContract
    {
        [DataMember(Name = "ObjectImages")]   //这里要跟ObjectImages.cs上面 Name = "ObjectImages"定义一致 
        public IList<ObjectImages> ObjectImages{ get; set; }//这个必须要和表名一致，以便后面取的人用。
    }
}
