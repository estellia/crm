using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Right
{
    [Serializable]
    public class RoleInfo : Base.ObjectOperateInfo
    {
        /// <summary>
        /// ϵͳ����Ա��ɫ�ı���
        /// </summary>
        public static string CODE_ADMIN = "admin";

        public RoleInfo()
            : base()
        {
            Application = new AppInfo();
            RoleMenus = new List<RoleMenuInfo>();
        }

        /// <summary>
        /// Ӧ��ϵͳ
        /// </summary>
        [XmlElement("app")]
        public AppInfo Application
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("role_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("role_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("role_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("role_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// �Ƿ�ϵͳ����
        /// </summary>
        [XmlElement("is_system")]
        public int IsSystem
        { get; set; }

        /// <summary>
        /// �Ƿ�ϵͳ����
        /// </summary>
        [XmlIgnore()]
        public int IsSystemDescription
        { get; set; }

        /// <summary>
        /// ��ɫ��Ӧ�Ĳ˵��б�
        /// </summary>
        [XmlArray("role_menus")]
        [XmlArrayItem("role_menu")]
        public IList<RoleMenuInfo> RoleMenus
        { get; set; }
    }
}
