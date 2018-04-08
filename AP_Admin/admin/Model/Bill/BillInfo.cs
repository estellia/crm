using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单信息
    /// </summary>
    [Serializable]
    public class BillInfo
    {
        public BillInfo()
        {
            BillKind = new BillKindInfo();
            BillStatus = new BillStatusInfo();
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("bill_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 表单种类
        /// </summary>
        [XmlElement("bill_kind_id")]
        public BillKindInfo BillKind
        { get; set; }

        /// <summary>
        /// 表单编码
        /// </summary>
        [XmlElement("bill_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        [XmlElement("bill_status_id")]
        public BillStatusInfo BillStatus
        { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [XmlElement("bill_money")]
        public decimal Money
        { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [XmlElement("batch_no")]
        public string BatchNo
        { get; set; }

    }
}
