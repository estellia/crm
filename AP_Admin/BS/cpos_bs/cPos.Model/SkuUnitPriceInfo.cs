using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Model
{
    /// <summary>
    /// sku 单位价格
    /// </summary>
    public class SkuUnitPriceInfo
    {
        /// <summary>
        /// sku单位价格标识
        /// </summary>
        public string sku_unit_price_id { get; set; }
        /// <summary>
        /// sku标识
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 组织标识，组织为空，则为所有
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public string item_price_type_id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// sku单位价格集合
        /// </summary>
        public IList<SkuUnitPriceInfo> SkuUnitPriceInfoList { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
    }
}
