using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单类型
    /// </summary>
    [Serializable]
    public class BillKindInfo
    {
        /// <summary>
        /// 菜单表单的编码
        /// </summary>
        public static string CODE_MENU = "menu";

        /// <summary>
        /// 客户表单的编码
        /// </summary>
        public static string CODE_CUSTOMER = "customer";
        /// <summary>
        /// 客户下的用户表单的编码
        /// </summary>
        public static string CODE_CUSTOMER_USER = "customer_user";
        /// <summary>
        /// 客户下的门店表单的编码
        /// </summary>
        public static string CODE_CUSTOMER_SHOP = "customer_shop";
        /// <summary>
        /// 客户下的终端表单的编码
        /// </summary>
        public static string CODE_CUSTOMER_TERMINAL = "customer_terminal";

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("bill_kind_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("bill_kind_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("bill_kind_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("bill_kind_name_en")]
        public string EnglishName
        { get; set; }

    }
}
