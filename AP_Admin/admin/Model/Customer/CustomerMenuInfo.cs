using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// 客户下的菜单
    /// </summary>
    public class CustomerMenuInfo
    {
        public CustomerMenuInfo()
            : this(new CustomerInfo(), new Right.MenuInfo())
        { }

        public CustomerMenuInfo(CustomerInfo customer)
            : this(customer, new Right.MenuInfo())
        { }

        public CustomerMenuInfo(Right.MenuInfo menu)
            : this(new CustomerInfo(), menu)
        { }

        public CustomerMenuInfo(CustomerInfo customer, Right.MenuInfo menu)
        {
            this.Customer = customer;
            this.Menu = menu;
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlIgnore()]
        public string ID
        { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        [XmlElement("customer")]
        public CustomerInfo Customer
        { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [XmlElement("menu")]
        public Right.MenuInfo Menu
        { get; set; }
    }
}
