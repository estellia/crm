using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model.Pos
{
    /// <summary>
    /// �ֿ���Ϣ
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
        /// ����
        /// </summary>
        [XmlElement("wh_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("wh_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("wh_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// ��ַ
        /// </summary>
        [XmlElement("wh_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        [XmlElement("wh_contacter")]
        public string Contacter
        { get; set; }

        /// <summary>
        /// �绰
        /// </summary>
        [XmlElement("wh_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("wh_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        [XmlElement("wh_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// �Ƿ�ȱʡ�ֿ�(1:�ǣ�0-��
        /// </summary>
        [XmlElement("is_default")]
        public int IsDefault
        { get; set; }

        /// <summary>
        /// �Ƿ�ȱʡ�ֿ�����
        /// </summary>
        [XmlIgnore()]
        public string IsDefaultDescription
        { get; set; }


        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("wh_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// ������λ
        /// </summary>
        [XmlIgnore()]
        public UnitInfo Unit
        { get; set; }
    }
}
