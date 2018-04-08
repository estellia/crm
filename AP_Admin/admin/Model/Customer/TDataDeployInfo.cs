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
    public class TDataDeployInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        public TDataDeployInfo()
            : base()
        {
            
        }

        [XmlElement("DataDeployId")]
        public string DataDeployId
        { get; set; }

        [XmlElement("DataDeployName")]
        public string DataDeployName
        { get; set; }

        [XmlElement("DataDeployDesc")]
        public string DataDeployDesc
        { get; set; }

        [XmlElement("db_server")]
        public string db_server
        { get; set; }

        [XmlElement("db_name")]
        public string db_name
        { get; set; }

        [XmlElement("db_user")]
        public string db_user
        { get; set; }

        [XmlElement("db_pwd")]
        public string db_pwd
        { get; set; }

        [XmlElement("access_url")]
        public string access_url
        { get; set; }

        [XmlElement("max_shop_count")]
        public int max_shop_count
        { get; set; }

        [XmlElement("max_user_count")]
        public int max_user_count
        { get; set; }

        [XmlElement("max_terminal_count")]
        public int max_terminal_count
        { get; set; }

        [XmlElement("key_file")]
        public string key_file
        { get; set; }

        [XmlElement("IsDelete")]
        public int IsDelete
        { get; set; }

        [XmlElement("CustomerCount")]
        public int CustomerCount
        { get; set; }

        /// <summary>
        /// 是否同步到阿拉丁 Jermyn20140529
        /// </summary>
        [XmlIgnore()]
        public int IsALD
        { get; set; }

        /// <summary>
        /// 是否同步到Pad Jermyn20140529
        /// </summary>
        [XmlIgnore()]
        public int IsPad
        { get; set; }
    }
}
