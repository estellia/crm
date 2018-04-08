using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 内部用户(店长，店员，总部管理员)消息契约
    /// 对应正念数据表:t_user,t_unit,t_user_role,t_role
    /// </summary>
    public class UserContract : IXgxToZmind, IZmindToXgx
    {
        //增删改标志
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表t_user的user_id字段
        //对应数据库字段约束：nvarchar(50),pk,32位GUID
        public string UserId { get; set; }
        //名称：编号
        //对应数据库字段：表t_user的user_code字段
        //对应数据库字段约束：nvarchar(50),not null

        public string UserCode { get; set; }
        //名称：名称
        //对应数据库字段：表t_user的user_name字段
        //对应数据库字段约束：nvarchar(150),not null
        public string UserName { get; set; }
        //名称：手机
        //对应数据库字段：表t_user的user_telephone字段
        //对应数据库字段约束：nvarchar(150),null
        public string UserTelephone { get; set; }
        //名称：创建时间
        //对应数据库字段：表t_user的create_time字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        public DateTime? CreateTime { get; set; }
        //名称：修改时间
        //对应数据库字段：表t_user的modify_time字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{yyyy-MM-dd hh:mm:ss形式的日期}
        public DateTime? ModifyTime { get; set; }
        //名称：组织架构单元标识
        //对应数据库字段：表t_user_role的unit_id字段
        //对应数据库字段约束：nvarchar(50),32位GUID,fk-UnitContract的pk,not null
        public string UnitId { get; set; }
        //名称：角色编码
        //对应数据库字段：表t_role的role_code字段
        //对应数据库字段约束：nvarchar(50),not null,有效值{Admin(管理员),manager(后台店长),clerkAPP(APP店员),managerAPP(APP店长)}
        public List<RoleEnum> RoleCode { get; set; }
    }
}
