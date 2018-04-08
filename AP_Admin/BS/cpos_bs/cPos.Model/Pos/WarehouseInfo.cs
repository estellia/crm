using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model.Pos
{
    /// <summary>
    /// 仓库信息
    /// </summary>
    [Serializable]
    public class WarehouseInfo : ObjectOperateInfo
    {
        public WarehouseInfo()
            : base()
        {
            this.Unit = new UnitInfo();
        }

        [XmlElement("warehouse_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("wh_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("wh_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("wh_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("wh_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("wh_contacter")]
        public string Contacter
        { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("wh_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("wh_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("wh_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 是否缺省仓库(1:是；0-否）
        /// </summary>
        [XmlElement("is_default")]
        public int IsDefault
        { get; set; }

        /// <summary>
        /// 是否缺省仓库描述
        /// </summary>
        [XmlIgnore()]
        public string IsDefaultDescription
        { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("wh_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        [XmlIgnore()]
        public UnitInfo Unit
        { get; set; }
    }
}
