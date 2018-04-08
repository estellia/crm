using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 商品SKU值
    /// </summary>
    public class SkuValue
    {
        /// <summary>
        /// 编号
        ///对应数据库字段：表 T_Prop 的 prop_id 字段
        ///对应数据库字段约束：nvarchar(50),pk,32位
        /// </summary>
        public string SkuValueId { get; set; }
        /// <summary>
        /// 编码
        /// 对应数据库字段：表 T_Prop 的 prop_code 字段
        /// 对应数据库字段约束：nvarchar(50),not null
        /// </summary>
        public string SkuValueCode { get; set; }

        /// <summary>
        /// 名称
        /// 对应数据库字段：表 T_Prop 的 prop_name 字段
        /// 对应数据库字段约束：nvarchar(250),not null
        /// </summary>
        public string SkuValueName { get; set; }

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
    }
}
