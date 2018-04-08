using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 商品明细
    /// </summary>
    public class ItemDetail
    {
        /// <summary>
        /// 商品明细编号,pk
        /// </summary>
        public string ItemDetailId { get; set; }
        /// <summary>
        /// Sku值编号列表, List最大长度5, SkuValue null,长度和顺序与SkuNameIdList一致
        /// </summary>
        public List<string> SkuValueIdList { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal? OriginalPrice { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal? RetailPrice { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
