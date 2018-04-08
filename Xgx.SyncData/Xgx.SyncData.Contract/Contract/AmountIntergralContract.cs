using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    /// <summary>
    /// 会员积分和余额契约
    /// 对应正念数据表:vip
    /// </summary>
    public class AmountIntergralContract : IXgxToZmind
    {
        //增删改标志
        public OptEnum Operation { get; set; }
        //名称：标识
        //对应数据库字段：表vip的vipid字段
        //对应数据库字段约束：nvarchar(50),pk,32位GUID
        public string VipId { get; set; }      
        public DateTime? CreateTime { get; set; }
        //名称：修改时间
        //对应数据库字段：表vip的lastupdatetime字段
        //对应数据库字段约束：datetime, not null
        public DateTime? ModifyTime { get; set; }
        //名称：余额
        //对应数据库字段：表vipamount的TotalAmount字段
        //牵扯到表VipIntegralDetail
        //对应数据库字段约束：decimal(18,2), null
        //以增量的方式来增加，比如新增了20元，就传20，使用减去了10元，就传-10
        public decimal VipAmount { get; set; }
        //名称：积分
        //对应数据库字段：表VipIntegral的TotalAmount字段
        //牵扯到表VipIntegralDetail
        //对应数据库字段约束：int, null
        //以增量的方式来增加，比如新增了20积分，就传20，使用减去了10积分，就传-10
        public int VipIntegral { get; set; }



 
    }
}
