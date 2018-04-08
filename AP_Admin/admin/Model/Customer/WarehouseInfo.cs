using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// 仓库信息
    /// </summary>
    [Serializable]
    public class WarehouseInfo
    {
        public string warehouse_id { get; set; }

        public string wh_code { get; set; }

        public string wh_name { get; set; }

        public string wh_name_en { get; set; }

        public string wh_address { get; set; }

        public string wh_contacter { get; set; }

        public string wh_tel { get; set; }

        public string wh_fax { get; set; }

        public string wh_status { get; set; }

        public string wh_remark { get; set; }

        public string is_default { get; set; }

        public string create_time { get; set; }

        public string create_user_id { get; set; }

        public string modify_time { get; set; }

        public string modify_user_id { get; set; }
    }
}
