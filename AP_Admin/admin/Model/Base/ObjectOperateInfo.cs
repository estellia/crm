using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    /// <summary>
    /// 对象的操作信息
    /// </summary>
    [Serializable]
    public class ObjectOperateInfo : ObjectModifyInfo
    {

        public ObjectOperateInfo()
        {
            Creater = new UserOperateInfo();
        }

        /// <summary>
        /// 对象的创建者
        /// </summary>
        [XmlIgnore()]
        public UserOperateInfo Creater
        { get; set; }

        /// <summary>
        /// 对象的创建时间
        /// </summary>
        [XmlIgnore()]
        public DateTime CreateTime
        { get; set; }
    }
}
