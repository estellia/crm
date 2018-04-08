using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Right
{
    /// <summary>
    /// 应用系统
    /// </summary>
    [Serializable]
    public class AppInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        /// <summary>
        /// 管理平台的应用系统的编码
        /// </summary>
        public static string CODE_ADMIN_PLATFORM = "ap";

        public AppInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("def_app_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("def_app_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("def_app_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("def_app_name_en")]
        public string EnglishName
        { get; set; }
    }
    /// <summary>
    /// 行业版本
    /// </summary>
    [Serializable]
    public class VersionInfo : cPos.Admin.Model.Base.ObjectOperateInfo
    {
        /// <summary>
        /// 管理平台的应用系统的编码
        /// </summary>
        public static string CODE_ADMIN_PLATFORM = "ap";

        public VersionInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("VocaVerMappingID")]
        public string ID
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("VersionName")]
        public string Name
        { get; set; }

    }
}
