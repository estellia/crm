using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Initial
{
    /// <summary>
    /// 应用系统
    /// </summary>
    [Serializable]
    public class InitialInfo
    {
        /// <summary>
        /// 客户信息
        /// </summary>
        public Customer.CustomerInfo customerInfo { get; set; }

        /// <summary>
        /// 客户下的用户信息
        /// </summary>
        public Customer.CustomerUserInfo customerUserInfo { get; set; }
        /// <summary>
        /// 客户下的门店信息
        /// </summary>
        public Customer.CustomerShopInfo customerShopInfo { get; set; }
        /// <summary>
        /// 仓库信息
        /// </summary>
        public Customer.WarehouseInfo warehouseInfo { get; set; }
        /// <summary>
        /// 客户下的门店中的POS终端信息
        /// </summary>
        public Customer.PosInfo posInfo { get; set; }
    }
}
