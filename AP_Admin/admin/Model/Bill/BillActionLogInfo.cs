using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单的操作历史
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
        /// 表单ID
        /// </summary>
        [XmlElement("bill_id")]
        public string BillID
        { get; set; }

        /// <summary>
        /// 表单操作
        /// </summary>
        [XmlElement("bill_action_id")]
        public string BillActionID
        { get; set; }

        /// <summary>
        /// 表单操作描述
        /// </summary>
        [XmlElement("bill_action_desc")]
        public string BillActionDescription
        { get; set; }

        /// <summary>
        /// 前置状态
        /// </summary>
        [XmlElement("pre_bill_status_id")]
        public string PreBillStatusID
        { get; set; }

        /// <summary>
        /// 前置状态描述
        /// </summary>
        [XmlElement("pre_bill_status_desc")]
        public string PreBillStatusDescription
        { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        [XmlElement("cur_bill_status_id")]
        public string CurBillStatusID
        { get; set; }

        /// <summary>
        /// 当前状态描述
        /// </summary>
        [XmlElement("cur_bill_status_desc")]
        public string CurBillStatusDescription
        { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        [XmlElement("action_user_id")]
        public string ActionUserID
        { get; set; }

        /// <summary>
        /// 操作用户姓名
        /// </summary>
        [XmlElement("action_user_name")]
        public string ActionUserName
        { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [XmlElement("action_time")]
        public DateTime ActionTime
        { get; set; }

        /// <summary>
        /// 操作注释
        /// </summary>
        [XmlElement("action_comment")]
        public string ActionComment
        { get; set; }

    }
}
