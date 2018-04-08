using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// ×ÖµäµÄÃ÷Ï¸
    /// </summary>
    [Serializable]
    public class DictionaryDetailInfo : ObjectOperateInfo
    {
        public DictionaryDetailInfo()
            : this(new DictionaryInfo())
        { }

        public DictionaryDetailInfo(DictionaryInfo dic)
            : base()
        {
            this.Dictionary = dic;
        }

        /// <summary>
        /// ×Öµä
        /// </summary>
        public DictionaryInfo Dictionary
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("dic_detail_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ±àÂë
        /// </summary>
        [XmlElement("dd_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// Ãû³Æ
        /// </summary>
        [XmlElement("dd_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ó¢ÎÄÃû³Æ
        /// </summary>
        [XmlElement("dd_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ±¸×¢
        /// </summary>
        [XmlElement("dd_memo")]
        public string Memo
        { get; set; }
    }
}
