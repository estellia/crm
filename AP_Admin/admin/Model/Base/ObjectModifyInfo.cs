using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// 对象的修改信息
    /// </summary>
    public class ObjectModifyInfo : SystemOperateInfo
    {
        public ObjectModifyInfo()
            : base()
        {
            LastEditor = new UserOperateInfo();
        }
        /// <summary>
        /// 对象的最后编辑者
        /// </summary>
        [XmlIgnore()]
        public UserOperateInfo LastEditor
        { get; set; }

        /// <summary>
        /// 对象的最后编辑时间
        /// </summary>
        [XmlIgnore()]
        public DateTime LastEditTime
        { get; set; }
    }
}
