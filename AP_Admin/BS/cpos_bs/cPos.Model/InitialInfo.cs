using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace cPos.Model
{
    /// <summary>
    /// 终端初始化
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class InitialInfo
    {
        //客户信息
        public CustomerInfo customerInfo { get; set; }

        //门店信息
        public UnitInfo unitInfo { get; set; }

        //仓库信息
        public Pos.WarehouseInfo warehouseInfo { get; set; }

        //终端信息
        public Pos.PosUnitInfo posUnitInfo { get; set; }

        //用户信息
        public User.UserInfo userInfo { get; set; }
    }
}
