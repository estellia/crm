using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model
{
    /// <summary>
    /// 品牌客户
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class BrandCustomerInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string brand_customer_id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string brand_customer_code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string brand_customer_name { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string brand_customer_eng { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string brand_customer_address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string brand_customer_post { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string brand_customer_contacter { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string brand_customer_tel { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string brand_customer_email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_name { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        [XmlIgnore()]
        public IList<BrandCustomerInfo> List { get; set; }
    }
}
