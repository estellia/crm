using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Model
{
    /// <summary>
    /// sku属性集合
    /// </summary>
    [Serializable]
    public class SkuPropInfo
    {
        /// <summary>
        /// sku与属性关系标识
        /// </summary>
        public string sku_prop_id { get; set; }
        /// <summary>
        /// 属性标识
        /// </summary>
        public string prop_id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
        /// <summary>
        /// 属性号码
        /// </summary>
        public string prop_code { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string prop_name { get; set; }
        /// <summary>
        /// 属性明细的控制标识
        /// </summary>
        public string prop_input_flag { get; set; }
    }
}
