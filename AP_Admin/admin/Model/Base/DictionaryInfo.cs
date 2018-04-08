using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// 字典
    /// </summary>
    [Serializable]
    public class DictionaryInfo : ObjectOperateInfo
    {
        public DictionaryInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("dic_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [XmlElement("dic_table")]
        public string Table
        { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [XmlElement("dic_field")]
        public string Field
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("dic_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("dic_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("dic_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("dic_memo")]
        public string Memo
        { get; set; }
    }
}
