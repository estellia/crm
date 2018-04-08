using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract.Contract
{
    /// <summary>
    /// 商品库存和销量
    /// </summary>
    public class ItemDetailAmountContract : IXgxToZmind
    {
        /// <summary>
        /// 增删改标志,not null
        /// </summary>
        public OptEnum Operation { get; set; }
        /// <summary>
        /// 商品明细编号,pk
        /// </summary>
        public string ItemDetailId { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public decimal? Inventory { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public decimal? SalesVolume { get; set; }
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
