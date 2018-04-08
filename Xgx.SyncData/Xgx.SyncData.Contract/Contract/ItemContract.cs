using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 商品消息契约
    /// </summary>
    public class ItemContract : IXgxToZmind
    {
        /// <summary>
        /// 增删改标志,not null
        /// </summary>
        public OptEnum Operation { get; set; }
        /// <summary>
        /// 商品标识,not null
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 商品分类标识,not null
        /// </summary>
        public string ItemCategoryId { get; set; }
        /// <summary>
        /// 商品编码,not null
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 商品名称,not null
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 商品英文名称,null
        /// </summary>
        public string ItemNameEn { get; set; }
        /// <summary>
        /// 商品简称,null
        /// </summary>
        public string ItemNameShort { get; set; }
        /// <summary>
        /// 拼音,null
        /// </summary>
        public string Pyzjm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ItemRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }        
        /// <summary>
        /// Sku名称编号列表, 最大长度5
        /// </summary>
        public List<string> SkuNameIdList { get; set; }
        /// <summary>
        /// 商品明细
        /// </summary>
        public List<ItemDetail> ItemDetailList { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public List<EnumDelivery> DeliveryList { get; set; }
    }
}
