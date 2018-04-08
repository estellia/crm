using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model.Exchange
{
    /// <summary>
    /// 通告与单位的关系
    /// </summary>
    public class AnnounceUnitInfo
    {
        public AnnounceUnitInfo(AnnounceInfo announce)
            : this(announce, new UnitInfo())
        { }

        public AnnounceUnitInfo(cPos.Model.UnitInfo unit)
            : this(new AnnounceInfo(), unit)
        { }

        public AnnounceUnitInfo(AnnounceInfo announce, cPos.Model.UnitInfo unit)
        {
            this.Announce = announce;
            this.Unit = unit;
        }

        public AnnounceUnitInfo()
            : this(new AnnounceInfo(), new UnitInfo())
        { }

        /// <summary>
        /// 业务通告信息
        /// </summary>
        [XmlIgnore()]
        public AnnounceInfo Announce
        { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        [XmlIgnore()]
        public cPos.Model.UnitInfo Unit
        { get; set; }
    }
}
