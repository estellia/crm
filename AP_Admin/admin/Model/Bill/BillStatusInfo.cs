using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// ����״̬
    /// </summary>
    [Serializable]
    public class BillStatusInfo : Base.ObjectOperateInfo
    {
        public BillStatusInfo()
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
        [XmlElement("bill_status_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("bill_status_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("bill_status_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ��ʼ��־
        /// </summary>
        [XmlElement("begin_flag")]
        public int BeginFlag
        { get; set; }

        /// <summary>
        /// ������־
        /// </summary>
        [XmlElement("end_flag")]
        public int EndFlag
        { get; set; }

        /// <summary>
        /// ɾ����־
        /// </summary>
        [XmlElement("delete_flag")]
        public int DeleteFlag
        { get; set; }

        /// <summary>
        /// ������־
        /// </summary>
        [XmlElement("reserve_flag")]
        public int ReserveFlag
        { get; set; }
    }
}
