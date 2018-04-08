using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model
{
    /// <summary>
    /// 商品类别model
    /// </summary>
    public class ItemCategoryInfo
    {
        /// <summary>
        /// 商品类别标识【保存必须】
        /// </summary>
        public string Item_Category_Id {get;set;}
        /// <summary>
        /// 商品类别号码【保存必须】
        /// </summary>
        public string Item_Category_Code {get;set;}
        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string Item_Category_Name {get;set;}
        /// <summary>
        /// 拼音助记码【保存必须】
        /// </summary>
        public string Pyzjm {get;set;}
        /// <summary>
        /// 状态号码
        /// </summary>
        public string Status {get;set;}
        /// <summary>
        /// 父节点标识【保存必须】，顶层父节点-99
        /// </summary>
        public string Parent_Id {get;set;}
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string Create_User_Id {get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_Time {get;set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modify_User_Id {get;set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public string Modify_Time { get; set; }
        /// <summary>
        /// 父节点名称【显示】
        /// </summary>
        public string Parent_Name { get; set; }
        /// <summary>
        /// 创建人名称【显示】
        /// </summary>
        public string Create_User_Name { get; set; }
        /// <summary>
        /// 修改人名称【显示】
        /// </summary>
        public string Modify_User_Name { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Status_desc { get; set; }
        /// <summary>
        /// 查询结果集数量【显示】
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        ///  行号【显示】
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 商品类别集合
        /// </summary>
        public IList<ItemCategoryInfo> ItemCategoryInfoList { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? DisplayIndex { get; set; }

        /// <summary>
        /// 客户标识
        /// </summary>
        public string customerId { get; set; }
    }
}
