using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 商品 Sku 契约
    /// 对应正念数据表: t_sku_prop, t_prop
    /// </summary>
    public class SkuContract : IXgxToZmind
    {
        /// <summary>
        /// 增删改标志，not null
        /// </summary>
        public OptEnum Operation { get; set; }

        /// <summary>
        /// 编号
        ///对应数据库字段：表 T_Prop 的 prop_id 字段
        ///对应数据库字段约束：nvarchar(50),pk,32位
        /// </summary>
        public string SkuId { get; set; }

        /// <summary>
        /// 编码
        /// 对应数据库字段：表 T_Prop 的 prop_code 字段
        /// 对应数据库字段约束：nvarchar(50),not null
        /// </summary>
        public string SkuCode { get; set; }

        /// <summary>
        /// 名称
        /// 对应数据库字段：表 T_Prop 的 prop_name 字段
        /// 对应数据库字段约束：nvarchar(250),not null
        /// </summary>
        public string SkuName { get; set; }
        /// <summary> 
        /// 创建时间
        /// 对应数据库字段：表 T_Prop 的 create_time 字段
        /// 对应数据库字段约束：nvarchar(50),null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// 对应数据库字段：表 T_Prop 的 modify_time 字段
        /// 对应数据库字段约束：nvarchar(50),null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Sku值列表
        /// </summary>
        public List<SkuValue> ValueList { get; set; }
    }
}
