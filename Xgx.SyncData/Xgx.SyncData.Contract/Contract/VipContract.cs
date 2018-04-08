using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 会员消息契约
    /// 对应正念数据表:vip
    /// </summary>
    public class VipContract : IXgxToZmind, IZmindToXgx
    {
        //增删改标志
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表vip的vipid字段
        //对应数据库字段约束：nvarchar(50),pk,32位GUID
        public string VipId { get; set; }
        //名称：名称
        //对应数据库字段：表vip的vipname字段
        //对应数据库字段约束：nvarchar(50),not null
        public string VipName { get; set; }
        //名称：编码
        //对应数据库字段：表vip的vipcode字段
        //对应数据库字段约束：nvarchar(50),not null
        public string VipCode { get; set; }
        //名称：创建时间
        //对应数据库字段：表vip的createtime字段
        //对应数据库字段约束：datetime，not null
        public DateTime? CreateTime { get; set; }
        //名称：修改时间
        //对应数据库字段：表vip的lastupdatetime字段
        //对应数据库字段约束：datetime, not null
        public DateTime? ModifyTime { get; set; }
        ////名称：级别(待定)
        ////对应数据库字段：会员卡类型表SysVipCardType表里的VipCardLevel
        ////对应数据库字段约束：int, null
        //public int VipLevel { get; set; }

        ////名称：对应的会员卡的级别
        ////对应数据库字段：会员卡类型表SysVipCardType表里的VipCardTypeID
        ////对应数据库字段约束：int, null
        public int VipCardTypeID { get; set; }

        //名称：手机号
        //对应数据库字段：表vip的phone字段
        //对应数据库字段约束：nvarchar(50), null
        public string Phone { get; set; }
        //名称：证件类型
        //对应数据库字段：表vip的idtype字段
        //对应数据库字段约束：nvarchar(50), null,有效值{身份证}
        public string IdType { get; set; }
        //名称：证件号码
        //对应数据库字段：表vip的idnumber字段
        //对应数据库字段约束：nvarchar(50), null
        public string IdNumber { get; set; }
        //名称：生日
        //对应数据库字段：表vip的birthday字段
        //对应数据库字段约束：nvarchar(50)，null,有效值{yyyy-MM-dd形式的日期}
        public DateTime? Birthday { get; set; }
        //名称：性别
        //对应数据库字段：表vip的gender字段
        //对应数据库字段约束：int, null,{0(女),1(男)}
        public int Gender { get; set; }
        //名称：电子邮箱
        //对应数据库字段：表vip的email字段
        //对应数据库字段约束：nvarchar(200), null
        public string Email { get; set; }



        //名称：会员openid（微信里的标识）
        //对应数据库字段：表Vip的weixinuserid字段
        //对应数据库字段约束：nvarchar(200), null
        public string OpenID { get; set; }

        //名称：微信原始ID
        //对应数据库字段：表Vip的WeiXinID字段
        //对应数据库字段约束：nvarchar(200), null
        public string WeiXinID { get; set; }

        //名称：被替换的会员的标识
        //在正念系统里会根据手机号去绑卡，绑完卡之后，会把被绑卡会员的信息删除
        //如果OldVipID有值的话，就把这个老会员的信息绑定到上面的会员信息里，并把这个会员信息虚拟删除
        //对应数据库字段约束：nvarchar(200), null
        public string OldVipID { get; set; }

    }
}
