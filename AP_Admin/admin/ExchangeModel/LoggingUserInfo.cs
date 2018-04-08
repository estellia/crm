using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.ExchangeModel.SSO
{
    [XmlRoot("data")]
    public class LoggingUserInfo
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        [XmlElement("customer_id")]
        public string CustomerID;

        /// <summary>
        /// 客户编码
        /// </summary>
        [XmlElement("customer_code")]
        public string CustomerCode;

        /// <summary>
        /// 客户名称
        /// </summary>
        [XmlElement("customer_name")]
        public string CustomerName;

        /// <summary>
        /// 数据库连接串
        /// </summary>
        [XmlElement("connection_string")]
        public string ConnectionString;

        /// <summary>
        /// 用户ID
        /// </summary>
        [XmlElement("user_id")]
        public string UserID;

        /// <summary>
        /// 用户姓名
        /// </summary>
        [XmlElement("user_name")]
        public string UserName;
    }
}
