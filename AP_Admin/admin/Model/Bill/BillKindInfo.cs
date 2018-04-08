using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public class BillKindInfo
    {
        /// <summary>
        /// �˵����ı���
        /// </summary>
        public static string CODE_MENU = "menu";

        /// <summary>
        /// �ͻ����ı���
        /// </summary>
        public static string CODE_CUSTOMER = "customer";
        /// <summary>
        /// �ͻ��µ��û����ı���
        /// </summary>
        public static string CODE_CUSTOMER_USER = "customer_user";
        /// <summary>
        /// �ͻ��µ��ŵ���ı���
        /// </summary>
        public static string CODE_CUSTOMER_SHOP = "customer_shop";
        /// <summary>
        /// �ͻ��µ��ն˱��ı���
        /// </summary>
        public static string CODE_CUSTOMER_TERMINAL = "customer_terminal";

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("bill_kind_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("bill_kind_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("bill_kind_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("bill_kind_name_en")]
        public string EnglishName
        { get; set; }

    }
}
