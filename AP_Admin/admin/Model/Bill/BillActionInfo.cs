using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// ���Ĳ���
    /// </summary>
    [Serializable]
    public class BillActionInfo
    {
        public BillActionInfo()
            : base()
        {
            BillKind = new BillKindInfo();
        }

        /// <summary>
        /// ����������
        /// </summary>
        [XmlElement("bill_kind")]
        public BillKindInfo BillKind
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("bill_action_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("bill_action_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("bill_action_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// �½���־
        /// </summary>
        [XmlElement("create_flag")]
        public int CreateFlag
        { get; set; }

        /// <summary>
        /// �޸ı�־
        /// </summary>
        [XmlElement("modify_flag")]
        public int ModifyFlag
        { get; set; }

        /// <summary>
        /// ɾ����־
        /// </summary>
        [XmlElement("delete_flag")]
        public int DeleteFlag
        { get; set; }

        /// <summary>
        /// ��˱�־
        /// </summary>
        [XmlElement("approve_flag")]
        public int ApproveFlag
        { get; set; }


        /// <summary>
        /// �˻ر�־
        /// </summary>
        [XmlElement("reject_flag")]
        public int RejectFlag
        { get; set; }

        /// <summary>
        /// ������־
        /// </summary>
        [XmlElement("reserve_flag")]
        public int ReserveFlag
        { get; set; }

    }
}
