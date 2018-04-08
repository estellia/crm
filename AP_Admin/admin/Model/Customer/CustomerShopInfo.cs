using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// 客户下的门店信息
    /// </summary>
    [Serializable]
    [XmlRoot("data")]
    public class CustomerShopInfo : Base.SystemOperateInfo
    {
        public CustomerShopInfo()
            : this(new CustomerInfo())
        { }

        public CustomerShopInfo(CustomerInfo customer)
            : base()
        {
            this.Customer = customer;
        }

        /// <summary>
        /// 所属客户
        /// </summary>
        [XmlIgnore()]
        public CustomerInfo Customer
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("unit_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("unit_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("unit_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("unit_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [XmlElement("unit_name_short")]
        public string ShortName
        { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [XmlElement("unit_province")]
        public string Province
        { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [XmlElement("unit_city")]
        public string City
        { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        [XmlElement("unit_country")]
        public string Country
        { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("unit_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("unit_post_code")]
        public string PostCode
        { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("unit_contact")]
        public string Contact
        { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("unit_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("unit_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [XmlElement("unit_email")]
        public string Email
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("unit_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("unit_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlElement("unit_status_desc")]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        [XmlElement("longitude")]
        public string longitude
        { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [XmlElement("dimension")]
        public string dimension
        { get; set; }

        /// <summary>
        /// 门头照1
        /// </summary>
        [XmlElement("shop_url1")]
        public string shop_url1
        { get; set; }

        /// <summary>
        /// 门头照2
        /// </summary>
        [XmlElement("shop_url2")]
        public string shop_url2
        { get; set; }

        /// <summary>
        /// customer_id
        /// </summary>
        [XmlElement("customer_id")]
        public string customer_id
        { get; set; }

        /// <summary>
        /// 门店列表
        /// </summary>
        [XmlIgnore()]
        public IList<CustomerShopInfo> CustomerShopList
        { get; set; }

        /// <summary>
        /// BatId
        /// </summary>
        [XmlElement("bat_id")]
        public string BatId
        { get; set; }

        /// <summary>
        /// 修改人(新)
        /// </summary>
        [XmlElement("modify_user_id")]
        public string ModifyUserId
        { get; set; }

        /// <summary>
        /// 修改时间(新)
        /// </summary>
        [XmlElement("modify_time")]
        public string ModifyTime
        { get; set; }
        /// <summary>
        /// 运营商ID
        /// </summary>
        [XmlElement("UnitId2")]
        public string UnitId
        { get; set; }
        /// <summary>
        /// 运营商code
        /// </summary>
        [XmlElement("unit_code2")]
        public string unit_code
        { get; set; }
        /// <summary>
        /// 运营商名称
        /// </summary>
        [XmlElement("unit_name2")]
        public string unit_name
        { get; set; }
    }
}
