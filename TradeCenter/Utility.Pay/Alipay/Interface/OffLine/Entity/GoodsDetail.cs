using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.Entity
{
    /// <summary>
    /// 商品明细信息
    /// </summary>
    public class GoodsDetail
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 收银台页面上，商品展示的超链接
        /// </summary>
        public string showUrl { get; set; }
        /// <summary>
        /// 商品数量。
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 商品描述信息。
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品单价。
        /// </summary>
        public string price { get; set; }
    }
}
