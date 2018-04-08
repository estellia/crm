using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Right
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Serializable]
    [XmlRoot("menu")]
    public class MenuInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        public MenuInfo()
            : base()
        {
        }

        /// <summary>
        /// 所属应用系统
        /// </summary>
        [XmlElement("reg_app_id")]
        public string ApplicationID
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("menu_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("menu_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("menu_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("menu_eng_name")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [XmlElement("menu_level")]
        public int Level
        { get; set; }

        /// <summary>
        ///url路径
        /// </summary>
        [XmlElement("url_path")]
        public string URLPath
        { get; set; }

        /// <summary>
        /// 图标路径
        /// </summary>
        [XmlElement("icon_path")]
        public string IconPath
        { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [XmlElement("display_index")]
        public int DisplayIndex
        { get; set; }

        /// <summary>
        /// 客户是否可见
        /// </summary>
        [XmlElement("user_flag")]
        public int CustomerVisible
        { get; set; }

        /// <summary>
        /// 客户是否可见描述
        /// </summary>
        [XmlIgnore()]
        public string CustomerVisibleDescription
        { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("status")]
        public int Status
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [XmlElement("parent_menu_id")]
        public string ParentMenuID
        { get; set; }

        /// <summary>
        /// JS访问时的路径
        /// </summary>
        [XmlIgnore()]
        public string URLPathWithID
        {
            get
            {
                if (Level == 3)
                {
                    if (!string.IsNullOrEmpty(URLPath) && URLPath.IndexOf("?") > 0)
                        return URLPath + "&cur_menu_id=" + ID;
                    else
                        return URLPath + "?cur_menu_id=" + ID;
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 商户ID
        /// </summary>
        [XmlIgnore()]
        public string CustomerID
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// 是否可访问
        /// </summary>
        [XmlElement("isCanAccess")]
        public int IsCanAccess
        { get; set; }

        /// <summary>
        /// 行业版本和菜单关系表ID
        /// </summary>
        [XmlElement("vvMappingMenuId")]
        public Guid vvMappingMenuId
        { get; set; }

    }
}
