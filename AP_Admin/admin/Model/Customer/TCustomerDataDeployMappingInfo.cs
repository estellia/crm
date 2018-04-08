using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// 客户信息
    /// </summary>
    [Serializable]
    [XmlRoot("data")]
    public class TCustomerDataDeployMappingInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        public TCustomerDataDeployMappingInfo()
            : base()
        {
            
        }

        [XmlElement("MappingId")]
        public string MappingId
        { get; set; }

        [XmlElement("CustomerId")]
        public string CustomerId
        { get; set; }

        [XmlElement("DataDeployId")]
        public string DataDeployId
        { get; set; }

        [XmlElement("CreateTime")]
        public string CreateTime
        { get; set; }

        [XmlElement("CreateBy")]
        public string CreateBy
        { get; set; }

        [XmlElement("LastUpdateBy")]
        public string LastUpdateBy
        { get; set; }

        [XmlElement("LastUpdateTime")]
        public string LastUpdateTime
        { get; set; }


        [XmlElement("IsDelete")]
        public int IsDelete
        { get; set; }

        /// <summary>
        /// Jermyn20140529 是否同步到阿拉丁
        /// </summary>
        [XmlElement("IsALD")]
        public int IsALD
        { get; set; }

        [XmlElement("IsPad")]
        public int IsPad
        { get; set; }
        [XmlElement("UnitId")]
        public string UnitId
        { get; set; }
    }
}
