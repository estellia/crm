using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// �ͻ��µ��ŵ���Ϣ
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
        /// �����ͻ�
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
        /// ����
        /// </summary>
        [XmlElement("unit_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("unit_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("unit_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [XmlElement("unit_name_short")]
        public string ShortName
        { get; set; }

        /// <summary>
        /// ʡ
        /// </summary>
        [XmlElement("unit_province")]
        public string Province
        { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        [XmlElement("unit_city")]
        public string City
        { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        [XmlElement("unit_country")]
        public string Country
        { get; set; }


        /// <summary>
        /// ��ַ
        /// </summary>
        [XmlElement("unit_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// �ʱ�
        /// </summary>
        [XmlElement("unit_post_code")]
        public string PostCode
        { get; set; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        [XmlElement("unit_contact")]
        public string Contact
        { get; set; }

        /// <summary>
        /// �绰
        /// </summary>
        [XmlElement("unit_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("unit_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("unit_email")]
        public string Email
        { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("unit_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        [XmlElement("unit_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        [XmlElement("unit_status_desc")]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("longitude")]
        public string longitude
        { get; set; }

        /// <summary>
        /// γ��
        /// </summary>
        [XmlElement("dimension")]
        public string dimension
        { get; set; }

        /// <summary>
        /// ��ͷ��1
        /// </summary>
        [XmlElement("shop_url1")]
        public string shop_url1
        { get; set; }

        /// <summary>
        /// ��ͷ��2
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
        /// �ŵ��б�
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
        /// �޸���(��)
        /// </summary>
        [XmlElement("modify_user_id")]
        public string ModifyUserId
        { get; set; }

        /// <summary>
        /// �޸�ʱ��(��)
        /// </summary>
        [XmlElement("modify_time")]
        public string ModifyTime
        { get; set; }
        /// <summary>
        /// ��Ӫ��ID
        /// </summary>
        [XmlElement("UnitId2")]
        public string UnitId
        { get; set; }
        /// <summary>
        /// ��Ӫ��code
        /// </summary>
        [XmlElement("unit_code2")]
        public string unit_code
        { get; set; }
        /// <summary>
        /// ��Ӫ������
        /// </summary>
        [XmlElement("unit_name2")]
        public string unit_name
        { get; set; }
    }
}
