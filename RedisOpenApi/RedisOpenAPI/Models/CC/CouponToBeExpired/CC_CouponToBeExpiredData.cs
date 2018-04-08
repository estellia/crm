using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.CouponToBeExpired
{
   public class CC_CouponToBeExpiredData
    {
        public CC_DataInfo first { get; set; }
        public CC_DataInfo keyword1 { get; set; }
        public CC_DataInfo keyword2 { get; set; }
        public CC_DataInfo keyword3 { get; set; }
        public CC_DataInfo keyword4 { get; set; }
        public CC_DataInfo remark { get; set; }
    }
}
