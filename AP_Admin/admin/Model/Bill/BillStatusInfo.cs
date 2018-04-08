using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单的状态
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
        /// 所属表单类型
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
        /// 名称
        /// </summary>
        [XmlElement("bill_status_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("bill_status_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 起始标志
        /// </summary>
        [XmlElement("begin_flag")]
        public int BeginFlag
        { get; set; }

        /// <summary>
        /// 结束标志
        /// </summary>
        [XmlElement("end_flag")]
        public int EndFlag
        { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        [XmlElement("delete_flag")]
        public int DeleteFlag
        { get; set; }

        /// <summary>
        /// 保留标志
        /// </summary>
        [XmlElement("reserve_flag")]
        public int ReserveFlag
        { get; set; }
    }
}
