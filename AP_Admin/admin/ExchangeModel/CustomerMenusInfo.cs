using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.ExchangeModel
{
    [XmlRoot("data")]
    [Serializable]
    public class CustomerMenusInfo
    {
        public CustomerMenusInfo()
        {
            Applications = new List<Model.Right.AppInfo>();
            Menus = new List<cPos.Admin.Model.Right.MenuInfo>();
        }

        [XmlArray("applications")]
        [XmlArrayItem("application")]
        public List<cPos.Admin.Model.Right.AppInfo> Applications
        { get; set; }

        [XmlArray("menus")]
        [XmlArrayItem("menu")]
        public List<cPos.Admin.Model.Right.MenuInfo> Menus
        { get; set; }
    }
}
