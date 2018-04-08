using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model
{
    /// <summary>
    /// 商品类
    /// </summary>
    [Serializable]
    public class ItemInfo
    {
        private string id;
        private string code;
        private string name;
        private string englishName;
        private string shortName;
        private string typeId;
        private string typeCode;
        private string typeDescription;
        private int isSku = 0;
        private int isBom = 0;
        private string barcode;
        private string status = "1";
        private string remark;

        //private IList<ItemPriceInfo> priceList = new List<ItemPriceInfo>();
        /// <summary>
        /// Id【保存必须】
        /// </summary>
        public string Item_Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 商品编码【保存必须】
        /// </summary>
        public string Item_Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 商品分类(Id)【保存必须】
        /// </summary>
        public string Item_Category_Id
        {
            get { return typeId; }
            set { typeId = value; }
        }
        /// <summary>
        /// 商品分类(编码)
        /// </summary>
        public string Item_Category_Code
        {
            get { return typeCode; }
            set { typeCode = value; }
        }
        /// <summary>
        /// 商品分类(描述)
        /// </summary>
        public string Item_Category_Name
        {
            get { return typeDescription; }
            set { typeDescription = value; }
        }
        /// <summary>
        /// 商品中文名称【保存必须】
        /// </summary>
        public string Item_Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 商品英文名称
        /// </summary>
        public string Item_Name_En
        {
            get { return englishName; }
            set { englishName = value; }
        }
        /// <summary>
        /// 商品简称
        /// </summary>
        public string Item_Name_Short
        {
            get { return shortName; }
            set { shortName = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Item_Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 状态(1:有效)
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Status_Desc { get; set; }
        /// <summary>
        /// 是否是商品(1:是,0:分类)
        /// </summary>
        public int IsSku
        {
            get { return isSku; }
            set { isSku = value; }
        }
        /// <summary>
        /// 是否是混合商品(1:是)
        /// </summary>
        public int IsBom
        {
            get { return isBom; }
            set { isBom = value; }
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }
        //商品的价格列表
        //public IList<ItemPriceInfo> PriceList
        //{
        //    get { return priceList; } 
        //    set { priceList = value; }
        //}

        /// <summary>
        /// 下拉树中的显示名称
        /// </summary>
        public string DisplayName
        {
            get { return code + " - " + shortName; }
        }

        private string createTime;
        /// <summary>
        /// CreateTime
        /// </summary>
        public string Create_Time
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string createUserId;
        /// <summary>
        /// CreateUserId
        /// </summary>
        public string Create_User_Id
        {
            get { return createUserId; }
            set { createUserId = value; }
        }

        private string modifyTime;
        /// <summary>
        /// ModifyTime
        /// </summary>
        public string Modify_Time
        {
            get { return modifyTime; }
            set { modifyTime = value; }
        }

        private string modifyUserId;
        /// <summary>
        /// ModifyUserId
        /// </summary>
        public string Modify_User_Id
        {
            get { return modifyUserId; }
            set { modifyUserId = value; }
        }
        /// <summary>
        /// 拼音助记码
        /// </summary>
        public string Pyzjm { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_Name { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modify_User_Name { get; set; }

        /// <summary>
        /// 商品与属性关系集合
        /// </summary>
        [XmlIgnore()]
        public IList<ItemPropInfo> ItemPropList { get; set; }
        /// <summary>
        /// 商品价格集合
        /// </summary>
        [XmlIgnore()]
        public IList<ItemPriceInfo> ItemPriceList { get; set; }
        /// <summary>
        /// sku集合
        /// </summary>
        [XmlIgnore()]
        public IList<SkuInfo> SkuList { get; set; }
        /// <summary>
        /// 商品集合
        /// </summary>
        [XmlIgnore()]
        public IList<ItemInfo> ItemInfoList { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 是否赠品
        /// </summary>
        public int ifgifts	{get;set;}
        /// <summary>
        /// 是否常用商品
        /// </summary>
        public int ifoften	{ get; set; }
        /// <summary>
        /// 是否服务性商品
        /// </summary>
        public int ifservice { get; set; }
        /// <summary>
        /// 非国标商品
        /// </summary>
        public int isGB { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string data_from { get; set; }
        /// <summary>
        /// 显示次序
        /// </summary>
        public int display_index { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }

        /// <summary>
        /// 商品（条码）
        /// </summary>
        [XmlElement("item_info_by_barcode")]
        public ItemInfo ItemInfoByBarcode { get; set; }

        /// <summary>
        /// Sku（条码）
        /// </summary>
        [XmlElement("sku_info_by_barcode")]
        public SkuInfo SkuInfoByBarcode { get; set; }
        /// <summary>
        /// 图片超链接
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customerId { get; set; }
    }
}
