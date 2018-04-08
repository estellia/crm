using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zmind.EventBus.Contract
{
    public class EventContract
    {
        //增删改标志
        public OptEnum Operation { get; set; }
        //实体类型
        public EntityTypeEnum EntityType{ get; set; }
        //实体Identity
        public string Id { get; set; }
        //其他特殊信息
        public string OtherCon { get; set; }
    }
}
