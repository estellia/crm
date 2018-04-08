using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// 客户下的登录用户
    /// </summary>
    [Serializable]
    [XmlRoot("data")]
    public class CustomerUserInfo : Base.SystemOperateInfo
    {
        public CustomerUserInfo()
            : this(new CustomerInfo())
        { }

        public CustomerUserInfo(CustomerInfo customer)
            : base()
        {
            this.Customer = customer;
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("user_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        [XmlElement("user_account")]
        public string Account
        { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        [XmlElement("user_name")]
        public string Name
        { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [XmlElement("user_pwd")]
        public string Password
        { get; set; }
        
        /// <summary>
        /// 状态(编码)
        /// </summary>
        [XmlElement("user_status")]
        public int Status
        { get; set; }
        
        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlElement("user_status_desc")]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [XmlElement("user_expired_date")]
        public string ExpiredDate
        { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        [XmlIgnore()]
        public CustomerInfo Customer
        { get; set; }

        /// <summary>
        /// 所属客户集合
        /// </summary>
        [XmlIgnore()]
        //[XmlElement("customer_list")]
        public IList<CustomerInfo> CustomerList
        { get; set; }

        /// <summary>
        /// 所属门店集合
        /// </summary>
        [XmlIgnore()]
        //[XmlElement("customer_shop_list")]
        public IList<CustomerShopInfo> CustomerShopList
        { get; set; }
        /// <summary>
        /// 运营商ID
        /// </summary>
        [XmlElement("UnitId")]
        public string UnitId
        { get; set; }
        /// <summary>
        /// 运营商code
        /// </summary>
        [XmlElement("unit_code")]
        public string unit_code
        { get; set; }
        /// <summary>
        /// 运营商名称
        /// </summary>
        [XmlElement("unit_name")]
        public string unit_name
        { get; set; }
    }
}
