using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// 操作者信息
    /// </summary>
    [Serializable]
    public class UserOperateInfo
    {

        /// <summary>
        /// ID
        /// </summary>
        [XmlIgnore()]
        public string ID
        { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [XmlIgnore()]
        public string Name
        { get; set; }
    }
}
