using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// �ֵ�
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
        /// ����
        /// </summary>
        [XmlElement("dic_table")]
        public string Table
        { get; set; }

        /// <summary>
        /// �ֶ���
        /// </summary>
        [XmlElement("dic_field")]
        public string Field
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("dic_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("dic_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("dic_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("dic_memo")]
        public string Memo
        { get; set; }
    }
}
