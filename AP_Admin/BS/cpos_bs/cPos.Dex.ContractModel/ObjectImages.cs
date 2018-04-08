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
     [DataContract(Namespace = Common.Config.NS, Name = "ObjectImages")]
    public class ObjectImages
    {
        [DataMember(Name = "ImageId")]
         public string ImageId { get; set; }

        [DataMember(Name = "ObjectId")]
        public string ObjectId { get; set; }

        [DataMember(Name = "ImageURL")]
        public string ImageURL { get; set; }

        [DataMember(Name = "DisplayIndex")]
        public int DisplayIndex { get; set; }

        [DataMember(Name = "CreateTime")]
        public string CreateTime { get; set; }     //在contract层只能定义为string类型，定义为datetime不对。

        [DataMember(Name = "CreateBy")]
        public string CreateBy { get; set; }

        [DataMember(Name = "LastUpdateBy")]
        public string LastUpdateBy { get; set; }

        [DataMember(Name = "LastUpdateTime")]
        public string LastUpdateTime { get; set; }

        [DataMember(Name = "IsDelete")]
        public int IsDelete { get; set; }

        [DataMember(Name = "CustomerId")]
        public string CustomerId { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "IfFlag")]
        public int IfFlag { get; set; }

        [DataMember(Name = "BatId")]
        public string BatId { get; set; }

     
    }
}
