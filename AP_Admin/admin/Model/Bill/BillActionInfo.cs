using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Bill
{
    /// <summary>
    /// 表单的操作
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
        /// 所属表单类型
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
        /// 名称
        /// </summary>
        [XmlElement("bill_action_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("bill_action_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 新建标志
        /// </summary>
        [XmlElement("create_flag")]
        public int CreateFlag
        { get; set; }

        /// <summary>
        /// 修改标志
        /// </summary>
        [XmlElement("modify_flag")]
        public int ModifyFlag
        { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        [XmlElement("delete_flag")]
        public int DeleteFlag
        { get; set; }

        /// <summary>
        /// 审核标志
        /// </summary>
        [XmlElement("approve_flag")]
        public int ApproveFlag
        { get; set; }


        /// <summary>
        /// 退回标志
        /// </summary>
        [XmlElement("reject_flag")]
        public int RejectFlag
        { get; set; }

        /// <summary>
        /// 保留标志
        /// </summary>
        [XmlElement("reserve_flag")]
        public int ReserveFlag
        { get; set; }

    }
}
