using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 商品品类消息契约
    /// 对应正念数据表:T_Item_Category
    /// </summary>
    public class ItemCategoryContract : IXgxToZmind
    {

        //增删改标志，not null
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表T_Item_Category的item_category_id字段
        //对应数据库字段约束：nvarchar(50),pk,32位GUID
        public string ItemCategoryId { get; set; }
        //名称：编码
        //对应数据库字段：表T_Item_Category的item_category_code字段
        //对应数据库字段约束：nvarchar(50),not null
        public string ItemCategoryCode { get; set; }
        //名称：名称
        //对应数据库字段：表T_Item_Category的item_category_name字段
        //对应数据库字段约束：nvarchar(150),not null
        public string ItemCategoryName { get; set; }
        //名称：类型
        //对应数据库字段：表T_Item_Category的status字段
        //对应数据库字段约束：nvarchar(50)，null,有效值1：代表有效，0代表删除
        public string Status { get; set; }
        //名称：父组织标识
        //对应数据库字段：表T_Item_Category的parent_id字段
        //对应数据库字段约束：nvarchar(50),not null
        //如果是一级分类，父ID是--
        public string ParentId { get; set; }
        //对应数据库字段：表T_Item_Category的create_time字段
        //对应数据库字段约束：nvarchar(50), null
        public DateTime? CreateTime { get; set; }
        //  对应数据库字段：表T_Item_Category的modify_time字段
        //对应数据库字段约束：nvarchar(50), null
        public DateTime? ModifyTime { get; set; }
        //   对应数据库字段：表T_Item_Category的displayIndex字段
        //对应数据库字段约束：int，null,
        public int? DisplayIndex { get; set; }

    }
}
