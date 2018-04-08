using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// �ֵ����ϸ
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
        /// �ֵ�
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
        /// ����
        /// </summary>
        [XmlElement("dd_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("dd_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("dd_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("dd_memo")]
        public string Memo
        { get; set; }
    }
}
