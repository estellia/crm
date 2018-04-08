using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Base
{
    public class SystemOperateInfo
    {
        /// <summary>
        /// 对象的最后修改时间戳
        /// </summary>
        [XmlIgnore()]
        public DateTime LastSystemModifyStamp
        { get; set; }
    }
}
