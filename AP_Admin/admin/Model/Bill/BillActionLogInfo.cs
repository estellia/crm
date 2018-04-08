using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// ���Ĳ�����ʷ
    /// </summary>
    [Serializable]
    public class BillActionLogInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("bill_action_log_id")]
        public int ID
        { get; set; }

        /// <summary>
        /// ��ID
        /// </summary>
        [XmlElement("bill_id")]
        public string BillID
        { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [XmlElement("bill_action_id")]
        public string BillActionID
        { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        [XmlElement("bill_action_desc")]
        public string BillActionDescription
        { get; set; }

        /// <summary>
        /// ǰ��״̬
        /// </summary>
        [XmlElement("pre_bill_status_id")]
        public string PreBillStatusID
        { get; set; }

        /// <summary>
        /// ǰ��״̬����
        /// </summary>
        [XmlElement("pre_bill_status_desc")]
        public string PreBillStatusDescription
        { get; set; }

        /// <summary>
        /// ��ǰ״̬
        /// </summary>
        [XmlElement("cur_bill_status_id")]
        public string CurBillStatusID
        { get; set; }

        /// <summary>
        /// ��ǰ״̬����
        /// </summary>
        [XmlElement("cur_bill_status_desc")]
        public string CurBillStatusDescription
        { get; set; }

        /// <summary>
        /// �����û�
        /// </summary>
        [XmlElement("action_user_id")]
        public string ActionUserID
        { get; set; }

        /// <summary>
        /// �����û�����
        /// </summary>
        [XmlElement("action_user_name")]
        public string ActionUserName
        { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [XmlElement("action_time")]
        public DateTime ActionTime
        { get; set; }

        /// <summary>
        /// ����ע��
        /// </summary>
        [XmlElement("action_comment")]
        public string ActionComment
        { get; set; }

    }
}
