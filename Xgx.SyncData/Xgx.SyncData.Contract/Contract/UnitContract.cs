using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 组织架构单元(门店或总部)消息契约
    /// 对应正念数据表:t_unit,t_type,t_city,t_unit_relation
    /// </summary>
    public class UnitContract : IXgxToZmind, IZmindToXgx
    {
        //增删改标志，not null
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表t_unit的unit_id字段
        //对应数据库字段约束：nvarchar(50),pk,32位GUID
        public string UnitId { get; set; }
        //名称：编码
        //对应数据库字段：表t_unit的unit_code字段
        //对应数据库字段约束：nvarchar(50),not null
        public string UnitCode { get; set; }
        //名称：名称
        //对应数据库字段：表t_unit的unit_name字段
        //对应数据库字段约束：nvarchar(150),not null
        public string UnitName { get; set; }
        //名称：类型
        //对应数据库字段：表t_type的type_code字段
        //对应数据库字段约束：nvarchar(50),有效值{总部，区域，门店},not null
        public string TypeCode { get; set; }
        //名称：父组织标识
        //对应数据库字段：表t_unit_relation的src_unit_id字段
        //对应数据库字段约束：nvarchar(50),null,32位GUID，有效值:本契约UnitId的所有已存在值
        public string ParentUnitId { get; set; }
        //名称：英文名称
        //对应数据库字段：表t_unit的unit_name_en字段
        //对应数据库字段约束：nvarchar(150)，null
        public string UnitNameEn { get; set; }
        //名称：简称
        //对应数据库字段：表t_unit的unit_name_short字段
        //对应数据库字段约束：nvarchar(50)，null
        public string UnitNameShort { get; set; }
        //名称：地址的省(市)
        //对应数据库字段：表t_city的city1_name字段
        //对应数据库字段约束：nvarchar(50)，City1Name City2Name City3Name一起null或者not null
        public string City1Name { get; set; }
        //名称：地址的市
        //对应数据库字段：表t_city的city2_name字段
        //对应数据库字段约束：nvarchar(50)，City1Name City2Name City3Name一起null或者not null
        public string City2Name { get; set; }
        //名称：地址的县(区)
        //对应数据库字段：表t_city的city3_name字段
        //对应数据库字段约束：nvarchar(50)，City1Name City2Name City3Name一起null或者not null
        public string City3Name { get; set; }
        //名称：详细地址
        //对应数据库字段：表t_unit的unit_address字段
        //对应数据库字段约束：nvarchar(250)，null
        public string UnitAddress { get; set; }
        //名称：联系人
        //对应数据库字段：表t_unit的unit_contact字段
        //对应数据库字段约束：nvarchar(max)，null
        public string UnitContact { get; set; }
        //名称：电话
        //对应数据库字段：表t_unit的unit_tel字段
        //对应数据库字段约束：nvarchar(50)，null
        public string UnitTel { get; set; }
        //名称：传真
        //对应数据库字段：表t_unit的unit_fax字段
        //对应数据库字段约束：nvarchar(50)，null
        public string UnitFax { get; set; }
        //名称：邮箱
        //对应数据库字段：表t_unit的unit_email字段
        //对应数据库字段约束：nvarchar(150)，null
        public string UnitEmail { get; set; }
        //名称：邮编
        //对应数据库字段：表t_unit的unit_postcode字段
        //对应数据库字段约束：nvarchar(50)，null
        public string UnitPostcode { get; set; }
        //名称：备注
        //对应数据库字段：表t_unit的unit_remark字段
        //对应数据库字段约束：nvarchar(500)，null
        public string UnitRemark { get; set; }
        //名称：创建时间
        //对应数据库字段：表t_unit的create_time字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        public DateTime? CreateTime { get; set; }
        //名称：修改时间
        //对应数据库字段：表t_unit的modify_time字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        public DateTime? ModifyTime { get; set; }
        //名称：营销类型
        //对应数据库字段：表t_unit的StoreType字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{NapaStores(加盟店),DirectStore(直营店)}
        public string StoreType { get; set; }
        //名称：经度
        //对应数据库字段约束：nvarchar(30)，null
        public string Longitude { get; set; }
        //名称：纬度
        //对应数据库字段约束：nvarchar(30)，null
        public string Latitude { get; set; }
    }
}
