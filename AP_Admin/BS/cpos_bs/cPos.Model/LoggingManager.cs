using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model
{
    /// <summary>
    /// 管理平台用户验证通过传递的信息
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class LoggingManager
    {
        /// <summary>
        /// 客户标识
        /// </summary>
        [XmlElement("customer_id")] 
        public string Customer_Id { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        [XmlElement("customer_code")]
        public string Customer_Code { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [XmlElement("customer_name")]
        public string Customer_Name { get; set; }

        /// <summary>
        /// 登录用户标识
        /// </summary>
        [XmlElement("user_id")]
        public string User_Id { get; set; }

        /// <summary>
        /// 登录用户名称
        /// </summary>
        [XmlElement("user_name")]
        public string User_Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [XmlElement("connection_string")]
        public string Connection_String { get; set; }
    }
}
