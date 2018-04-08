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
    public class CustomerInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        public CustomerInfo()
            : base()
        {
            
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("customer_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("customer_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("customer_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("customer_name_en")]
        public string EnglishName
        { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("customer_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("customer_post_code")]
        public string PostCode
        { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("customer_contacter")]
        public string Contacter
        { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("customer_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("customer_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// E-Mail
        /// </summary>
        [XmlElement("customer_email")]
        public string Email
        { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [XmlElement("customer_cell")]
        public string Cell
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("customer_memo")]
        public string Memo
        { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        [XmlElement("customer_start_date")]
        public string StartDate
        { get; set; }

        /// <summary>
        /// 状态(编码)
        /// </summary>
        [XmlElement("customer_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 连接信息
        /// </summary>
        [XmlIgnore()]
        public CustomerConnectInfo Connect
        { get; set; }

        /// <summary>
        /// 可操作菜单列表
        /// </summary>
        [XmlIgnore()]
        public IList<CustomerMenuInfo> Menus
        { get; set; }

        /// <summary>
        /// 客户列表
        /// </summary>
        [XmlIgnore()]
        public IList<CustomerInfo> CustomerList
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

        [XmlIgnore()]
        public string DataDeployId
        { get; set; }

        /// <summary>
        /// 是否同步到阿拉丁 Jermyn20140529
        /// </summary>
        [XmlIgnore()]
        public int IsALD
        { get; set; }

        /// <summary>
        /// 是否同步Pad Jermyn20140529
        /// </summary>
        [XmlIgnore()]
        public int IsPad
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
		/// <summary>
		/// 门店数量
		/// </summary>
		[XmlElement("Units")]
		public int Units
		{ get; set; }
		public string Units_zh
		{ get; set; }
	}
}
