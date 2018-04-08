using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 卡类型、级别消息契约
    /// 对应正念数据表:会员卡类型表SysVipCardType
    /// </summary>
    public class SysVipCardTypeContract : IZmindToXgx
    {
        
        //增删改标志，not null
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表SysVipCardType的VipCardTypeID字段
        //对应数据库字段约束：int
        public int VipCardTypeID { get; set; }
        //名称：卡类型名称
        //对应数据库字段：表SysVipCardType的VipCardTypeName字段
        //对应数据库字段约束：nvarchar(50),not null
        public string VipCardTypeName { get; set; }
        //名称：卡等级
        //对应数据库字段：表SysVipCardType的VipCardTypeName字段
        //对应数据库字段约束：int
        public string VipCardLevel { get; set; }

    }
}
