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
        /// 系统管理员角色的编码
        /// </summary>
        public static string CODE_ADMIN = "admin";

        public RoleInfo()
            : base()
        {
            Application = new AppInfo();
            RoleMenus = new List<RoleMenuInfo>();
        }

        /// <summary>
        /// 应用系统
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
        /// 编码
        /// </summary>
        [XmlElement("role_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("role_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("role_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 是否系统保留
        /// </summary>
        [XmlElement("is_system")]
        public int IsSystem
        { get; set; }

        /// <summary>
        /// 是否系统保留
        /// </summary>
        [XmlIgnore()]
        public int IsSystemDescription
        { get; set; }

        /// <summary>
        /// 角色对应的菜单列表
        /// </summary>
        [XmlArray("role_menus")]
        [XmlArrayItem("role_menu")]
        public IList<RoleMenuInfo> RoleMenus
        { get; set; }
    }
}
